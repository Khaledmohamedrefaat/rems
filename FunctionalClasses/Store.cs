﻿using Real_Estate_Management_Software.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management_Software.FunctionalClasses
{
    public class Store : Asset
    {
        public StoreModel Model;
        public Store() : base()
        {
            Model = new StoreModel();
        }
        public Store(StoreModel model)
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
    }
}
