using Real_Estate_Managment_Software___GUI.DatabaseModels;
using Real_Estate_Managment_Software___GUI.FunctionalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Real_Estate_Managment_Software___GUI
{
    public partial class InfoRecord : Form
    {
        
        Building building;
        public static Building updBuilding;
        bool inUpdate;
        public static Label unitsLabel, imgsLabel;
        public string table;
        public void Freeze()
        {
            tableLayoutPanel1.Enabled = false;
        }
        public void UnFreeze()
        {
            tableLayoutPanel1.Enabled = true;
        }
        public InfoRecord(Building building, string table)
        {
            InitializeComponent();
            this.building = building;
            button2.Visible = button3.Visible = false;
            textBox1.Visible = textBox2.Visible = textBox3.Visible = textBox4.Visible = textBox5.Visible = textBox6.Visible = false;
            inUpdate = false;
            unitsLabel = lbl_UnitsID;
            imgsLabel = lbl_ImagesID;
            this.table = table;
            unitsLabel.Text = imgsLabel.Text = "";
        }

        private async void AddRecord_Load(object sender, EventArgs e)
        {
            try
            {
                this.CenterToParent();
                Freeze();
                await LoadInfo();
                UnFreeze();

            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }
        public async Task<string> LoadInfo(){
            MongoDBConnection db = new MongoDBConnection();
            building.Model = await db.LoadRecordById<BuildingModel>(table, building.Model.Id);
            await building.LoadUnits(building.Model.Units);
            lbl_ID.Text = building.Model.Id.ToString();
            lbl_Area.Text = building.Model.Area.ToString();
            lbl_Address.Text = building.Model.City;
            label10.Text = building.Model.Governorate;
            lbl_Price.Text = building.Model.Street;
            lbl_Status.Text = building.Model.Status;
            label6.Text = building.Model.orderID.ToString();

            textBox1.Text = building.Model.Id.ToString();
            textBox2.Text = building.Model.Area.ToString();
            textBox3.Text = building.Model.City;
            textBox4.Text = building.Model.Governorate;
            textBox6.Text = building.Model.Street;
            textBox5.Text = building.Model.Status;
            unitsLabel.Text = imgsLabel.Text = "";
            foreach (SIPair sIPair in building.Model.Units)
            {
                if (unitsLabel.Text != "")
                    unitsLabel.Text += ", ";
                unitsLabel.Text += sIPair.Id.ToString();
            }
            foreach (int id in building.Model.ImagesIds)
            {
                if (imgsLabel.Text != "")
                    imgsLabel.Text += ", ";
                imgsLabel.Text += id.ToString();
            }
            /*
            if (building.Model.Status == "Available")
            {
                button4.Visible = false;
                button12.Visible = button13.Visible = true;
            }
            else
            {
                button4.Visible = true;
                button12.Visible = button13.Visible = false;
            }
            */
            return "Done";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnl_HorizontalLine1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbl_Status_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Price_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Address_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Area_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void lbl_ID_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void button8_Click(object sender, EventArgs e)
        {
            if (building.Model.Units.Count == 0)
            {
                MessageBox.Show("This Unit Has No Nested Units");
                return;
            }
            //await building.LoadUnits(building.Model.Units);
            using (ApartInfo apartInfo = new ApartInfo(building))
                apartInfo.ShowDialog();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try { 
            if (building.Model.ImagesIds.Count == 0)
            {
                MessageBox.Show("This Unit Has No Images.");
                return;
            }
            MongoDBConnection db = new MongoDBConnection();
            List<string> images = new List<string>();
                Freeze();
                foreach (int id in building.Model.ImagesIds)
            {
                var ret = await db.LoadRecordById<ImageModel>("Images", id);
                images.Add(ret.Content);
            }
                UnFreeze();
                using (viewPhoto x = new viewPhoto(images))
                x.ShowDialog();
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            try { 
            if (inUpdate == false){
                inUpdate = true;
                button2.Visible = button3.Visible = true;
                textBox1.Visible = textBox2.Visible = textBox3.Visible = textBox4.Visible = textBox5.Visible = textBox6.Visible = true;
                lbl_Address.Visible = lbl_ID.Visible = lbl_Area.Visible = lbl_Price.Visible = lbl_Status.Visible = false;
                button12.Visible = button13.Visible = button8.Visible = button1.Visible = false;
                updBuilding = new Building(building.Model);
            }
            else{
                if (!CheckFilterInput())
                    return;
                inUpdate = false;
                button2.Visible = button3.Visible = false;
                textBox1.Visible = textBox2.Visible = textBox3.Visible = textBox4.Visible = textBox5.Visible = textBox6.Visible = false;
                lbl_Address.Visible = lbl_ID.Visible = lbl_Area.Visible = lbl_Price.Visible = lbl_Status.Visible = true;
                button8.Visible = button1.Visible = true;
                updBuilding.Model.Id = textBox1.Text;
                updBuilding.Model.Area = Convert.ToInt32(textBox2.Text);
                updBuilding.Model.City = textBox3.Text;
                    updBuilding.Model.Governorate = textBox4.Text;
                    updBuilding.Model.Street = textBox4.Text;
                    updBuilding.Model.Status = textBox5.Text;
                    Freeze();
                    await Building.UpdateBuilding(updBuilding.Model, table);
                await LoadInfo();
                    UnFreeze();
                    MessageBox.Show("Updated Building Successfully");
            }
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }

        public bool CheckFilterInput()
        {
            if (!Validator.IsStatus(textBox5.Text))
                return false;
            if (!Validator.IsArea(textBox2.Text))
                return false;
            if (!Validator.IsPrice(textBox4.Text))
                return false;
            if (!Validator.IsAddress(textBox3.Text))
                return false;
            return true;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            try { 
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            string base64String = "";
            if (open.ShowDialog() == DialogResult.OK)
            {
                using (Image image = Image.FromFile(open.FileName))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        base64String = Convert.ToBase64String(imageBytes);
                            Freeze();
                            var chk = await ImageModel.getAllModels(new ImageModel { Content = base64String });
                            UnFreeze();
                            if (chk.Count > 0)
                        {
                            if ((MessageBox.Show("Unit is Duplicate, Want To add old one instead ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                            {
                                updBuilding.Model.ImagesIds.Add(chk[0].Id);
                                if (imgsLabel.Text != "")
                                    imgsLabel.Text += ", ";
                                imgsLabel.Text += updBuilding.Model.ImagesIds.Last().ToString();
                            }
                            return;
                        }
                    }
                }
                ImageModel img = new ImageModel();
                img.Content = base64String;
                MongoDBConnection db = new MongoDBConnection();
                    Freeze();
                    var nextId = await db.GetNextSeqVal("Images");
                img.Id = nextId;
                await db.InsertRecord<ImageModel>("Images", img);
                    UnFreeze();
                    updBuilding.Model.ImagesIds.Add(img.Id);
                if (imgsLabel.Text != "")
                    imgsLabel.Text += ", ";
                imgsLabel.Text += nextId.ToString();
                MessageBox.Show("Added Image Successfully");
            }
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }

        private async void button12_Click(object sender, EventArgs e)
        {
            try { 
            using (SellRecord sellRecord = new SellRecord(building, "Building"))
                sellRecord.ShowDialog();
                Freeze();
                await LoadInfo();
                UnFreeze();
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }

        private async void button13_Click(object sender, EventArgs e)
        {
            try { 
            using (RentRecord rentRecord = new RentRecord(building, "Buildings"))
                rentRecord.ShowDialog();
                Freeze();
                await LoadInfo();
                UnFreeze();
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try { 
            MongoDBConnection db = new MongoDBConnection();
            if(this.building.Model.Status == "Sold"){
                    Freeze();
                    SoldModel model = await db.LoadRecordById<SoldModel>("Sold", this.building.Model.orderID);
                    UnFreeze();
                    using (SellInfo sellInfo = new SellInfo(model, "Buildings"))
                    sellInfo.ShowDialog();
            }
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (AddRecord addRecord = new AddRecord(false, "Update", new SIPair(table, building.Model.Id), table))
                addRecord.ShowDialog();
        }
    }
}
