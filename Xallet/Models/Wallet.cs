using Xallet.Data;
using Xallet.ViewModels;

namespace Xallet.Models
{
    public class Wallet : BindableBase
    {
        private string _id;
        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _address;
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private CryptoCurrency _tokenType;
        public CryptoCurrency TokenType
        {
            get => _tokenType;
            set => SetProperty(ref _tokenType, value);
        }

        private Amount _token;
        public Amount Token
        {
            get => _token;
            set => SetProperty(ref _token, value);
        }

        private Amount _localCurrency;
        public Amount LocalCurrency
        {
            get => _localCurrency;
            set => SetProperty(ref _localCurrency, value);
        }
    }
}
