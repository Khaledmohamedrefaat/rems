using Real_Estate_Management_Software.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management_Software.FunctionalClasses
{
    public class Land : Asset
    {

        public Land() : base()
        {

        }
        public Land(LandModel model) : base(model)
        {
        }
    }
}
