using System.Threading.Tasks;
using Xallet.Models;

namespace Xallet.Services
{
    public interface IBlockchainService
    {
        Task<double[]> GetAccountBalanceAsync(params string[] address);
        Task<double> GetCryptoValueAsync();
        Task<Transaction[]> GetTransactionsAsync(string address);
    }
}
