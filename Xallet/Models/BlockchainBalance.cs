using Newtonsoft.Json;

namespace Xallet.Models
{
    [JsonObject]
    public class BlockchainBalance
    {
        [JsonProperty("final_balance")]
        public double Balance { get; set; }

        [JsonProperty("n_tx")]
        public int TransactionCount { get; set; }

        [JsonProperty("total_received")]
        public double TotalReceived { get; set; }
    }
}
