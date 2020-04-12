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
    public partial class viewApartsForm : Form
    {
        Building building = new Building();
        int index = 0;
        public viewApartsForm(Building building)
        {
            InitializeComponent();
            this.building = building;
            index = 0;
            button9.Visible = false;
        }

        private async void viewApartsForm_Load(object sender, EventArgs e)
        {
            await building.LoadUnits(building.Model.Units);
            DisplayApart();
        }

        private void DisplayApart(){
            Apartment curr = new Apartment();
            curr = building.Apartments[index];
            textBox8.Text = curr.Model.Id.ToString();
            textBox7.Text = curr.Model.Area.ToString();
            textBox9.Text = curr.Model.Address;
            textBox12.Text = curr.Model.Status;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            index--;
            DisplayApart();
            if (index == 0)
                button9.Visible = false;
            if (index < building.Apartments.Count - 1)
                button1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            index++;
            DisplayApart();
            if (index == building.Apartments.Count - 1)
                button1.Visible = false;
            if (index > 0)
                button9.Visible = true;
        }
    }
}
