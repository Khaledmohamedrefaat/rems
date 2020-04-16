using MongoDB.Driver;
using Real_Estate_Managment_Software___GUI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Managment_Software___GUI.FunctionalClasses
{
    public class Sold : Transaction
    {
        public SoldModel Model;
        public Sold() : base()
        {

        }
        public Sold(SoldModel model)
        {
            this.Model = model;
        }

        public static async Task<List<SoldModel>> getAllModels(SoldModel record)
        {
            MongoDBConnection db = new MongoDBConnection();
            var collection = db.db.GetCollection<SoldModel>("Sold");
            var filter = Builders<SoldModel>.Filter.Empty;
            if (record.Name != "") filter &= Builders<SoldModel>.Filter.Eq("Name", record.Name);
            if (record.AssetId != null) filter &= Builders<SoldModel>.Filter.Eq("AssetId", record.AssetId);
            var ret = await collection.FindAsync<SoldModel>(filter);
            return ret.ToList();
        }
    }
}
