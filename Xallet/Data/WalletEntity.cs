using System;
using SQLite;

namespace Xallet.Data
{
    public class WalletEntity
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string FriendlyName { get; set; }
        public string PublicAddress { get; set; }
        public CryptoCurrency CryptoCurrency { get; set; }
        public double CachedValue { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public enum CryptoCurrency
    {
        Unknown = -1,
        Bitcoin = 0,
        Ethereum = 1
    }
}
