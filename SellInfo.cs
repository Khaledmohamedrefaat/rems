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
    public partial class SellInfo : Form
    {
        SoldModel model;
        public static SoldModel updModel;
        bool isUpdate;
        public string from;
        public SellInfo(SoldModel model, string from)
        {
            InitializeComponent();
            isUpdate = false;
            this.model = model;
            updModel = model;
            this.from = from;
            LoadInfo();
        }

        public void LoadInfo(){
            lbl_Desposit.Text = tbx_Insurance.Text = model.InsallDeposit.ToString();
            lbl_Duration.Text = tbx_Duration.Text = model.Duration.ToString();
            lbl_Name.Text = tbx_Name.Text = model.Name;
            lbl_NationalID.Text = tbx_NationalID.Text =  model.NationalID;
            lbl_Price.Text = tbx_Price.Text = model.InstallPrice.ToString();
            lbl_Start.Text = model.TransactionTime.ToString();
            lbl_TotalCash.Text = tbx_TotalCash.Text = model.AmountCollected.ToString();
            dateTimePicker1.Value = model.TransactionTime;
            richTextBox1.Text = model.Notes;
            lbl_End.Text = model.TransactionTime.ToString();
            label10.Text = model.AssetId.Typ + " - " + model.AssetId.Id.ToString();
            int diff = ((dateTimePicker2.Value.Year - dateTimePicker1.Value.Year) * 12) + dateTimePicker2.Value.Month - dateTimePicker1.Value.Month;
            tbx_Duration.Text = diff.ToString();
        }
        Thread ldbx = new Thread(new ThreadStart(Loading));
        public static void Loading()
        {
            Application.Run(new LoadingBox());
        }
        private async void button12_Click(object sender, EventArgs e)
        {
            try { 
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter The Amount Collected", "Input", "");
            if (!Validator.IsPrice(input))
                return;
            updModel = model;
            updModel.AmountCollected += Convert.ToInt32(input);
            MongoDBConnection db = new MongoDBConnection();
            ldbx.Start();
            await db.UpdateRecord("Sold", updModel.Id, updModel);
            ldbx.Abort();
            model = updModel;
            LoadInfo();
            MessageBox.Show("Updated Successfully");
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }

        private void SellInfo_Load(object sender, EventArgs e)
        {
            tbx_Duration.Visible = tbx_Insurance.Visible = tbx_Name.Visible = tbx_NationalID.Visible = tbx_Price.Visible = tbx_TotalCash.Visible = false;
            dateTimePicker1.Visible = dateTimePicker2.Visible = false;
            richTextBox1.ReadOnly = true;
            lbl_Desposit.Visible = lbl_Duration.Visible = lbl_End.Visible = lbl_Name.Visible = lbl_NationalID.Visible = lbl_Price.Visible = lbl_Start.Visible = lbl_TotalCash.Visible = true;
        }
        public bool CheckFilters()
        {
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
        private async void button9_Click(object sender, EventArgs e)
        {
            try { 
            if (!isUpdate){
                isUpdate = true;
                tbx_Duration.Visible = tbx_Insurance.Visible = tbx_Name.Visible = tbx_NationalID.Visible = tbx_Price.Visible = tbx_TotalCash.Visible = true;
                dateTimePicker1.Visible = dateTimePicker2.Visible = true;
                richTextBox1.ReadOnly = false;
                lbl_Desposit.Visible = lbl_Duration.Visible = lbl_End.Visible = lbl_Name.Visible = lbl_NationalID.Visible = lbl_Price.Visible = lbl_Start.Visible = lbl_TotalCash.Visible = false;
            }
            else{
                if (!CheckFilters())
                    return;
                isUpdate = false;
                tbx_Duration.Visible = tbx_Insurance.Visible = tbx_Name.Visible = tbx_NationalID.Visible = tbx_Price.Visible = tbx_TotalCash.Visible = false;
                dateTimePicker1.Visible = dateTimePicker2.Visible = false;
                richTextBox1.ReadOnly = true;
                lbl_Desposit.Visible = lbl_Duration.Visible = lbl_End.Visible = lbl_Name.Visible = lbl_NationalID.Visible = lbl_Price.Visible = lbl_Start.Visible = lbl_TotalCash.Visible = true;
                updModel.Name = tbx_Name.Text;
                updModel.NationalID = tbx_NationalID.Text;
                updModel.InstallPrice = Convert.ToInt32(tbx_Price.Text);
                updModel.InsallDeposit = Convert.ToInt32(tbx_Insurance.Text);
                updModel.AmountCollected = Convert.ToInt32(tbx_TotalCash.Text);
                updModel.TransactionTime = dateTimePicker1.Value;
                //updModel.InstallEnd = dateTimePicker.Value;
                updModel.Duration = ((dateTimePicker2.Value.Year - dateTimePicker1.Value.Year) * 12) + dateTimePicker2.Value.Month - dateTimePicker1.Value.Month;
                updModel.Notes = richTextBox1.Text;
                MongoDBConnection db = new MongoDBConnection();
                ldbx.Start();
                await db.UpdateRecord("Sold", updModel.Id, updModel);
                ldbx.Abort();
                model = updModel;
                LoadInfo();
                MessageBox.Show("Updated Successfully");
            }
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            updModel.Duration = ((dateTimePicker2.Value.Year - dateTimePicker1.Value.Year) * 12) + dateTimePicker2.Value.Month - dateTimePicker1.Value.Month;
            lbl_Duration.Text = tbx_Duration.Text = updModel.Duration.ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int diff = ((dateTimePicker2.Value.Year - dateTimePicker1.Value.Year) * 12) + dateTimePicker2.Value.Month - dateTimePicker1.Value.Month;
            tbx_Duration.Text = diff.ToString();
        }
    }
}
