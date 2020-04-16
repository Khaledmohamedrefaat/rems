using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Managment_Software___GUI.DatabaseModels
{
    public class ImageModel
    {
        [BsonId]
        public Guid Mongo_id { get; set; }
        public int Id { get; set; }
        public string Content { get; set; }
        public ImageModel()
        {

        }
        public static async Task<List<ImageModel>> getAllModels(ImageModel record)
        {
            MongoDBConnection db = new MongoDBConnection();
            var collection = db.db.GetCollection<ImageModel>("Images");
            var filter = Builders<ImageModel>.Filter.Empty;
            filter &= Builders<ImageModel>.Filter.Eq("Content", record.Content);
            var ret = await collection.FindAsync<ImageModel>(filter);
            return ret.ToList();
        }

    }

}
