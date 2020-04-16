using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Managment_Software___GUI.DatabaseModels
{
    public class SoldModel : TransactionModel
    {
        public int InsallDeposit;
        public int InstallDuration;
        public int InstallPrice;

        public SoldModel() : base()
        {

        }
        public SoldModel(string status, DateTime transactionTime, SIPair assetId, int amountCollected, string nationalID, int installDeposit, int installDuration, int installPrice) : base(status, transactionTime, assetId, amountCollected, nationalID)
        {
            this.InsallDeposit = installDeposit;
            this.InstallDuration = installDuration;
            this.InstallPrice = installPrice;
        }
    }
}
