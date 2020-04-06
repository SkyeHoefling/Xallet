using Newtonsoft.Json;

namespace Xallet.Models
{
    [JsonObject]
    public class BlockchainRawAddress
    {
        [JsonProperty("hash160")]
        public string Hash160 { get; set; }

        [JsonProperty("address")]
        public string PublicAddress { get; set; }

        [JsonProperty("n_tx")]
        public string TransactionCount { get; set; }

        [JsonProperty("total_received")]
        public long TotalReceived { get; set; }

        [JsonProperty("total_sent")]
        public long TotalSent { get; set; }

        [JsonProperty("final_balance")]
        public long Balance { get; set; }

        [JsonProperty("txs")]
        public BlockchainTransaction[] Transactions { get; set; }
    }

    [JsonObject]
    public class BlockChainTransactionInput
    {
        [JsonProperty("sequence")]
        public long Sequence { get; set; }

        [JsonProperty("witness")]
        public string Witness { get; set; }

        [JsonProperty("script")]
        public string Script { get; set; }

        [JsonProperty("prev_out")]
        public BlockchainPreviousOutput PreviousOutput { get; set; }
    }

    [JsonObject]
    public class BlockchainPreviousOutput
    {
        [JsonProperty("spent")]
        public bool Spent { get; set; }

        [JsonProperty("tx_index")]
        public int TransactionIndex { get; set; }

        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("addr")]
        public string PublicAddress { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }

        [JsonProperty("n")]
        public int Number { get; set; }

        [JsonProperty("script")]
        public string Script { get; set; }
    }


    [JsonObject]
    public class BlockchainTransaction
    {
        [JsonProperty("ver")]
        public double Version { get; set; }

        [JsonProperty("inputs")]
        public BlockChainTransactionInput[] Inputs { get; set; }

        [JsonProperty("weight")]
        public long Weight { get; set; }

        [JsonProperty("block_height")]
        public long BlockHeight { get; set; }

        [JsonProperty("relayed_by")]
        public string RelayedBy { get; set; }

        [JsonProperty("out")]
        public BlockchainPreviousOutput[] Out { get; set; }

        [JsonProperty("lock_time")]
        public long LockTime { get; set; }

        [JsonProperty("result")]
        public long Result { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("block_index")]
        public int BlockIndex { get; set; }

        [JsonProperty("time")]
        public int Time { get; set; }

        [JsonProperty("tx_index")]
        public int TransactionIndex { get; set; }

        [JsonProperty("vin_sz")]
        public long vin_sz { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("vout_sz")]
        public long vout_sz { get; set; }
    }
}
