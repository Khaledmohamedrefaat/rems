using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Managment_Software___GUI.DatabaseModels
{
    public class Seq
    {
        [BsonId]
        public Guid _id { get; set; }
        public int display_id { get; set; }
    }
    public class CodeElement
    {
        [BsonId]
        public Guid _id { get; set; }
        public string code { get; set; }
        public int nextVal { get; set; }
    }
    public class MongoDBConnection
    {
        public IMongoDatabase db;
        public MongoDBConnection(string database = "remsdb")
        {
            var client = new MongoClient("mongodb+srv://reesmaso:r2e0m2s0@cluster0-h6uge.mongodb.net/test?retryWrites=true&w=majority");
            db = client.GetDatabase(database);
        }

        public async Task<string> InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            await collection.InsertOneAsync(record);
            return "Inserted Successfully";
        }

        public async Task<List<T>> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            var result = await collection.FindAsync(new BsonDocument());
            return result.ToList();
        }

        public async Task<T> LoadRecordById<T>(string table, string id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = await collection.FindAsync(filter);
            return result.First();
        }

        public async Task<T> LoadRecordById<T>(string table, int id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = await collection.FindAsync(filter);
            return result.First();
        }

        public async Task<string> UpdateRecord<T>(string table, string id, T record)
        {
            await DeleteRecord<T>(table, id);
            await InsertRecord<T>(table, record);
            return "Updated Successfully";
        }
        public async Task<string> UpdateRecord<T>(string table, int id, T record)
        {
            await DeleteRecord<T>(table, id);
            await InsertRecord<T>(table, record);
            return "Updated Successfully";
        }

        public async Task<string> UpdateRecord<T>(string table, string id, T _, string field, string value)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            var update = Builders<T>.Update.Set(field, value);
            await collection.UpdateOneAsync(filter, update);
            return "Updated Successfully";
        }
        public async Task<string> UpdateRecord<T>(string table, string id, T _, string field, int value)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            var update = Builders<T>.Update.Set(field, value);
            await collection.UpdateOneAsync(filter, update);
            return "Updated Successfully";
        }
        public async Task<string> UpdateRecord<T>(string table, Guid id, T _, string field, int value)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", id);
            var update = Builders<T>.Update.Set(field, value);
            await collection.UpdateOneAsync(filter, update);
            return "Updated Successfully";
        }


        public async Task<string> DeleteRecord<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("_id", id);

            await collection.DeleteOneAsync(filter);
            return "Deleted Successfully";
        }

        public async Task<string> DeleteRecord<T>(string table, int id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            await collection.DeleteOneAsync(filter);
            return "Deleted Successfully";
        }
        public async Task<string> DeleteRecord<T>(string table, string id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            await collection.DeleteOneAsync(filter);
            return "Deleted Successfully";
        }

        public async Task<long> GetCollectionSize<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            var ret = await collection.CountDocumentsAsync(new BsonDocument());
            return ret;
        }

        public async Task<int> GetNextSeqVal(string sequenceName)
        {
            var recs = await LoadRecords<Seq>("Sequences");
            var next = recs[0].display_id;
            await UpdateRecord<Seq>("Sequences", recs[0]._id, new Seq { _id = recs[0]._id, display_id = next + 1 }, "display_id", next + 1);
            return next;
        }

        public async Task<int> getNextCodeIncrement(string code)
        {
            var collection = db.GetCollection<CodeElement>("Codes");
            var filter = Builders<CodeElement>.Filter.Eq("code", code);
            var result = await collection.FindAsync(filter);
            List<CodeElement> codes = result.ToList();
            if(codes.Count == 0)
            {
                CodeElement new_code = new CodeElement();
                new_code.code = code;
                new_code.nextVal = 1;
                await InsertRecord<CodeElement>("Codes", new_code);
                return 1;
            }
            else
            {
                var nextID = codes[0].nextVal;
                await collection.DeleteOneAsync(filter);
                CodeElement new_code = new CodeElement();
                new_code.code = code;
                new_code.nextVal = nextID + 1;
                await InsertRecord<CodeElement>("Codes", new_code);
                return nextID;
            }
        }
    }
}
