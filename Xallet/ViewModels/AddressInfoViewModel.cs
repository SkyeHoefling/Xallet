using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xallet.Data;
using Xallet.Models;
using Xallet.Services;
using Xallet.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Xallet.ViewModels
{
    public class AddressInfoViewModel : BindableBase
    {
        private bool _isBusy = false;
        protected TransactionService TransactionService { get; }
        public AddressInfoViewModel(Wallet wallet)
        {
            TransactionService = new TransactionService();
            Wallet = wallet;
            Edit = new Command(OnEdit);
            Back = new Command<bool>(OnBack);
            ShowCode = new Command(OnShowCode);
            MessagingCenter.Instance.Subscribe<AddOrUpdateWalletViewModel, WalletEntity>(this, "AddOrUpdateWallet", OnNewWallet);

            Transactions = new ObservableCollection<Transaction>(new Transaction[0]);
            Initialize();
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

        private ObservableCollection<Transaction> _transactions;
        public ObservableCollection<Transaction> Transactions
        {
            get => _transactions;
            set => SetProperty(ref _transactions, value);
        }

        private async void Initialize()
        {
            LoadData();
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                await TransactionService.SyncWithBlockchainAsync(Wallet.Address);
                LoadData();
            }
        }

        private void LoadData()
        {
            var items = TransactionService.GetTransactions(Wallet.Address);
            Transactions = new ObservableCollection<Transaction>(items);
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
