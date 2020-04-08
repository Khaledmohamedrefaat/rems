using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management_Software.DatabaseModels
{
    public class RentalModel : TransactionModel
    {
        public int Price;
        public int Insurance;
        public DateTime End;

        public RentalModel() : base()
        {

        }
        public RentalModel(string status, DateTime transactionTime, int assetId, int amountCollected, string nationalID, int price, int insurance, DateTime end) : base(status, transactionTime, assetId, amountCollected, nationalID)
        {
            this.Price = price;
            this.Insurance = insurance;
            this.End = end;
        }
    }
}
