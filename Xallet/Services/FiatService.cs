using System;
using System.IO;
using SQLite;
using Xallet.Data;

namespace Xallet.Services
{
    public class FiatService
    {
        protected SQLiteConnection Connection { get; }
        public FiatService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "wallets.db");
            Connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex);
            Connection.CreateTable<ConversionRateEntity>();
        }

        public ConversionRateEntity GetCurrentRate(CryptoCurrency crypto)
        {
            var findLatest = Connection
                .Table<ConversionRateEntity>()
                .Where(x => x.Crypto == crypto)
                .OrderByDescending(x => x.Timestamp)
                .FirstOrDefault();

            return findLatest == null ?
                null :
                new ConversionRateEntity
                {
                    Id = findLatest.Id,
                    Crypto = findLatest.Crypto,
                    Fiat = findLatest.Fiat,
                    Rate = findLatest.Rate,
                    Timestamp = findLatest.Timestamp
                };
        }
    }
}
