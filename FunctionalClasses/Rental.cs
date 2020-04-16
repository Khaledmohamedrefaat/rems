using MongoDB.Driver;
using Real_Estate_Managment_Software___GUI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Managment_Software___GUI.FunctionalClasses
{
    public class Rental : Transaction
    {
        public RentalModel Model;
        public Rental() : base()
        {

        }
        public Rental(RentalModel model)
        {
            this.Model = model;
        }
        public static async Task<List<RentalModel>> getAllModels(RentalModel record)
        {
            MongoDBConnection db = new MongoDBConnection();
            var collection = db.db.GetCollection<RentalModel>("Rentals");
            var filter = Builders<RentalModel>.Filter.Empty;
            if (record.Name != "") filter &= Builders<RentalModel>.Filter.Eq("Name", record.Name);
            if (record.AssetId != null) filter &= Builders<RentalModel>.Filter.Eq("AssetId", record.AssetId);
            var ret = await collection.FindAsync<RentalModel>(filter);
            return ret.ToList();
        }
    }
}
