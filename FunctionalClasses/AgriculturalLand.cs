using Real_Estate_Management_Software.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management_Software.FunctionalClasses
{
    public class AgriculturalLand : Asset
    {
        public AgriculturalLandModel Model;
        public AgriculturalLand() : base()
        {

        }
        public AgriculturalLand(AgriculturalLandModel model)
        {
            this.Model = model;
        }
    }
}
