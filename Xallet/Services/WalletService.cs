using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SQLite;
using Xallet.Data;

namespace Xallet.Services
{
    public class WalletService
    {
        protected SQLiteConnection Connection { get; }
        public WalletService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "wallets.db");
            Connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex);
            Connection.CreateTable<WalletEntity>();
        }

        public IEnumerable<WalletEntity> GetWallets()
        {
            return Connection.Table<WalletEntity>().ToArray();
        }

        public WalletEntity AddWallet(string name, string address)
        {
            var entity = new WalletEntity
            {
                Id = $"{Guid.NewGuid()}",
                FriendlyName = name,
                PublicAddress = address,
                CryptoCurrency = GetCryptoCurrency(),
                Timestamp = DateTime.UtcNow
            };

            Connection.InsertOrReplace(entity);
            return entity;

            CryptoCurrency GetCryptoCurrency()
            {
                // this is a naive check, but will work for our purposes here
                return address.StartsWith("0x") ? CryptoCurrency.Ethereum : CryptoCurrency.Bitcoin;
            };
        }

        public void RemoveWallet(string id)
        {
            var findWallet = Connection.Find<WalletEntity>(id);
            if (findWallet == null)
                return;

            Connection.Delete<WalletEntity>(findWallet.Id);
        }

        internal async Task SyncWithBlockchainAsync()
        {
            var accounts = GetWallets();

            await EthereumAsync();
            await BitcoinAsync();

            async Task EthereumAsync()
            {
                var etherService = new EtherScanService();
                var ethereumAccounts = accounts.Where(x => x.CryptoCurrency == CryptoCurrency.Ethereum).ToArray();
                var ethereumValues = await etherService.GetAccountBalanceAsync(ethereumAccounts.Select(x => x.PublicAddress).ToArray());
                for (int index = 0; index < ethereumAccounts.Length; index++)
                {
                    ethereumAccounts[index].CachedValue = ethereumValues[index];
                    ethereumAccounts[index].Timestamp = DateTime.UtcNow;

                    Connection.InsertOrReplace(ethereumAccounts[index]);
                }

                var etherUSD = await etherService.GetCryptoValueAsync();
                var newEntity = new ConversionRateEntity
                {
                    Id = $"{Guid.NewGuid()}",
                    Crypto = CryptoCurrency.Ethereum,
                    Fiat = FiatCurrency.USD,
                    Rate = etherUSD,
                    Timestamp = DateTime.UtcNow
                };

                Connection.InsertOrReplace(newEntity);
            }

            async Task BitcoinAsync()
            {
                var bitcoinAccounts = accounts.Where(x => x.CryptoCurrency == CryptoCurrency.Bitcoin).ToArray();
                await Task.Delay(0);
            }
        }
    }
}
