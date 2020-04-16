using MongoDB.Driver;
using Real_Estate_Managment_Software___GUI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Managment_Software___GUI.FunctionalClasses
{
    public class Factory : Asset
    {
        public FactoryModel Model;

        public Factory() : base()
        {

        }
        public Factory(FactoryModel model)
        {
            this.Model = model;
        }
        public static async Task<List<FactoryModel>> getAllModels(FactoryModel record)
        {
            MongoDBConnection db = new MongoDBConnection();
            var collection = db.db.GetCollection<FactoryModel>("Factories");
            var filter = Builders<FactoryModel>.Filter.Empty;
            if (record.Id != -1) filter &= Builders<FactoryModel>.Filter.Eq("Id", record.Id);
            if (record.Area != -1) filter &= Builders<FactoryModel>.Filter.Eq("Area", record.Area);
            if (record.Address != "") filter &= Builders<FactoryModel>.Filter.Eq("Address", record.Address);
            if (record.Status != "") filter &= Builders<FactoryModel>.Filter.Eq("Status", record.Status);
            if (record.price != -1) filter &= Builders<FactoryModel>.Filter.Eq("price", record.price);
            var ret = await collection.FindAsync<FactoryModel>(filter);
            return ret.ToList();
        }
    }
}
