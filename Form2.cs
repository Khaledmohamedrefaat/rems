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
    public partial class Form2 : Form
    {
        List<int> photoSID = new List<int>();
        public Form2()
        {
            InitializeComponent();
        }

        private async void button13_Click(object sender, EventArgs e)
        {
            ApartmentModel model = new ApartmentModel();
            model.Area = Convert.ToInt32(textBox1.Text);
            model.Address = textBox2.Text;
            model.Status = "Available";
            model.ImagesIds = photoSID;
            Apartment obj = new Apartment(model);
            await Form1.addApart(obj);
            photoSID = new List<int>();
            Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label5.Text = "";
        }
    }
}
