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
    public partial class ApartInfo : Form
    {
        Building building;
        int cnt, index;
        List<int> imagesIds = new List<int>();
        bool inUpdate;
        AssetModel updModel;
        Thread ldbx = new Thread(new ThreadStart(Loading));
        public static void Loading()
        {
            Application.Run(new LoadingBox());
        }
        public ApartInfo(Building building)
        {
            InitializeComponent();
            this.building = building;
            cnt = building.Apartments.Count + building.Storages.Count + building.Stores.Count;
            index = 0;
            inUpdate = false;
            textBox1.Visible = textBox2.Visible = textBox3.Visible = textBox4.Visible = textBox5.Visible = button2.Visible = false;
            lbl_Address.Visible = lbl_ID.Visible = lbl_Area.Visible = lbl_Price.Visible = lbl_Status.Visible = true;
            button12.Visible = button13.Visible = button1.Visible = true;
        }

        private void ApartInfo_Load(object sender, EventArgs e)
        {
            LoadUnit();
        }

        private void btn_prv_Click(object sender, EventArgs e)
        {
            index--;
            LoadUnit();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            index++;
            LoadUnit();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try { 
            if (imagesIds.Count == 0)
            {
                MessageBox.Show("This Unit Has No Images.");
                return;
            }
            MongoDBConnection db = new MongoDBConnection();
            List<string> images = new List<string>();
            ldbx.Start();
            foreach (int id in imagesIds)
            {
                var ret = await db.LoadRecordById<ImageModel>("Images", id);
                images.Add(ret.Content);
            }
            ldbx.Abort();
            using (viewPhoto x = new viewPhoto(images))
                x.ShowDialog();
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string from = label2.Text;
            from += "s";
            Building tmp = new Building();
            if (from == "Apartments")
                tmp.Apartments.Add(building.Apartments[index]);
            else if (from == "Storages")
                tmp.Storages.Add(building.Storages[index - building.Apartments.Count]);
            else if (from == "Stores")
                tmp.Stores.Add(building.Stores[index - building.Apartments.Count - building.Storages.Count]);
            using (SellRecord sellRecord = new SellRecord(building, from))
                sellRecord.ShowDialog();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            try { 
            string from = label2.Text;
            from += "s";

            if(lbl_Status.Text == "Sold"){
                SoldModel model = new SoldModel();
                MongoDBConnection db = new MongoDBConnection();
                ldbx.Start();
                if (from == "Apartments")
                    model = await db.LoadRecordById<SoldModel>("Sold", building.Apartments[index].Model.orderID);

                else if (from == "Storages")
                    model = await db.LoadRecordById<SoldModel>("Sold", building.Storages[index - building.Apartments.Count].Model.orderID);
                
                else if (from == "Stores")
                    model = await db.LoadRecordById<SoldModel>("Sold", building.Stores[index - building.Apartments.Count - building.Storages.Count].Model.orderID);
                ldbx.Abort();
                using (SellInfo sellInfo = new SellInfo(model, "Others"))
                    sellInfo.ShowDialog();

            }
            else if(lbl_Status.Text == "Rented")
            {
                RentalModel model = new RentalModel();
                MongoDBConnection db = new MongoDBConnection();
                ldbx.Start();
                if (from == "Apartments")
                    model = await db.LoadRecordById<RentalModel>("Rentals", building.Apartments[index].Model.orderID);

                else if (from == "Storages")
                    model = await db.LoadRecordById<RentalModel>("Rentals", building.Storages[index - building.Apartments.Count].Model.orderID);

                else if (from == "Stores")
                    model = await db.LoadRecordById<RentalModel>("Rentals", building.Stores[index - building.Apartments.Count - building.Storages.Count].Model.orderID);
                ldbx.Abort();
                using (RentInfo rentInfo = new RentInfo(model, "Others"))
                    rentInfo.ShowDialog();
            }
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string from = label2.Text;
            from += "s";
            Building tmp = new Building();
            if (from == "Apartments")
                tmp.Apartments.Add(building.Apartments[index]);
            else if (from == "Storages")
                tmp.Storages.Add(building.Storages[index - building.Apartments.Count]);
            else if (from == "Stores")
                tmp.Stores.Add(building.Stores[index - building.Apartments.Count - building.Storages.Count]);
            using (RentRecord sellRecord = new RentRecord(building, from))
                sellRecord.ShowDialog();
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
        private async void button9_Click(object sender, EventArgs e)
        {
            try { 
            if (inUpdate == false)
            {
                inUpdate = true;
                button12.Visible = button13.Visible = true;
                textBox1.Visible = textBox2.Visible = textBox3.Visible = textBox4.Visible = textBox5.Visible = button2.Visible = true;
                lbl_Address.Visible = lbl_ID.Visible = lbl_Area.Visible = lbl_Price.Visible = lbl_Status.Visible = false;
                button12.Visible = button13.Visible = button1.Visible = false;
                if(label2.Text == "Apartment"){
                    updModel = new ApartmentModel();
                }
                else if(label2.Text == "Storage"){
                    updModel = new StorageModel();
                }
                else if(label2.Text == "Store"){
                    updModel = new StoreModel();
                }
            }
            else
            {
                if (!CheckFilterInput())
                    return;
                inUpdate = false;
                button12.Visible = button13.Visible = false;
                textBox1.Visible = textBox2.Visible = textBox3.Visible = textBox4.Visible = textBox5.Visible = button2.Visible = false;
                lbl_Address.Visible = lbl_ID.Visible = lbl_Area.Visible = lbl_Price.Visible = lbl_Status.Visible = true;
                button12.Visible = button13.Visible = button1.Visible = true;
                updModel.Id = Convert.ToInt32(textBox1.Text);
                updModel.Area = Convert.ToInt32(textBox2.Text);
                updModel.price = Convert.ToInt32(textBox4.Text);
                updModel.Address = textBox3.Text;
                updModel.Status = textBox5.Text;

                MongoDBConnection db = new MongoDBConnection();
                ldbx.Start();
                if (label2.Text == "Apartment")
                {
                    await db.DeleteRecord<ApartmentModel>("Apartments", updModel.Id);
                    await db.InsertRecord("Apartments", updModel);
                }
                else if (label2.Text == "Storage")
                {
                    await db.DeleteRecord<ApartmentModel>("Apartments", updModel.Id);
                    await db.InsertRecord("Apartments", updModel);
                }
                else if (label2.Text == "Store")
                {
                    await db.DeleteRecord<ApartmentModel>("Apartments", updModel.Id);
                    await db.InsertRecord("Apartments", updModel);
                }
                ldbx.Abort();
                LoadUnit();
                MessageBox.Show("Updated Building Successfully");
            }
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
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
                        ldbx.Start();
                        var chk = await ImageModel.getAllModels(new ImageModel { Content = base64String });
                        if (chk.Count > 0)
                        {
                            ldbx.Abort();
                            if ((MessageBox.Show("Unit is Duplicate, Want To add old one instead ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                            {
                                imagesIds.Add(chk[0].Id);
                                if (label8.Text != "")
                                    label8.Text += ", ";
                                label8.Text += imagesIds.Last().ToString();
                            }
                            return ;
                        }
                    }
                }
                ImageModel img = new ImageModel();
                img.Content = base64String;
                MongoDBConnection db = new MongoDBConnection();
                ldbx.Start();
                var nextId = await db.GetNextSeqVal("Images");
                img.Id = nextId;
                await db.InsertRecord<ImageModel>("Images", img);
                ldbx.Abort();
                imagesIds.Add(img.Id);
                if (label8.Text != "")
                    label8.Text += ", ";
                label8.Text += nextId.ToString();
                MessageBox.Show("Added Image Successfully");
            }
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }

        private void LoadUnit(){
            if (index == 0)
                btn_prv.Visible = false;
            else
                btn_prv.Visible = true;
            if (index == cnt - 1)
                btn_next.Visible = false;
            else
                btn_next.Visible = true;
            if (index < building.Apartments.Count){
                ApartmentModel model = building.Apartments[index].Model;
                label2.Text = "Apartment";
                lbl_ID.Text = textBox1.Text = model.Id.ToString();
                lbl_Area.Text = textBox2.Text = model.Area.ToString();
                lbl_Address.Text = textBox3.Text = model.Address;
                lbl_Price.Text = textBox4.Text = model.price.ToString();
                lbl_Status.Text = textBox5.Text = model.Status;
                imagesIds = model.ImagesIds;
                label6.Text = model.orderID.ToString();
                if (label6.Text == "0")
                    button4.Visible = label6.Visible = false;
                else
                    button4.Visible = label6.Visible = true;
                label8.Text = "";
                foreach (int id in model.ImagesIds)
                {
                    if (label8.Text != "")
                        label8.Text += ", ";
                    label8.Text += id.ToString();
                }
            }
            else if(index - building.Apartments.Count < building.Storages.Count){
                StorageModel model = building.Storages[index - building.Apartments.Count].Model;
                label2.Text = "Storage";
                lbl_ID.Text = textBox1.Text = model.Id.ToString();
                lbl_Area.Text = textBox2.Text = model.Area.ToString();
                lbl_Address.Text = textBox3.Text = model.Address;
                lbl_Price.Text = textBox4.Text = model.price.ToString();
                lbl_Status.Text = textBox5.Text = model.Status;
                imagesIds = model.ImagesIds;
                label6.Text = model.orderID.ToString();
                if (label6.Text == "0")
                    button4.Visible = label6.Visible = false;
                else
                    button4.Visible = label6.Visible = true;
                label8.Text = "";
                foreach (int id in model.ImagesIds)
                {
                    if (label8.Text != "")
                        label8.Text += ", ";
                    label8.Text += id.ToString();
                }
            }
            else{
                StoreModel model = building.Stores[index - building.Apartments.Count - building.Storages.Count].Model;
                label2.Text = "Store";
                lbl_ID.Text = textBox1.Text = model.Id.ToString();
                lbl_Area.Text = textBox2.Text = model.Area.ToString();
                lbl_Address.Text = textBox3.Text = model.Address;
                lbl_Price.Text = textBox4.Text = model.price.ToString();
                lbl_Status.Text = textBox5.Text = model.Status;
                imagesIds = model.ImagesIds;
                label6.Text = model.orderID.ToString();
                if (label6.Text == "0")
                    button4.Visible = label6.Visible = false;
                else
                    button4.Visible = label6.Visible = true;
                label8.Text = "";
                foreach (int id in model.ImagesIds)
                {
                    if (label8.Text != "")
                        label8.Text += ", ";
                    label8.Text += id.ToString();
                }
            }
            if (lbl_Status.Text == "Available")
            {
                button4.Visible = false;
                button12.Visible = button13.Visible = true;
            }
            else
            {
                button4.Visible = true;
                button12.Visible = button13.Visible = false;
            }
        }
    }
}
