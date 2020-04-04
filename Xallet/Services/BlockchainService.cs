using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xallet.Models;

namespace Xallet.Services
{
    public class BlockchainService : IBlockchainService
    {
        private const string API_Accounts = "https://blockchain.info/balance?active={0}";
        private const double TokenFactor = 100000000;
        public async Task<double[]> GetAccountBalanceAsync(params string[] addresses)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var aggregatedAddresses = addresses.Aggregate((x, y) => $"{x}|{y}");
                    var route = string.Format(API_Accounts, aggregatedAddresses);

                    var result = await client.GetAsync(route);
                    if (result.IsSuccessStatusCode)
                    {
                        var content = await result.Content.ReadAsStringAsync();
                        var json = JObject.Parse(content);

                        var blockchainBalances = new List<BlockchainBalance>();
                        for (int index = 0; index < addresses.Length; index++)
                        {
                            var findBalance = json.SelectToken(addresses[index]);
                            if (findBalance != null)
                                blockchainBalances.Add(findBalance.ToObject<BlockchainBalance>());
                        }

                        return blockchainBalances.Select(x => x.Balance / TokenFactor).ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return new double[addresses.Length];
        }

        public async Task<double> GetCryptoValueAsync()
        {
            await Task.Delay(0);
            return 6600;
            //try
            //{
            //    using (var client = new HttpClient())
            //    {
            //        var apiKey = AppSettingsManager.Settings["EtherScan:ApiToken"];
            //        var route = string.Format(API_Price, apiKey);
            //
            //        var result = await client.GetAsync(route);
            //        if (result.IsSuccessStatusCode)
            //        {
            //            var content = await result.Content.ReadAsStringAsync();
            //            var etherscanResponse = JsonConvert.DeserializeObject<EtherscanResponse<EtherscanPrice>>(content);
            //            return etherscanResponse.Result.EtherUSD;
            //        }
            //    }
            //}
            //catch
            //{
            //}
            //
            //return 0;
        }
    }
}
