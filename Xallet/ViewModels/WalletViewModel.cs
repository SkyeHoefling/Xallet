using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xallet.Data;
using Xallet.Extensions;
using Xallet.Models;
using Xallet.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Xallet.ViewModels
{
    public class WalletViewModel : BindableBase
    {
        protected WalletService WalletService { get; }
        protected FiatService FiatService { get; }
        public WalletViewModel()
        {
            WalletService = new WalletService();
            FiatService = new FiatService();

            MessagingCenter.Instance.Subscribe<NewWalletViewModel, WalletEntity>(this, "NewWallet", OnNewWallet);
            Add = new Command(OnAdd);
            
            Initialize();
        }

        public ICommand Add { get; }

        public Amount TotalAmount { get; set; }

        private ObservableCollection<Wallet> _wallets;
        public ObservableCollection<Wallet> Wallets
        {
            get => _wallets;
            set => SetProperty(ref _wallets, value);
        }

        private async void Initialize()
        {
            LoadLocalData();
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                await WalletService.SyncWithBlockchainAsync();
                LoadLocalData();
            }
        }

        private void LoadLocalData()
        {
            var wallets = WalletService
                .GetWallets()
                .OrderBy(x => x.FriendlyName)
                .Select(x =>
                {
                    var rate = FiatService.GetCurrentRate(x.CryptoCurrency);
                    return x.ToWallet(rate);
                });

            Wallets = new ObservableCollection<Wallet>(wallets);

            TotalAmount = new Amount
            {
                Value = Wallets == null || Wallets.Count == 0 ? 0 : Wallets.Select(x => x.LocalCurrency.Value).Aggregate((x,y) => x + y),
                Currency = "USD"
            };
        }

        private void OnAdd()
        {
            App.Current.MainPage.Navigation.PushAsync(new Views.NewWalletPage());
        }

        private void OnNewWallet(NewWalletViewModel sender, WalletEntity args)
        {
            // TODO: this needs to work with LoadData
            var rate = FiatService.GetCurrentRate(args.CryptoCurrency);
            var newWallet = args.ToWallet(rate);

            var tempWallets = Wallets.ToList();
            tempWallets.Add(newWallet);

            var orderedWallets = tempWallets.OrderBy(x => x.Name).ToList();
            Wallets = new ObservableCollection<Wallet>(orderedWallets);
        }
    }
}
