using System;
using SQLite;

namespace Xallet.Data
{
    public class TransactionEntity
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string PublicAddress { get; set; }
        public DateTime Timestamp { get; set; }
        public long Value { get; set; }
    }
}
