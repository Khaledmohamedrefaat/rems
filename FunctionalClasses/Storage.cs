using MongoDB.Driver;
using Real_Estate_Managment_Software___GUI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Managment_Software___GUI.FunctionalClasses
{
    public class Storage : Asset
    {
        public StorageModel Model;
        public Storage() : base()
        {
            Model = new StorageModel();
        }
        public Storage(StorageModel model)
        {
            this.Model = model;
        }

        public static async Task<List<StorageModel>> getAllModels(StorageModel record)
        {
            MongoDBConnection db = new MongoDBConnection();
            var collection = db.db.GetCollection<StorageModel>("Storages");
            var filter = Builders<StorageModel>.Filter.Empty;
            if (record.Id != "") filter &= Builders<StorageModel>.Filter.Eq("Id", record.Id);
            if (record.Area != -1) filter &= Builders<StorageModel>.Filter.Eq("Area", record.Area);
            if (record.City != "") filter &= Builders<StorageModel>.Filter.Eq("City", record.City);
            if (record.Governorate != "") filter &= Builders<StorageModel>.Filter.Eq("Governorate", record.Governorate);
            if (record.Street != "") filter &= Builders<StorageModel>.Filter.Eq("Address", record.Street);
            if (record.Status != "") filter &= Builders<StorageModel>.Filter.Eq("Status", record.Status);
            if (record.price != -1) filter &= Builders<StorageModel>.Filter.Eq("price", record.price);
            var ret = await collection.FindAsync<StorageModel>(filter);
            return ret.ToList();
        }

    }
}
