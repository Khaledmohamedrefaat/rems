﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management_Software.DatabaseModels
{
    public class StoreModel : AssetModel
    {
        public int Price;

        public StoreModel() : base()
        {

        }
        public StoreModel(int area, string status, string address, List<SIPair> units, List<int> imagesIds, int price) : base(area, status, address, units, imagesIds)
        {
            this.Price = price;
        }
    }
}
