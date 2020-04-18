using MongoDB.Driver;
using Real_Estate_Managment_Software___GUI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Managment_Software___GUI.FunctionalClasses
{
    public class Villa : Asset
    {
        public VillaModel Model;
        public Villa() : base()
        {

        }
        public Villa(VillaModel model)
        {
            this.Model = model;
        }

        public static async Task<List<VillaModel>> getAllModels(VillaModel record)
        {
            MongoDBConnection db = new MongoDBConnection();
            var collection = db.db.GetCollection<VillaModel>("Villas");
            var filter = Builders<VillaModel>.Filter.Empty;
            if (record.Id != "") filter &= Builders<VillaModel>.Filter.Eq("Id", record.Id);
            if (record.Area != -1) filter &= Builders<VillaModel>.Filter.Eq("Area", record.Area);
            if (record.City != "") filter &= Builders<VillaModel>.Filter.Eq("City", record.City);
            if (record.Governorate != "") filter &= Builders<VillaModel>.Filter.Eq("Governorate", record.Governorate);
            if (record.Street != "") filter &= Builders<VillaModel>.Filter.Eq("Address", record.Street);
            if (record.Status != "") filter &= Builders<VillaModel>.Filter.Eq("Status", record.Status);
            if (record.price != -1) filter &= Builders<VillaModel>.Filter.Eq("price", record.price);
            var ret = await collection.FindAsync<VillaModel>(filter);
            return ret.ToList();
        }
    }
}
