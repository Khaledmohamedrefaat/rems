using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management_Software.DatabaseModels
{
    public abstract class TransactionModel
    {
        [BsonId]
        public Guid Mongo_id { get; set; }
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime TransactionTime { get; set; }
        public int AssetId { get; set; }
        public int AmountCollected { get; set; }
        public string NationalID { get; set; }

        public TransactionModel()
        {

        }
        public TransactionModel(string status, DateTime transactionTime, int assetId, int amountCollected, string nationalID)
        {
            this.Status = status;
            this.TransactionTime = transactionTime;
            this.AssetId = assetId;
            this.AmountCollected = amountCollected;
            this.NationalID = nationalID;
        }
    }
}
