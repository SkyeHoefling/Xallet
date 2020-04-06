using Newtonsoft.Json;

namespace Xallet.Models
{
    [JsonObject]
    public class EtherscanTransaction
    {
        [JsonProperty("blockNumber")]
        public long BlockNumber { get; set; }

        [JsonProperty("timeStamp")]
        public long Timestamp { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        [JsonProperty("blockHash")]
        public string BlockHash { get; set; }

        [JsonProperty("transactionIndex")]
        public int TransactionIndex { get; set; }

        [JsonProperty("from")]
        public string FromAddress { get; set; }

        [JsonProperty("to")]
        public string ToAddress { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }

        [JsonProperty("gas")]
        public long Gas { get; set; }

        [JsonProperty("gasPrice")]
        public long GasPrice { get; set; }

        [JsonProperty("isError")]
        public int IsError { get; set; }

        [JsonProperty("txreceipt_status")]
        public int TransactionReceiptStatus { get; set; }

        [JsonProperty("input")]
        public string Input { get; set; }

        [JsonProperty("contractAddress")]
        public string ContractAddress { get; set; }

        [JsonProperty("cumulativeGasUsed")]
        public long CumulativeGasUsed { get; set; }

        [JsonProperty("gasUsed")]
        public long GasUsed { get; set; }

        [JsonProperty("confirmations")]
        public long Confirmations { get; set; }
    }
}
