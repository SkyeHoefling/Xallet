using System;
using Xallet.Data;
using Xallet.Models;

namespace Xallet.Extensions
{
    public static class WalletExtensions
    {
        public static Wallet ToWallet(this WalletEntity entity, ConversionRateEntity fiatRate)
        {
            return new Wallet
            {
                Name = entity.FriendlyName,
                Address = entity.PublicAddress,
                TokenType = entity.CryptoCurrency,
                Token = new Amount
                {
                    Currency = entity.CryptoCurrency == CryptoCurrency.Bitcoin ? "BTC" : "ETH",
                    Value = entity.CachedValue
                },
                LocalCurrency = new Amount
                {
                    Currency = $"{fiatRate.Fiat}",
                    Value = Math.Round(fiatRate.Rate * entity.CachedValue, 2)
                }
            };
        }
    }
}
