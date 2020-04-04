using Newtonsoft.Json;

namespace Xallet.Models
{
    [JsonObject]
    public class BlockchainPrice
    {
        [JsonProperty("15m")]
        public double FifteenMinutesPrice { get; set; }

        [JsonProperty("last")]
        public double LastPrice { get; set; }

        [JsonProperty("buy")]
        public double BuyPrice { get; set; }

        [JsonProperty("sell")]
        public double SellPrice { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }
}
