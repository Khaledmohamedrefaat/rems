﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Managment_Software___GUI.DatabaseModels
{
    public class SIPair
    {
        /*
        [BsonId]
        public Guid Mongo_id;
        */
        public string Typ;
        public string Id;
        public SIPair(string typ, string id)
        {
            this.Typ = typ;
            this.Id = id;
        }
    }
    public abstract class AssetModel
    {
        [BsonId]
        public Guid Mongo_id { get; set; }
        public string Id { get; set; }
        public int Area { get; set; }
        public string Status { get; set; }
        public string City { get; set; }
        public string Governorate { get; set; }

        public string Street { get; set; }
        public List<SIPair> Units { get; set; }
        public List<int> ImagesIds { get; set; }

        public int price { get; set; }

        public int orderID { get; set; }

        public int numFloors { get; set; }
        public SIPair assetID { get; set; }
        public AssetModel()
        {
            Units = new List<SIPair>();
            ImagesIds = new List<int>();
        }
        public AssetModel(int area, string status, string address, List<SIPair> units, List<int> imagesIds)
        {
            this.Area = area;
            this.Status = status;
            this.Units = units;
            this.ImagesIds = imagesIds;
        }
    }
}
