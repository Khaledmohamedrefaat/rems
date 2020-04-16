using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Managment_Software___GUI.DatabaseModels
{
    public class AgriculturalLandModel : AssetModel
    {
        public AgriculturalLandModel() : base()
        {

        }
        public AgriculturalLandModel(int area, string status, string address, List<SIPair> units, List<int> imagesIds) : base(area, status, address, units, imagesIds)
        {
        }
    }
}
