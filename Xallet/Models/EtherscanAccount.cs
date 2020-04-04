using Newtonsoft.Json;

namespace Xallet.Models
{
    [JsonObject]
    public class EtherscanAccount
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("balance")]
        public double Balance { get; set; }
    }
}
