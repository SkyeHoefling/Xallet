using System;
using System.Windows.Input;
using Xallet.Services;
using Xamarin.Forms;
using ZXing;

namespace Xallet.ViewModels
{
    public class NewWalletViewModel : BindableBase
    {
        public NewWalletViewModel()
        {
            Scan = new Command(OnScan);
            Save = new Command(OnSave);
            MessagingCenter.Instance.Subscribe<ScanViewModel, Result>(this, "QRScanReceived", OnScanReceived);
        }
        
        public ICommand Scan { get; }
        public ICommand Save { get; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _address;
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private bool _isScanning = false;
        private void OnScan()
        {
            if (_isScanning)
                return;

            _isScanning = true;
            App.Current.MainPage.Navigation.PushAsync(new Views.ScanPage());
        }

        private bool _isSaving = false;
        private void OnSave()
        {
            if (_isSaving)
                return;

            _isSaving = true;
            MessagingCenter.Instance.Unsubscribe<ScanViewModel, Result>(this, "QRScanReceived");
            

            var service = new WalletService();
            var entity = service.AddWallet(Name, Address);
            MessagingCenter.Instance.Send(this, "NewWallet", entity);
            App.Current.MainPage.Navigation.PopAsync();
        }

        private void OnScanReceived(object sender, Result args)
        {
            if (args != null && args is Result result)
                Address = result.Text;
        }
    }
}
