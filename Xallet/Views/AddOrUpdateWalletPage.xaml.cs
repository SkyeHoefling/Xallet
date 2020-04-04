
using Xallet.Models;
using Xallet.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddOrUpdateWalletPage : ContentPage
    {
        public AddOrUpdateWalletPage()
        {
            InitializeComponent();
            BindingContext = new AddOrUpdateWalletViewModel();
        }

        public AddOrUpdateWalletPage(Wallet wallet)
        {
            InitializeComponent();
            BindingContext = new AddOrUpdateWalletViewModel(wallet);
        }
    }
}
