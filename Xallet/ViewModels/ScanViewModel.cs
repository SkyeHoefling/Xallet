using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;

namespace Xallet.ViewModels
{
    public class ScanViewModel
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

        private void OnScanResult(object item)
        {
            if (item is Result result)
            {
                //result.Text
            }
        }
    }
}
