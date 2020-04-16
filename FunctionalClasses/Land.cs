using MongoDB.Driver;
using Real_Estate_Managment_Software___GUI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Managment_Software___GUI.FunctionalClasses
{
    public class Land : Asset
    {
        public LandModel Model;
        public Land() : base()
        {

        }
        public Land(LandModel model)
        {
            this.Model = model;
        }

        public static async Task<List<LandModel>> getAllModels(LandModel record)
        {
            MongoDBConnection db = new MongoDBConnection();
            var collection = db.db.GetCollection<LandModel>("Lands");
            var filter = Builders<LandModel>.Filter.Empty;
            if (record.Id != -1) filter &= Builders<LandModel>.Filter.Eq("Id", record.Id);
            if (record.Area != -1) filter &= Builders<LandModel>.Filter.Eq("Area", record.Area);
            if (record.Address != "") filter &= Builders<LandModel>.Filter.Eq("Address", record.Address);
            if (record.Status != "") filter &= Builders<LandModel>.Filter.Eq("Status", record.Status);
            if (record.price != -1) filter &= Builders<LandModel>.Filter.Eq("price", record.price);
            var ret = await collection.FindAsync<LandModel>(filter);
            return ret.ToList();
        }
    }
}
