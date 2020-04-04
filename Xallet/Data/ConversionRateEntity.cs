using System;
using SQLite;

namespace Xallet.Data
{
    public class ConversionRateEntity
    {
        [PrimaryKey]
        public string Id { get; set; }
        public CryptoCurrency Crypto { get; set; }
        public FiatCurrency Fiat { get; set; }
        public double Rate { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public enum FiatCurrency
    {
        USD = 0
    }
}
