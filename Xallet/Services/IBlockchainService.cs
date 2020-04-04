using System.Threading.Tasks;

namespace Xallet.Services
{
    public interface IBlockchainService
    {
        Task<double[]> GetAccountBalanceAsync(params string[] address);
        Task<double> GetCryptoValueAsync();
    }
}
