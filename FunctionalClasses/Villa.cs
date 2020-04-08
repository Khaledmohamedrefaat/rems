using Real_Estate_Management_Software.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management_Software.FunctionalClasses
{
    public class Villa : Asset
    {

        public Villa() : base()
        {

        }
        public Villa(VillaModel model) : base(model)
        {
        }
    }
}
