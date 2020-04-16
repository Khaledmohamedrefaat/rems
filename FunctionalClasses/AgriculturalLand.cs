using MongoDB.Driver;
using Real_Estate_Managment_Software___GUI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Managment_Software___GUI.FunctionalClasses
{
    public class AgriculturalLand : Asset
    {
        public AgriculturalLandModel Model;
        public AgriculturalLand() : base()
        {

        }
        public AgriculturalLand(AgriculturalLandModel model)
        {
            this.Model = model;
        }

        public static async Task<List<AgriculturalLandModel>> getAllModels(AgriculturalLandModel record)
        {
            MongoDBConnection db = new MongoDBConnection();
            var collection = db.db.GetCollection<AgriculturalLandModel>("AgriculturalLands");
            var filter = Builders<AgriculturalLandModel>.Filter.Empty;
            if (record.Id != -1) filter &= Builders<AgriculturalLandModel>.Filter.Eq("Id", record.Id);
            if (record.Area != -1) filter &= Builders<AgriculturalLandModel>.Filter.Eq("Area", record.Area);
            if (record.Address != "") filter &= Builders<AgriculturalLandModel>.Filter.Eq("Address", record.Address);
            if (record.Status != "") filter &= Builders<AgriculturalLandModel>.Filter.Eq("Status", record.Status);
            if (record.price != -1) filter &= Builders<AgriculturalLandModel>.Filter.Eq("price", record.price);
            var ret = await collection.FindAsync<AgriculturalLandModel>(filter);
            return ret.ToList();
        }
    }
}
