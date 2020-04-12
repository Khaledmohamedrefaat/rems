using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management_Software.DatabaseModels
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

    }
}
