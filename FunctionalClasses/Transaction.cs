using Real_Estate_Management_Software.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management_Software.FunctionalClasses
{
    public abstract class Transaction
    {
        public Transaction() 
        {

        }

        public async Task<string> Cancel()
        {
            throw new NotImplementedException();
        }

        public async Task<string> Edit()
        {
            throw new NotImplementedException();
        }

        public async Task<string> Update()
        {
            throw new NotImplementedException();
        }

        public async Task<Asset> ViewAsset()
        {
            throw new NotImplementedException();
        }
    }
}
