using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Xallet.Data;
using Xallet.Models;

namespace Xallet.Services
{
    public class TransactionService
    {
        protected WalletService WalletService { get; }
        protected SQLiteConnection Connection { get; }
        public TransactionService()
        {
            WalletService = new WalletService();

            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "wallets.db");
            Connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex);
            Connection.CreateTable<TransactionEntity>();
        }

        public IEnumerable<Transaction> GetTransactions(string address)
        {
            return Connection
                .Table<TransactionEntity>()
                .Where(x => x.PublicAddress == address)
                .OrderByDescending(x => x.Timestamp)
                .Select(x => new Transaction
                {
                    Hash = x.Id,
                    PublicAddress = x.PublicAddress,
                    Timestamp = x.Timestamp,
                    Tokens = x.Value
                })
                .ToArray();
        }

        internal async Task SyncWithBlockchainAsync(string address)
        {
            await EthereumAsync();
            await BitcoinAsync();

            async Task EthereumAsync()
            {
                await Task.Delay(0);
            }

            async Task BitcoinAsync()
            {
                var blockchainService = new BlockchainService();
                var transactions = await blockchainService.GetTransactionsAsync(address);

                foreach (var item in transactions)
                {
                    var currentTransaction = Connection.Find<TransactionEntity>(item.Hash);
                    if (currentTransaction == null)
                        currentTransaction = new TransactionEntity();

                    currentTransaction.Id = item.Hash;
                    currentTransaction.PublicAddress = item.PublicAddress;
                    currentTransaction.Timestamp = item.Timestamp;
                    currentTransaction.Value = item.Tokens;

                    Connection.InsertOrReplace(currentTransaction);
                }
            }
        }
    }
}
