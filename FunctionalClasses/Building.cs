using MongoDB.Driver;
using Real_Estate_Managment_Software___GUI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Managment_Software___GUI.FunctionalClasses
{
    public class Building : Asset
    {

        public BuildingModel Model;
        public Building() : base()
        {
            Model = new BuildingModel();
        }
        public Building(BuildingModel model)
        {
            this.Model = model;
        }

        public static async Task<string> UpdateBuilding(BuildingModel record, string table)
        {
            MongoDBConnection db = new MongoDBConnection();
            var collection = db.db.GetCollection<BuildingModel>(table);
            var filter = Builders<BuildingModel>.Filter.Eq("Id", record.Id);
            var update = Builders<BuildingModel>.Update.Set("Area", record.Area).Set("Status", record.Status).Set("Address", record.Address).Set("orderID", record.orderID).Set("price", record.price).Set("Units", record.Units).Set("ImagesIds", record.ImagesIds);
            await collection.UpdateOneAsync(filter, update);
            return "Done";
        }

        public static async Task<List<BuildingModel>> getAllModels(BuildingModel record, string table)
        {
            MongoDBConnection db = new MongoDBConnection();
            var collection = db.db.GetCollection<BuildingModel>(table);
            var filter = Builders<BuildingModel>.Filter.Empty;
            if (record.Id != -1) filter &= Builders<BuildingModel>.Filter.Eq("Id", record.Id);
            if (record.Area != -1) filter &= Builders<BuildingModel>.Filter.Eq("Area", record.Area);
            if (record.Address != "") filter &= Builders<BuildingModel>.Filter.Eq("Address", record.Address);
            if (record.Status != "") filter &= Builders<BuildingModel>.Filter.Eq("Status", record.Status);
            if (record.price != -1) filter &= Builders<BuildingModel>.Filter.Eq("price", record.price);
            var ret = await collection.FindAsync<BuildingModel>(filter);
            return ret.ToList();
        }


    }
}
