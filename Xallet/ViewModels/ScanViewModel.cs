using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;

namespace Xallet.ViewModels
{
    public class ScanViewModel : BindableBase
    {
        public ScanViewModel()
        {
            ScanResult = new Command(OnScanResult);
            ScannerOptions = new MobileBarcodeScanningOptions
            {
                AutoRotate = true,
                PossibleFormats = new List<BarcodeFormat>(new[] { BarcodeFormat.QR_CODE })
            };
        }

        public ICommand ScanResult { get; }
        public MobileBarcodeScanningOptions ScannerOptions { get; }

        private bool _isScanning = true;
        public bool IsScanning
        {
            get => _isScanning;
            set => SetProperty(ref _isScanning, value);
        }

        private void OnScanResult(object item)
        {
            if (item is Result result)
            {
                MessagingCenter.Instance.Send(this, "QRScanReceived", result);
                MainThread.BeginInvokeOnMainThread(() => App.Current.MainPage.Navigation.PopAsync());
            }
        }
    }
}
