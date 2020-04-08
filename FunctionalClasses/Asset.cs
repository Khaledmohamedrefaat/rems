using Real_Estate_Management_Software.ConnectionClasses;
using Real_Estate_Management_Software.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management_Software.FunctionalClasses
{
    public abstract class Asset{
        public AssetModel Model;
        public List<Apartment> Apartments;
        public List<Storage> Storages;
        public List<Store> Stores;

        public Asset(){

        }
        public Asset(AssetModel model)
        {
            this.Model = model;
        }
        public async Task<string> LoadAssetById(SIPair unit){
            throw new NotImplementedException();
        }

        public void LoadUnits(){
            throw new NotImplementedException();
        }

        public async Task<string> Sell()
        {
            throw new NotImplementedException();
        }

        public async Task<string> Rent()
        {
            throw new NotImplementedException();
        }

        public async Task<string> getInstallment()
        {
            throw new NotImplementedException();
        }

        public async Task<string> Delete()
        {
            throw new NotImplementedException();
        }

        public async Task<string> Insert()
        {
            throw new NotImplementedException();
        }
        public async Task<string> Update()
        {
            throw new NotImplementedException();
        }
    }
}
