using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management_Software.DatabaseModels
{
    public class SoldModel : TransactionModel
    {

        public SoldModel() : base()
        {

        }
        public int InsallDeposit;
        public int InstallDuration;
        public SoldModel(string status, DateTime transactionTime, int assetId, int amountCollected, string nationalID, int installDeposit, int installDuration) : base(status, transactionTime, assetId, amountCollected, nationalID)
        {
            this.InsallDeposit = installDeposit;
            this.InstallDuration = installDuration;
        }
    }
}
