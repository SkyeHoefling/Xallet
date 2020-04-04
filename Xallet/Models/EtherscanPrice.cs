using Newtonsoft.Json;

namespace Xallet.Models
{
    [JsonObject]
    public class EtherscanPrice
    {
        [JsonProperty("ethbtc")]
        public double EtherBitcoin { get; set; }

        [JsonProperty("ethbtc_timestamp")]
        public string EtherBitcoinTimestamp { get; set; }

        [JsonProperty("ethusd")]
        public double EtherUSD { get; set; }

        [JsonProperty("ethusd_timestamp")]
        public string EtherUSDimestamp { get; set; }
    }
}
