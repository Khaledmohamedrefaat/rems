using Real_Estate_Management_Software.ConnectionClasses;
using Real_Estate_Management_Software.DatabaseModels;
using Real_Estate_Management_Software.FunctionalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Real_Estate_Management_Software
{
    public partial class viewAllForm : Form
    {
        public viewAllForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            BuildingModel searchModel = new BuildingModel();
            if (textBox1.Text != "")
                searchModel.Id = Convert.ToInt32(textBox1.Text);
            else
                searchModel.Id = -1;
            if (textBox2.Text != "")
                searchModel.Area = Convert.ToInt32(textBox2.Text);
            else
                searchModel.Area = -1;
            searchModel.Address = textBox3.Text;
            searchModel.Status = textBox4.Text;
            var retList = await Building.getAllModels(searchModel);
            panel3.Controls.Clear();
            foreach(BuildingModel model in retList){
                Building b = new Building(model);
                ListItem c = new ListItem(b);
                c.Dock = DockStyle.Top;
                panel3.Controls.Add(c);
            }
        }
    }
}
