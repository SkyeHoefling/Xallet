using Xallet.Models;
using Xallet.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressInfoPage : ContentPage
    {
        public AddressInfoPage()
        {
            InitializeComponent();
        }

        public AddressInfoPage(Wallet wallet) : this()
        {
            BindingContext = new AddressInfoViewModel(wallet);
        }

        protected override bool OnBackButtonPressed()
        {
            if (BindingContext is AddressInfoViewModel viewModel)
                viewModel.Back.Execute(false);

            return base.OnBackButtonPressed();
        }
    }
}
