using Real_Estate_Management_Software.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management_Software.FunctionalClasses
{
    public class Building : Asset{

        public Building() : base()
        {

        }
        public Building(BuildingModel model) : base(model){
        }

    }
}
