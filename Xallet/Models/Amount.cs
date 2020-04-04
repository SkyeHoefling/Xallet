using Xallet.ViewModels;

namespace Xallet.Models
{
    public class Amount : BindableBase
    {
        private double _value;
        public double Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        private string _currency;
        public string Currency
        {
            get => _currency;
            set => SetProperty(ref _currency, value);
        }
    }
}
