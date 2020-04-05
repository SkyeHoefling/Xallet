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
            MessagingCenter.Instance.Subscribe<AddOrUpdateWalletViewModel, WalletEntity>(this, "AddOrUpdateWallet", OnNewWallet);
        }

        public ICommand Edit { get; }
        public ICommand Back { get; }

        private Wallet _wallet;
        public Wallet Wallet
        {
            get => _wallet;
            set => SetProperty(ref _wallet, value);
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
