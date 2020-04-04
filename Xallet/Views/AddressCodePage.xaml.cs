using Xallet.Models;
using Xallet.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xallet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressCodePage : ContentPage
    {
        public AddressCodePage()
        {
            InitializeComponent();
        }

        public AddressCodePage(Wallet wallet) : this()
        {
            BindingContext = new AddressCodeViewModel(wallet);
        }
    }
}
