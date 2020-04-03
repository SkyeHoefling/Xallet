using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Xallet.ViewModels
{
    public abstract class BindableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void SetProperty<T>(ref T address, T value, [CallerMemberName]string property = "")
        {
            address = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
