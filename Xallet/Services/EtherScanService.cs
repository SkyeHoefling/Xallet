using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xallet.Extensions;
using Xallet.Models;

namespace Xallet.Services
{
    public class EtherScanService : IBlockchainService
    {
        private const string API_Accounts = "https://api.etherscan.io/api?module=account&action=balancemulti&address={0}&tag=latest&apikey={1}";
        private const string API_Price = "https://api.etherscan.io/api?module=stats&action=ethprice&apikey={0}";
        private const string API_Transactions = "https://api.etherscan.io/api?module=account&action=txlist&address={0}&startblock=0&endblock=99999999&sort=asc&apikey={1}";
        private const double TokenFactor = 1000000000000000000;
        public async Task<double[]> GetAccountBalanceAsync(params string[] addresses)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var aggregatedAddresses = addresses.Aggregate((x, y) => $"{x},{y}");
                    var apiKey = AppSettingsManager.Settings["EtherScan:ApiToken"];
                    var route = string.Format(API_Accounts, aggregatedAddresses,  apiKey);

                    var result = await client.GetAsync(route);
                    if (result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();
                        var etherscanResponse = JsonConvert.DeserializeObject<EtherscanResponse<EtherscanAccount[]>>(content);
                        return etherscanResponse.Result.Select(x => x.Balance / TokenFactor).ToArray();
                    }
                }
            }
            catch
            {
            }

            return new double[addresses.Length];
        }

        public async Task<double> GetCryptoValueAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var apiKey = AppSettingsManager.Settings["EtherScan:ApiToken"];
                    var route = string.Format(API_Price, apiKey);

                    var result = await client.GetAsync(route);
                    if (result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();
                        var etherscanResponse = JsonConvert.DeserializeObject<EtherscanResponse<EtherscanPrice>>(content);
                        return etherscanResponse.Result.EtherUSD;
                    }
                }
            }
            catch
            {
            }

            return 0;
        }

        public async Task<Transaction[]> GetTransactionsAsync(string address)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var apiKey = AppSettingsManager.Settings["EtherScan:ApiToken"];
                    var route = string.Format(API_Transactions, address, apiKey);

                    var result = await client.GetAsync(route);
                    if (result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();
                        var etherscanResponse = JsonConvert.DeserializeObject<EtherscanResponse<EtherscanTransaction[]>>(content);
                        return etherscanResponse.Result
                            .Select(x => new Transaction
                            {
                                Hash = x.Hash,
                                PublicAddress = address,
                                Timestamp = DateTimeExtensions.UnixTimeToDateTimeUtc(x.Timestamp),
                                Tokens = x.Value / TokenFactor
                            })
                            .ToArray();
                    }
                }
            }
            catch
            {
            }

            return new Transaction[0];
        }
    }
}
