using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management_Software.DatabaseModels
{
    public class VillaModel : AssetModel
    {
        public bool HasGarden;
        
        public VillaModel() : base()
        {

        }
        
        public VillaModel(int area, string status, string address, List<SIPair> units, List<int> imagesIds, bool hasGarden) : base(area, status, address, units, imagesIds)
        {
            this.HasGarden = hasGarden;
        }
    }
}
