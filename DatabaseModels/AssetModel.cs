using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management_Software.DatabaseModels
{
    public class SIPair
    {
        public string Typ;
        public int Id;
        public SIPair(string typ, int id)
        {
            this.Typ = typ;
            this.Id = id;
        }
    }
    public abstract class AssetModel
    {
        [BsonId]
        public Guid Mongo_id { get; set; }
        public int Id { get; set; }
        public int Area { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public List<SIPair> Units { get; set; }
        public List<int> ImagesIds { get; set; }

        public AssetModel(){
        }
        public AssetModel(int area, string status, string address, List<SIPair> units, List<int> imagesIds)
        {
            this.Area = area;
            this.Status = status;
            this.Address = address;
            this.Units = units;
            this.ImagesIds = imagesIds;
        }
    }
}
