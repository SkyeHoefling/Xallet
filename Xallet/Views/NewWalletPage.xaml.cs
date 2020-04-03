using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewWalletPage : ContentPage
    {
        public NewWalletPage()
        {
            InitializeComponent();
        }

        private void ScanClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ScanPage());
        }
    }
}
