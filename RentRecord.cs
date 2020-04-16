using Real_Estate_Managment_Software___GUI.DatabaseModels;
using Real_Estate_Managment_Software___GUI.FunctionalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Real_Estate_Managment_Software___GUI
{
    public partial class RentRecord : Form
    {
        Building building;
        public string from;
        public RentRecord(Building building, string from)
        {
            InitializeComponent();
            this.building = building;
            this.from = from;
        }
        public bool checkFilter(){
            if (!Validator.IsName(tbx_Name.Text))
                return false;
            if (!Validator.IsNationalID(tbx_NationalID.Text))
                return false;
            if (!Validator.IsPrice(tbx_Price.Text))
                return false;
            if (!Validator.IsPrice(tbx_Insurance.Text))
                return false;
            if (!Validator.IsPrice(tbx_TotalCash.Text))
                return false;
            if (!Validator.IsPrice(tbx_Duration.Text))
                return false;
            return true;
        }
        Thread ldbx = new Thread(new ThreadStart(Loading));
        public static void Loading()
        {
            Application.Run(new LoadingBox());
        }
        private async void button12_Click(object sender, EventArgs e){
            try { 
            if (!checkFilter())
                return;
            RentalModel model = new RentalModel();
            MongoDBConnection db = new MongoDBConnection();
            ldbx.Start();
            var nextId = await db.GetNextSeqVal("Buildings");
            ldbx.Abort();
            model.Id = nextId;
            model.AssetId = new SIPair("Buildings", building.Model.Id);
            model.AmountCollected = Convert.ToInt32(tbx_TotalCash.Text);
            model.Name = tbx_Name.Text;
            model.NationalID = tbx_NationalID.Text;
            model.Price = Convert.ToInt32(tbx_Price.Text);
            int diff = ((dateTimePicker2.Value.Year - dateTimePicker1.Value.Year) * 12) + dateTimePicker2.Value.Month - dateTimePicker1.Value.Month;
            model.Duration = diff;
            model.Insurance = Convert.ToInt32(tbx_Insurance.Text);
            model.TransactionTime = dateTimePicker1.Value;
            model.End = dateTimePicker2.Value;
            model.Notes = richTextBox1.Text;
            if (from == "Buildings")
            {
            ldbx.Start();
                model.AssetId = new SIPair("Buildings", building.Model.Id);
                await Asset.Rent(new Rental(model), building.Model, from, building.Model.Id);
                string bx = from.Remove(from.Length - 1);
                ldbx.Abort();
                MessageBox.Show("Rented " + bx + " Successfully");
            }
            else if (from == "Apartments")
            {
                ldbx.Start();
                model.AssetId = new SIPair("Apartments", building.Apartments[0].Model.Id);
                await Asset.Rent(new Rental(model), building.Apartments[0].Model, from, building.Apartments[0].Model.Id);
                string bx = from.Remove(from.Length - 1);
                ldbx.Abort();
                MessageBox.Show("Rented " + bx + " Successfully");
            }
            else if (from == "Storages")
            {
                ldbx.Start();
                model.AssetId = new SIPair("Storages", building.Storages[0].Model.Id);
                await Asset.Rent(new Rental(model), building.Storages[0].Model, from, building.Storages[0].Model.Id);
                string bx = from.Remove(from.Length - 1);
                ldbx.Abort(); 
                MessageBox.Show("Rented " + bx + " Successfully");
            }
            else if (from == "Stores")
            {
                ldbx.Start();
                model.AssetId = new SIPair("Stores", building.Stores[0].Model.Id);
                await Asset.Rent(new Rental(model), building.Stores[0].Model, from, building.Stores[0].Model.Id);
                string bx = from.Remove(from.Length - 1);
                ldbx.Abort();
                MessageBox.Show("Rented " + bx + " Successfully");
            }
            Close();
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            int diff = ((dateTimePicker2.Value.Year - dateTimePicker1.Value.Year) * 12) + dateTimePicker2.Value.Month - dateTimePicker1.Value.Month;
            tbx_Duration.Text = diff.ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int diff = ((dateTimePicker2.Value.Year - dateTimePicker1.Value.Year) * 12) + dateTimePicker2.Value.Month - dateTimePicker1.Value.Month;
            tbx_Duration.Text = diff.ToString();
        }
    }
}
