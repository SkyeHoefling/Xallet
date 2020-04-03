using System.Windows.Input;
using Xamarin.Forms;

namespace Xallet.ViewModels
{
    public class NewWalletViewModel : BindableBase
    {
        public NewWalletViewModel()
        {
            Save = new Command(OnSave);
        }
        public ICommand Save { get; }

        private string _address;
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private void OnSave()
        {
        }
    }
}
