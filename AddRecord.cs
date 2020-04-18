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


    public partial class AddRecord : Form
    {
        public void Freeze()
        {
            foreach (Control c in panel2.Controls)
                c.Enabled = false;
        }
        public void UnFreeze()
        {
            foreach (Control c in panel2.Controls)
                c.Enabled = true;
        }
        static List<SIPair> addUnits = new List<SIPair>();
        List<int> addImages = new List<int>();
        static Label unitsLbl;
        bool hasUnits;
        string from, table;
        static SIPair mainassetID;
        SIPair assetId;
        public AddRecord(bool hasUnits, string from, SIPair assetId, string table)
        {
            InitializeComponent();
            this.hasUnits = hasUnits;
            comboBox1.Visible = false;
            lbl_Status.Text = label2.Text = "";
            addImages = new List<int>();
            if (hasUnits)
            {
                label8.Visible = textBox6.Visible = false;
                
                unitsLbl = lbl_Status;
            }
            else
            {
                label10.Visible = textBox7.Visible = false;
            }
            this.from = from;
            this.assetId = assetId;
            this.table = table;
            lbl_Title.Text = "Add New " + table.Remove(table.Length - 1);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AddRecord_Load(object sender, EventArgs e)
        {
            if (!hasUnits){
                button8.Visible = false;
                lbl_Status.Visible = false;
                label1.Text = "    Type :";
                comboBox1.Visible = true;
                lbl_Title.Text = "Add New Unit";
                comboBox1.SelectedIndex = 0;
            }
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            if(mainassetID == null){
                var code = await Asset.GenerateCode(table, textBox4.Text, textBox2.Text);
                mainassetID = new SIPair(table, code);
            }

            using (AddRecord addRecord = new AddRecord(false, "Unit", mainassetID, table))
                addRecord.ShowDialog();
        }

        public bool CheckFilterInput()
        {
            if (!Validator.IsArea(textBox1.Text))
                return false;
            if (!Validator.IsAddress(textBox3.Text))
                return false;
            if (!Validator.IsAddress(textBox2.Text))
                return false;
            if (!Validator.IsAddress(textBox4.Text))
                return false;
            if (!hasUnits && !Validator.IsPrice(textBox5.Text)) ;
            return true;
        }

        public async Task<bool> checkDuplicate(BuildingModel model, string table)
        {
            try
            {

                var tmp = model.Id;
                model.Id = "";
                Freeze();
                var retList = await Building.getAllModels(model, table);
                UnFreeze();
                model.Id = tmp;
                return retList.Count != 0;
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
                return false;
            }
        }
        public async Task<bool> checkDuplicate(ApartmentModel model, string table)
        {
            try { 
            var tmp = model.Id;
            model.Id = "";
                Freeze();
            var retList = await Apartment.getAllModels(model, table);
                UnFreeze();
            model.Id = tmp;
            return retList.Count != 0;
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
                return false;
            }
        }
        
        public async Task<bool> checkDuplicate(AgriculturalLandModel model)
        {
            try { 
            var tmp = model.Id;
            model.Id = "";
                Freeze();
            var retList = await AgriculturalLand.getAllModels(model);
                UnFreeze();
            model.Id = tmp;
            return retList.Count != 0;
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
                return false;
            }
        }
        public async Task<bool> checkDuplicate(LandModel model)
        {
            try { 
            var tmp = model.Id;
            model.Id = "";
                Freeze();
            var retList = await Land.getAllModels(model);
                UnFreeze();
            model.Id = tmp;
            return retList.Count != 0;
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
                return false;
            }
        }
        public async Task<bool> checkDuplicate(VillaModel model)
        {
            try{ 
            var tmp = model.Id;
            model.Id = "";
                Freeze();
            var retList = await Villa.getAllModels(model);
                UnFreeze();
            model.Id = tmp;
            return retList.Count != 0;
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
                return false;
            }
        }
        public async Task<bool> checkDuplicate(StorageModel model)
        {
            try { 
            var tmp = model.Id;
            model.Id = "";
                Freeze();
            var retList = await Storage.getAllModels(model);
                UnFreeze();
            model.Id = tmp;
            return retList.Count != 0;
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
                return false;
            }
        }
        public async Task<bool> checkDuplicate(StoreModel model)
        {
            try { 
            var tmp = model.Id;
            model.Id = "";
                Freeze();
            var retList = await Store.getAllModels(model);
                UnFreeze();
            model.Id = tmp;
            return retList.Count != 0;
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
                return false;
            }
        }

        public async Task<string> fn_AddUnit(){
            try { 
            if (!CheckFilterInput())
                return "Done";

            if (hasUnits)
            {
                BuildingModel model = new BuildingModel();
                MongoDBConnection db = new MongoDBConnection();
                model.Id = assetId.Id;
                model.Area = Convert.ToInt32(textBox1.Text);
                model.Governorate = textBox2.Text;
                model.City = textBox4.Text;
                model.Street = textBox3.Text;
                model.Units = addUnits;
                model.Status = "Available";
                addUnits = new List<SIPair>();
                model.ImagesIds = addImages;
                model.assetID = new SIPair("", "-0");
                    model.numFloors = Convert.ToInt32(textBox7.Text);
                    if (mainassetID == null)
                    {
                        var code = await Asset.GenerateCode(table, textBox4.Text, textBox2.Text);
                        mainassetID = new SIPair(table, code);
                    }
                    model.Id = mainassetID.Id;
                    Freeze();
                if (await checkDuplicate(model, table))
                {
                        UnFreeze();
                    MessageBox.Show("This Unit Already Exists.", "Duplicate!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "Done";
                }
                await Asset.Insert(model, table);
                MessageBox.Show("Added " + table.Remove(table.Length - 1) +" Successfully");
                    mainassetID = null;
                Close();

            }
            else
            {
                string table = comboBox1.SelectedItem.ToString();
                /*
                if(table == null){
                    MessageBox.Show("You Must Choose A Type.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "Done";
                }
                */
                if (table == "Apartments")
                {
                    ApartmentModel model = new ApartmentModel();
                    MongoDBConnection db = new MongoDBConnection();
                    model.Area = Convert.ToInt32(textBox1.Text);
                    model.price = Convert.ToInt32(textBox5.Text);
                    model.Governorate = textBox2.Text;
                    model.City = textBox4.Text;
                    model.Street = textBox3.Text;
                    model.ImagesIds = addImages;
                    model.Status = "Available";
                        string building_id = "";
                        int idx = -1;
                        for (int i = 0; i < assetId.Id.Length; ++i)
                            if (assetId.Id[i] == '-')
                                idx = i;
                        for (int i = idx + 1; i < assetId.Id.Length; ++i)
                            building_id += assetId.Id[i];
                        var nextId = Asset.GenerateCode("Apartments", model.City, model.Governorate, textBox6.Text, building_id);
                    model.Id = nextId;
                    model.assetID = assetId;
                        
                    Freeze();
                    if (await checkDuplicate(model, "Apartments"))
                    {
                        UnFreeze();
                        if ((MessageBox.Show("Unit is Duplicate, Want To add old one instead ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                        {
                            model.Id = "";
                                Freeze();
                            var retList = await Apartment.getAllModels(model, "Apartments");
                                UnFreeze();
                            addLabels(retList[0].Id);
                            MessageBox.Show("Added Apartment Successfully");
                            Close();
                        }
                        return "Done";
                    }
                    Freeze();
                    await Asset.Insert(model, table);
                    UnFreeze();
                    addLabels(nextId);
                    MessageBox.Show("Added Apartment Successfully");
                    Close();
                }
                else if (table == "Storages")
                {
                    StorageModel model = new StorageModel();
                    MongoDBConnection db = new MongoDBConnection();

                    model.Area = Convert.ToInt32(textBox1.Text);
                    model.price = Convert.ToInt32(textBox5.Text);
                    model.Governorate = textBox2.Text;
                    model.City = textBox4.Text;
                    model.Street = textBox3.Text;
                    model.ImagesIds = addImages;
                    model.Status = "Available";
                    model.assetID = assetId;
                        string building_id = "";
                        int idx = -1;
                        for (int i = 0; i < assetId.Id.Length; ++i)
                            if (assetId.Id[i] == '-')
                                idx = i;
                        for (int i = idx + 1; i < assetId.Id.Length; ++i)
                            building_id += assetId.Id[i];
                        var nextId = Asset.GenerateCode("Storages", model.City, model.Governorate, textBox6.Text, building_id);
                        model.Id = nextId;
                        Freeze();
                    if (await checkDuplicate(model))
                    {
                            UnFreeze();
                        if ((MessageBox.Show("Unit is Duplicate, Want To add old one instead ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                        {
                            model.Id = "";
                                Freeze();
                            var retList = await Storage.getAllModels(model);
                                UnFreeze();
                            addLabels(retList[0].Id);
                            MessageBox.Show("Added Storage Successfully");
                            Close();
                        }
                        return "Done";
                    }
                        Freeze();
                    await Asset.Insert(model, table);
                        UnFreeze();

                    addLabels(nextId);

                    MessageBox.Show("Added Storage Successfully");
                    Close();
                }
                else if (table == "Stores")
                {
                    StoreModel model = new StoreModel();
                    MongoDBConnection db = new MongoDBConnection();
                    model.Area = Convert.ToInt32(textBox1.Text);
                    model.price = Convert.ToInt32(textBox5.Text);
                    model.Governorate = textBox2.Text;
                    model.City = textBox4.Text;
                    model.Street = textBox3.Text;
                    model.ImagesIds = addImages;
                    model.Status = "Available";
                    model.assetID = assetId;
                        string building_id = "";
                        int idx = -1;
                        for (int i = 0; i < assetId.Id.Length; ++i)
                            if (assetId.Id[i] == '-')
                                idx = i;
                        for (int i = idx + 1; i < assetId.Id.Length; ++i)
                            building_id += assetId.Id[i];
                        var nextId = Asset.GenerateCode("Stores", model.City, model.Governorate, textBox6.Text, building_id);
                        model.Id = nextId;
                        Freeze();
                        if (await checkDuplicate(model))
                    {
                            UnFreeze();
                            if ((MessageBox.Show("Unit is Duplicate, Want To add old one instead ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                        {
                            model.Id = "";
                                Freeze();
                                var retList = await Store.getAllModels(model);
                                UnFreeze();
                                addLabels(retList[0].Id);
                            MessageBox.Show("Added Store Successfully");
                            Close();
                        }
                        return "Done";
                    }
                        Freeze();
                        await Asset.Insert(model, table);
                        UnFreeze();
                        addLabels(nextId);
                    

                    MessageBox.Show("Added Store Successfully");
                    Close();
                }

            }
            return "Done";
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
                return "false";
            }
        }

        public void addLabels(string nextId){
            string table = comboBox1.Text;
            if (from == "Unit")
            {
                    addUnits.Add(new SIPair(table, nextId));
                    if (unitsLbl.Text != "")
                        unitsLbl.Text += ", ";
                    unitsLbl.Text += nextId.ToString();
            }
            else if(from == "Update")
            {
                InfoRecord.updBuilding.Model.Units.Add(new SIPair(table, nextId));
                if (InfoRecord.unitsLabel.Text != "")
                    InfoRecord.unitsLabel.Text += ", ";
                InfoRecord.unitsLabel.Text += nextId.ToString();
            }
        }
        private async void button12_Click(object sender, EventArgs e)
        {
            try { 
            await fn_AddUnit();
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }
        public async Task<string> fn_AddImage(){
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
                        double fileSizeMB = imageBytes.Length / (1024 * 1024);
                        if (fileSizeMB > 5f)
                        {
                            MessageBox.Show("The Image Size Shouldn't Exceed 5 MB.", "Couldn't Add Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return "Done";
                        }
                        base64String = Convert.ToBase64String(imageBytes);
                        Freeze();
                        var chk = await ImageModel.getAllModels(new ImageModel { Content = base64String });
                        UnFreeze();
                        if (chk.Count > 0){
                            if ((MessageBox.Show("Unit is Duplicate, Want To add old one instead ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
                            {
                                addImages.Add(chk[0].Id);
                                if (label2.Text != "")
                                    label2.Text += ", ";
                                label2.Text += addImages.Last().ToString();
                            }
                            return "Done";
                        }
                    }
                }
                ImageModel img = new ImageModel();
                img.Content = base64String;
                MongoDBConnection db = new MongoDBConnection();
                var nextId = await db.GetNextSeqVal("Images");
                img.Id = nextId;
                Freeze();
                await db.InsertRecord<ImageModel>("Images", img);
                UnFreeze();
                addImages.Add(img.Id);
                if (label2.Text != "")
                    label2.Text += ", ";
                label2.Text += addImages.Last().ToString();
                MessageBox.Show("Added Image Successfully");
            }
            return "Done";
        }

        private void lbl_Title_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try { 
            await fn_AddImage();
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }
    }
}
