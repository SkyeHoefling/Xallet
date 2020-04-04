using Newtonsoft.Json;

namespace Xallet.Models
{
    [JsonObject]
    public class EtherscanResponse<TResult>
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("result")]
        public TResult Result { get; set; }
    }

}
