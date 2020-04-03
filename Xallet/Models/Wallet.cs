namespace Xallet.Models
{
    public class Wallet
    {
        public string Name { get; set; }
        public Amount Token { get; set; }
        public Amount LocalCurrency { get; set; }
    }
}
