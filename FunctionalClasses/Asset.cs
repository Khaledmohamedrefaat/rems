using Real_Estate_Managment_Software___GUI.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Managment_Software___GUI.FunctionalClasses
{
    public abstract class Asset
    {
        public List<Apartment> Apartments;
        public List<Storage> Storages;
        public List<Store> Stores;

        public Asset()
        {
            Apartments = new List<Apartment>();
            Storages = new List<Storage>();
            Stores = new List<Store>();
        }
        public async Task<string> LoadAssetById(SIPair unit)
        {
            string table = unit.Typ;
            int id = unit.Id;
            if (table == "Buildings")
            {
                return "No need to Add Something";
                MongoDBConnection db = new MongoDBConnection();
                BuildingModel model = await db.LoadRecordById<BuildingModel>(table, id);
                Building ret = new Building(model);
            }
            else if (table == "Apartments")
            {
                MongoDBConnection db = new MongoDBConnection();
                ApartmentModel model = await db.LoadRecordById<ApartmentModel>(table, id);
                Apartment ret = new Apartment(model);
                this.Apartments.Add(ret);
                return "Loaded Apartment Successfully";
            }
            else if (table == "AgriculturalLands")
            {
                return "No need to Add Something";
                MongoDBConnection db = new MongoDBConnection();
                AgriculturalLandModel model = await db.LoadRecordById<AgriculturalLandModel>(table, id);
                AgriculturalLand ret = new AgriculturalLand(model);
            }
            else if (table == "Lands")
            {
                return "No need to Add Something";
                MongoDBConnection db = new MongoDBConnection();
                LandModel model = await db.LoadRecordById<LandModel>(table, id);
                Land ret = new Land(model);
            }
            else if (table == "Factories")
            {
                return "No need to Add Something";
                MongoDBConnection db = new MongoDBConnection();
                FactoryModel model = await db.LoadRecordById<FactoryModel>(table, id);
                Factory ret = new Factory(model);
            }
            else if (table == "Storages")
            {
                MongoDBConnection db = new MongoDBConnection();
                StorageModel model = await db.LoadRecordById<StorageModel>(table, id);
                Storage ret = new Storage(model);
                this.Storages.Add(ret);
                return "Loaded Storage Successfully";
            }
            else if (table == "Stores")
            {
                MongoDBConnection db = new MongoDBConnection();
                StoreModel model = await db.LoadRecordById<StoreModel>(table, id);
                Store ret = new Store(model);
                this.Stores.Add(ret);
                return "Loaded Store Successfully";
            }
            else if (table == "Villas")
            {
                return "No need to Add Something";
                MongoDBConnection db = new MongoDBConnection();
                VillaModel model = await db.LoadRecordById<VillaModel>(table, id);
                Villa ret = new Villa(model);
            }
            else
                return "Failed To Load Asset";
        }

        public async Task<string> LoadUnits(List<SIPair> unitList)
        {
            Apartments.Clear();
            Storages.Clear();
            Stores.Clear();
            foreach (SIPair unit in unitList)
            {
                await LoadAssetById(unit);
            }
            return "Done";
        }

        public static async Task<string> Sell<T>(Sold sellOrder, T assetObj, string table, int Id)
        {
            MongoDBConnection db = new MongoDBConnection();
            await db.InsertRecord<SoldModel>("Sold", sellOrder.Model);
            await db.UpdateRecord<T>(table, Id, assetObj, "Status", "Sold");
            await db.UpdateRecord<T>(table, Id, assetObj, "orderID", sellOrder.Model.Id);
            return "Done";
        }

        public static async Task<string> Rent<T>(Rental rentOrder, T assetObj, string table, int Id)
        {
            MongoDBConnection db = new MongoDBConnection();
            await db.InsertRecord<RentalModel>("Rentals", rentOrder.Model);
            await db.UpdateRecord<T>(table, Id, assetObj, "Status", "Rented");
            await db.UpdateRecord<T>(table, Id, assetObj, "orderID", rentOrder.Model.Id);
            return "Done";
        }

        public static async Task<string> getInstallment<T>(T assetObj, string table, int orderID)
        {
            MongoDBConnection db = new MongoDBConnection();
            SoldModel sellOrder = await db.LoadRecordById<SoldModel>("Sold", orderID);
            await db.UpdateRecord<SoldModel>("Sold", orderID, sellOrder, "AmountCollected", sellOrder.AmountCollected + sellOrder.InstallPrice);
            return "Done";
        }

        public static async Task<string> Delete<T>(T assetObj, string table, int id)
        {
            MongoDBConnection db = new MongoDBConnection();
            await db.DeleteRecord<T>(table, id);
            return "Done";
        }

        public static async Task<string> Insert<T>(T assetObj, string table)
        {
            MongoDBConnection db = new MongoDBConnection();
            await db.InsertRecord<T>(table, assetObj);
            return "Done";
        }

    }
}
