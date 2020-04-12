using MongoDB.Driver;
using Real_Estate_Management_Software.ConnectionClasses;
using Real_Estate_Management_Software.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management_Software.FunctionalClasses
{
    public class Building : Asset{

        public BuildingModel Model;
        public Building() : base()
        {
            Model = new BuildingModel();
        }
        public Building(BuildingModel model)
        {
            this.Model = model;
        } 

        public static async Task<string> UpdateBuilding(BuildingModel record){
            MongoDBConnection db = new MongoDBConnection();
            var collection = db.db.GetCollection<BuildingModel>("Buildings");
            var filter = Builders<BuildingModel>.Filter.Eq("Id", record.Id);
            var update = Builders<BuildingModel>.Update.Set("Area", record.Area).Set("Status", record.Status).Set("Address", record.Address).Set("orderID", record.orderID);
            await collection.UpdateOneAsync(filter, update);
            return "Done";
        }

        public static async Task<List<BuildingModel>> getAllModels(BuildingModel record){
            MongoDBConnection db = new MongoDBConnection();
            var collection = db.db.GetCollection<BuildingModel>("Buildings");
            var filter = Builders<BuildingModel>.Filter.Empty;
            if (record.Id != -1) filter &= Builders<BuildingModel>.Filter.Eq("Id", record.Id);
            if (record.Area != -1) filter &= Builders<BuildingModel>.Filter.Eq("Area", record.Area);
            if (record.Address != "") filter &= Builders<BuildingModel>.Filter.Eq("Address", record.Address);
            if (record.Status != "") filter &= Builders<BuildingModel>.Filter.Eq("Status", record.Status);
            var ret = await collection.FindAsync<BuildingModel>(filter);
            return ret.ToList();
        }

        public void readFromUser(){
            Console.Write("Enter the Area :"); this.Model.Area = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the Address :"); this.Model.Address = Console.ReadLine();
            this.Model.Status = "Available";
            this.Model.orderID = -1;
            this.Model.Units = new List<SIPair>();
            this.Model.ImagesIds = new List<int>();
        }

    }
}
