using MongoDB.Driver;
using Real_Estate_Managment_Software___GUI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Managment_Software___GUI.FunctionalClasses
{
    public class Store : Asset
    {
        public StoreModel Model;
        public Store() : base()
        {
            Model = new StoreModel();
        }
        public Store(StoreModel model)
        {
            this.Model = model;
        }



        public static async Task<List<StoreModel>> getAllModels(StoreModel record)
        {
            MongoDBConnection db = new MongoDBConnection();
            var collection = db.db.GetCollection<StoreModel>("Stores");
            var filter = Builders<StoreModel>.Filter.Empty;
            if (record.Id != "") filter &= Builders<StoreModel>.Filter.Eq("Id", record.Id);
            if (record.Area != -1) filter &= Builders<StoreModel>.Filter.Eq("Area", record.Area);
            if (record.City != "") filter &= Builders<StoreModel>.Filter.Eq("City", record.City);
            if (record.Governorate != "") filter &= Builders<StoreModel>.Filter.Eq("Governorate", record.Governorate);
            if (record.Street != "") filter &= Builders<StoreModel>.Filter.Eq("Address", record.Street);
            if (record.Status != "") filter &= Builders<StoreModel>.Filter.Eq("Status", record.Status);
            if (record.price != -1) filter &= Builders<StoreModel>.Filter.Eq("price", record.price);
            var ret = await collection.FindAsync<StoreModel>(filter);
            return ret.ToList();
        }
    }
}
