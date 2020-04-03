using System.Collections.ObjectModel;
using Xallet.Models;

namespace Xallet.ViewModels
{
    public class WalletViewModel
    {
        public WalletViewModel()
        {
            TotalAmount = new Amount { Value = 102.42, Currency = "USD" };
            Wallets = new ObservableCollection<Wallet>();
            Wallets.Add(new Wallet { Name = "Wallet 1", LocalCurrency = new Amount { Value = 100, Currency = "USD" }, Token = new Amount { Value = 0.3455672, Currency = "BTC" } });
            Wallets.Add(new Wallet { Name = "Wallet 2", LocalCurrency = new Amount { Value = 158.23, Currency = "USD" }, Token = new Amount { Value = 1, Currency = "ETH" } });
            Wallets.Add(new Wallet { Name = "Wallet 3", LocalCurrency = new Amount { Value = 1857, Currency = "USD" } , Token = new Amount { Value = .99993452, Currency = "ETH" } });
        }

        public Amount TotalAmount { get; set; }
        public ObservableCollection<Wallet> Wallets { get; }
    }
}
