using Xallet.Models;

namespace Xallet.ViewModels
{
    public class AddressCodeViewModel : BindableBase
    {
        public AddressCodeViewModel(Wallet wallet)
        {
            Address = wallet.Address;
        }

        private string _address;
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }
    }
}
