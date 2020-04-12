using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Real_Estate_Management_Software.FunctionalClasses;

namespace Real_Estate_Management_Software
{
    public partial class ListItem : UserControl
    {
        Building building = new Building();
        public ListItem(Building building){
            InitializeComponent();
            this.building = building;
        }

        private void ListItem_Load(object sender, EventArgs e)
        {
            label6.Text = building.Model.Id.ToString();
            label7.Text = building.Model.Area.ToString();
            label8.Text = building.Model.Address;
            label9.Text = building.Model.Status;

        }
    }
}
