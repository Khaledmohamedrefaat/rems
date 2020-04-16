using MongoDB.Driver;
using Real_Estate_Managment_Software___GUI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Managment_Software___GUI.FunctionalClasses
{
    public class Apartment : Asset
    {
        public ApartmentModel Model;
        public Apartment() : base()
        {
            Model = new ApartmentModel();
        }
        public Apartment(ApartmentModel model)
        {
            this.Model = model;
        }
        public void readFromUser()
        {
            Console.Write("Enter the Area :"); this.Model.Area = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter the Address :"); this.Model.Address = Console.ReadLine();
            this.Model.Status = "Available";
            this.Model.orderID = -1;
        }
        public static async Task<List<ApartmentModel>> getAllModels(ApartmentModel record, string table)
        {
            MongoDBConnection db = new MongoDBConnection();
            var collection = db.db.GetCollection<ApartmentModel>(table);
            var filter = Builders<ApartmentModel>.Filter.Empty;
            if (record.Id != -1) filter &= Builders<ApartmentModel>.Filter.Eq("Id", record.Id);
            if (record.Area != -1) filter &= Builders<ApartmentModel>.Filter.Eq("Area", record.Area);
            if (record.Address != "") filter &= Builders<ApartmentModel>.Filter.Eq("Address", record.Address);
            if (record.Status != "") filter &= Builders<ApartmentModel>.Filter.Eq("Status", record.Status);
            if (record.price != -1) filter &= Builders<ApartmentModel>.Filter.Eq("price", record.price);
            var ret = await collection.FindAsync<ApartmentModel>(filter);
            return ret.ToList();
        }

    }
}
