using System;
using System.Windows.Input;
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

        private string _address;
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private void OnScan()
        {
            App.Current.MainPage.Navigation.PushAsync(new Views.ScanPage());
        }

        private void OnSave()
        {
        }

        private void OnScanReceived(object sender, Result args)
        {
            if (args != null && args is Result result)
                Address = result.Text;
        }
    }
}
