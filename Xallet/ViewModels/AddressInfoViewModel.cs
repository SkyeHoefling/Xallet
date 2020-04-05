using System.Collections.ObjectModel;
using System.Windows.Input;
using Xallet.Data;
using Xallet.Models;
using Xallet.Views;
using Xamarin.Forms;

namespace Xallet.ViewModels
{
    public class AddressInfoViewModel : BindableBase
    {
        private bool _isBusy = false;
        public AddressInfoViewModel(Wallet wallet)
        {
            Wallet = wallet;
            Edit = new Command(OnEdit);
            Back = new Command<bool>(OnBack);
            ShowCode = new Command(OnShowCode);
            MessagingCenter.Instance.Subscribe<AddOrUpdateWalletViewModel, WalletEntity>(this, "AddOrUpdateWallet", OnNewWallet);

            Transactions = new ObservableCollection<string>(new[] { "Hello", "World" });
        }

        public ICommand Edit { get; }
        public ICommand Back { get; }
        public ICommand ShowCode { get; }

        private Wallet _wallet;
        public Wallet Wallet
        {
            get => _wallet;
            set => SetProperty(ref _wallet, value);
        }

        private ObservableCollection<string> _transactions;
        public ObservableCollection<string> Transactions
        {
            get => _transactions;
            set => SetProperty(ref _transactions, value);
        }

        private void OnEdit()
        {
            if (_isBusy)
                return;

            App.Current.MainPage.Navigation.PushAsync(new AddOrUpdateWalletPage(Wallet));
            _isBusy = false;
        }

        private void OnBack(bool doNavigation)
        {
            if (_isBusy)
                return;

            MessagingCenter.Instance.Unsubscribe<AddressInfoViewModel>(this, "AddOrUpdateWallet");

            if (doNavigation)
                App.Current.MainPage.Navigation.PopAsync();

            _isBusy = false;
        }

        private void OnShowCode()
        {
            if (_isBusy)
                return;

            App.Current.MainPage.Navigation.PushAsync(new AddressCodePage(Wallet));
            _isBusy = false;
        }

        private void OnNewWallet(AddOrUpdateWalletViewModel sender, WalletEntity args)
        {
            if (args != null)
            {
                Wallet.Address = args.PublicAddress;
                Wallet.Name = args.FriendlyName;
            }
        }
    }
}
