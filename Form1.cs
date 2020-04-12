using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Real_Estate_Management_Software.ConnectionClasses;
using Real_Estate_Management_Software.DatabaseModels;
using Real_Estate_Management_Software.FunctionalClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Real_Estate_Management_Software
{
    public partial class Form1 : Form
    {
        static List<Apartment> addAparts = new List<Apartment>();
        static List<int> addPhotos = new List<int>();
        static List<SIPair> updateAparts = new List<SIPair>();
        BuildingModel updateModel;
        BuildingModel viewModel;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = label5.Text = label7.Text = label9.Text = label18.Text = label20.Text = "";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private async void button14_Click(object sender, EventArgs e)
        {
            updateModel.Units = updateAparts;
            updateModel.Status = textBox13.Text;
            updateModel.Address = textBox3.Text;
            updateModel.Area = Convert.ToInt32(textBox4.Text);
            await Building.UpdateBuilding(updateModel);
            MessageBox.Show("Updated Building Successfully");
            label4.Text = label5.Text = label7.Text = label9.Text = label18.Text = label20.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox13.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Form2 x = new Form2()){
                x.ShowDialog();
            }
            if (addAparts.Count == 0)
                return;
            if (label4.Text != "")
                label4.Text += ", ";
            label4.Text += addAparts.Last().Model.Id.ToString();
            
        }
        public async static Task<string> addApart(Apartment app){
            MongoDBConnection db = new MongoDBConnection();
            var nextId = await db.GetNextSeqVal("Apartments");
            app.Model.Id = nextId;
            await db.InsertRecord("Apartments", app.Model);
            app.Model = await db.LoadRecordById<ApartmentModel>("Apartments", app.Model.Id);
            addAparts.Add(app);
            updateAparts.Add(new SIPair("Aparments", nextId));
            MessageBox.Show("Added Apartment Successfully");
            return "Done";
        }

        private async void button13_Click(object sender, EventArgs e)
        {
            BuildingModel model = new BuildingModel();
            model.Area = Convert.ToInt32(textBox1.Text);
            model.Address = textBox2.Text;
            model.Status = "Available";
            model.ImagesIds = addPhotos;
            MongoDBConnection db = new MongoDBConnection();
            var nextId = await db.GetNextSeqVal("Buildings");
            model.Id = nextId;
            foreach (Apartment app in addAparts)
                model.Units.Add(new SIPair("Apartments", app.Model.Id));
            await Asset.Insert(model, "Buildings");
            MessageBox.Show("Added Building Successfully");
            label4.Text = label5.Text = textBox1.Text = textBox2.Text = "";
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            BuildingModel model = new BuildingModel();
            var id = Convert.ToInt32(textBox6.Text);
            await Building.Delete(model, "Buildings", id);
            MessageBox.Show("Deleted Building Successfully");
            textBox6.Text = "";
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            BuildingModel model = new BuildingModel();
            SoldModel tmp = new SoldModel();
            Sold tmpObj = new Sold(tmp);
            await Asset.Sell(tmpObj, model, "Buildings", Convert.ToInt32(textBox10.Text));
            MessageBox.Show("Sold Building Successfully");
            textBox10.Text = "";
        }

        private async void button11_Click(object sender, EventArgs e)
        {
            BuildingModel model = new BuildingModel();
            RentalModel tmp = new RentalModel();
            Rental tmpObj = new Rental(tmp);
            await Asset.Rent(tmpObj, model, "Buildings", Convert.ToInt32(textBox11.Text));
            MessageBox.Show("Rented Building Successfully");
            textBox11.Text = "";
        }

        private async void button9_Click(object sender, EventArgs e)
        {
            MongoDBConnection db = new MongoDBConnection();
            viewModel = await db.LoadRecordById<BuildingModel>("Buildings", Convert.ToInt32(textBox8.Text));
            textBox7.Text = viewModel.Area.ToString();
            textBox9.Text = viewModel.Address;
            textBox12.Text = viewModel.Status;
            label18.Text = "";
            foreach (SIPair ass in viewModel.Units){
                if (label18.Text != "")
                    label18.Text += ", ";
                label18.Text += ass.Id.ToString();
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            MongoDBConnection db = new MongoDBConnection();
            updateModel = await db.LoadRecordById<BuildingModel>("Buildings", Convert.ToInt32(textBox5.Text));
            textBox4.Text = updateModel.Area.ToString();
            textBox3.Text = updateModel.Address;
            textBox13.Text = updateModel.Status;
            label9.Text = "";
            foreach (SIPair ass in updateModel.Units)
            {
                if (label9.Text != "")
                    label9.Text += ", ";
                label9.Text += ass.Id.ToString();
            }
            updateAparts = new List<SIPair>();
            updateAparts = updateModel.Units;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (Form2 x = new Form2())
            {
                x.ShowDialog();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Building building = new Building(viewModel);
            using (viewApartsForm x = new viewApartsForm(building))
                x.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            using (viewAllForm x = new viewAllForm())
                x.ShowDialog();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            string base64String = "";
            if (open.ShowDialog() == DialogResult.OK){
                using (Image image = Image.FromFile(open.FileName)){
                    using (MemoryStream m = new MemoryStream()){
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        base64String = Convert.ToBase64String(imageBytes);
                    }
                }
                ImageModel img = new ImageModel();
                img.Content = base64String;
                MongoDBConnection db = new MongoDBConnection();
                var nextId = await db.GetNextSeqVal("Images");
                img.Id = nextId;
                await db.InsertRecord<ImageModel>("Images", img);
                addPhotos.Add(img.Id);
                if (label5.Text != "")
                    label5.Text += ", ";
                label5.Text += addPhotos.Last().ToString();
                MessageBox.Show("Added Image Successfully");
            }
            
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            MongoDBConnection db = new MongoDBConnection();
            List<string> images = new List<string>();
            foreach(int id in viewModel.ImagesIds){
                var ret = await db.LoadRecordById<ImageModel>("Images", id);
                images.Add(ret.Content);
            }
            using (viewPhoto x = new viewPhoto(images))
                x.ShowDialog();
        }
    }
    public class Milestone2Sample
    {
        public void PrintOptions()
        {
            Console.WriteLine("What Operation do you want to do to the table ?");
            Console.WriteLine("[V]iew the table");
            Console.WriteLine("[I]nsert a building");
            Console.WriteLine("[D]elete a building");
            Console.WriteLine("[U]pdate a building");
            Console.WriteLine("[E]xit");
        }
        public async Task<String> ViewTable()
        {
            try
            {
                MongoDBConnection db = new MongoDBConnection();
                var ret = await db.LoadRecords<BuildingModel>("Buildings");
                Console.WriteLine("************************************************************************");
                Console.WriteLine("Buildings Table:");
                Console.WriteLine("----------------");
                foreach (var building in ret)
                {
                    Console.WriteLine($"ID : {building.Id} - Address : {building.Address} - Area : {building.Area} - Status : {building.Status} \n");
                }
                Console.WriteLine("*************************************************************************");
            }
            catch (Exception)
            {
                Console.WriteLine("Retrieving Table Failed. Check Your Connection or Input");
            }

            return "Done";
        }
        public async Task<String> InsertRecord()
        {
            /*
            try
            {
            */


            MongoDBConnection db = new MongoDBConnection();
            var res = await db.LoadRecords<Seq>("Sequences");
            BuildingModel toAdd = new BuildingModel();
            var ret2 = await db.GetNextSeqVal("Sequences");
            toAdd.Id = ret2;
            Console.Write("Enter Building Address :"); toAdd.Address = Console.ReadLine();
            Console.Write("Enter Building Area :"); toAdd.Area = Convert.ToInt32(Console.ReadLine());
            toAdd.Status = "Available";
            Console.WriteLine("*********************");
            var ret = await db.InsertRecord<BuildingModel>("Buildings", toAdd);
            Console.WriteLine("Inserted Successfully.");
            Console.WriteLine("*********************");
            /*    
            }
                catch (Exception)
                {
                    Console.WriteLine("Insertion Failed. Check Your Connection or Input\n");
                }*/

            return "Done";
        }
        public async Task<String> DeleteRecord()
        {
            try
            {
                MongoDBConnection db = new MongoDBConnection();
                Console.Write("Enter Building Id  :"); var id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("*********************");
                var ret = await db.DeleteRecord<BuildingModel>("Buildings", id);
                Console.WriteLine("Deleted Successfully.");
                Console.WriteLine("*********************");
            }
            catch (Exception)
            {
                Console.WriteLine("Deletion Failed. Check Your Connection or Input\n");
            }
            return "Done";
        }
        public async Task<String> UpdateRecord()
        {
            try
            {
                MongoDBConnection db = new MongoDBConnection();
                Console.Write("Enter Building Id  :"); var id = Convert.ToInt32(Console.ReadLine());
                char choice;
                Console.Write("Do you want to [S]ell or [R]ent ? : "); choice = Console.ReadLine()[0];
                choice = char.ToUpper(choice);
                while (choice != 'S' && choice != 'R') {
                    Console.Write("Please Enter a valid choice(S, E) : "); choice = Console.ReadLine()[0];
                    choice = char.ToUpper(choice);
                }
                string change = choice == 'S' ? "Sold" : "Rented";
                await db.UpdateRecord<BuildingModel>("Buildings", id, new BuildingModel(), "Status", change);
                Console.WriteLine("***********************");
                Console.WriteLine("Updated Successfully.");
                Console.WriteLine("***********************");
            }
            catch (Exception)
            {
                Console.WriteLine("Insertion Failed. Check Your Connection or Input\n");
            }

            return "Done";
        }
        public async Task<string> Run()
        {
            PrintOptions();
            char choice;
            string inp = Console.ReadLine();
            if (inp != "")
                choice = inp[0];
            else
                choice = 'L';
            choice = char.ToUpper(choice);
            while (choice != 'E')
            {
                switch (choice)
                {
                    case 'V':
                        await ViewTable();
                        break;
                    case 'I':
                        await InsertRecord();
                        break;
                    case 'D':
                        await DeleteRecord();
                        break;
                    case 'U':
                        await UpdateRecord();
                        break;
                    case 'E':
                        break;
                    default:
                        Console.WriteLine("Not A Valid Choice Try Again");
                        break;
                }
                PrintOptions();
                inp = Console.ReadLine();
                if (inp != "")
                    choice = inp[0];
                else
                    choice = 'L';
                choice = char.ToUpper(choice);
            }
            return "Done";
        }
    }

    public class Milestone3{
        private readonly string table = "Buildings";
        public void PrintOptions()
        {
            Console.WriteLine("What Operation do you want to do to the table ?");
            Console.WriteLine("[1] - View All Buildings");
            Console.WriteLine("[2] - View A Building");
            //Console.WriteLine("[3] - Sell A Building");
            //Console.WriteLine("[4] - Rent A Building");
            Console.WriteLine("[5] - Add A New Building");
            Console.WriteLine("[6] - Delete A Building");
            Console.WriteLine("[7] - Update Building Info");
            Console.WriteLine("[0] - Exit");
        }
        public void PrintOne(BuildingModel b){

            Console.WriteLine("---------------------");
            Console.WriteLine($"ID : {b.Id}");
            Console.WriteLine($"Area : {b.Area}");
            Console.WriteLine($"Address : {b.Address}");
            Console.WriteLine($"Status : {b.Status}");
            Console.WriteLine("---------------------");
        }
        public void PrintOne(StorageModel b)
        {

            Console.WriteLine("---------------------");
            Console.WriteLine($"ID : {b.Id}");
            Console.WriteLine($"Area : {b.Area}");
            Console.WriteLine($"Address : {b.Address}");
            Console.WriteLine($"Status : {b.Status}");
            Console.WriteLine("---------------------");
        }
        public void PrintOne(StoreModel b)
        {

            Console.WriteLine("---------------------");
            Console.WriteLine($"ID : {b.Id}");
            Console.WriteLine($"Area : {b.Area}");
            Console.WriteLine($"Address : {b.Address}");
            Console.WriteLine($"Status : {b.Status}");
            Console.WriteLine("---------------------");
        }
        public void PrintOne(ApartmentModel b)
        {

            Console.WriteLine("---------------------");
            Console.WriteLine($"ID : {b.Id}");
            Console.WriteLine($"Area : {b.Area}");
            Console.WriteLine($"Address : {b.Address}");
            Console.WriteLine($"Status : {b.Status}");
            Console.WriteLine("---------------------");
        }
        public async Task<string> ViewAll(){
            Console.WriteLine("**************************************************");
            Console.WriteLine("Buildings Table");
            MongoDBConnection db = new MongoDBConnection();
            List<BuildingModel> tmp = await db.LoadRecords<BuildingModel>(this.table);
            foreach(BuildingModel b in tmp)
                PrintOne(b);
            Console.WriteLine("**************************************************");
            return "Done";
        }
        public async Task<string> viewOne(int Id){
            MongoDBConnection db = new MongoDBConnection();
            var ret = await db.LoadRecordById<BuildingModel>(this.table, Id);
            PrintOne(ret);
            Console.WriteLine("[1]- View Apartments");
            Console.WriteLine("[2]- View Storages");
            Console.WriteLine("[3]- View Stores");
            Console.WriteLine("[0] - Do Nothing");
            int ch = Convert.ToInt32(Console.ReadLine());
            Building buildingObject = new Building(ret);
            await buildingObject.LoadUnits(ret.Units);
            switch (ch)
            {
                case 1:
                    foreach (var apart in buildingObject.Apartments)
                        PrintOne(apart.Model);
                    break;
                case 2:
                    foreach (var storage in buildingObject.Storages)
                        PrintOne(storage.Model);
                    break;
                case 3:
                    foreach (var store in buildingObject.Stores)
                        PrintOne(store.Model);
                    break;
                case 0:
                    break;
                default:
                    break;
            }
            return "Done";
        }
        public async Task<string> Run()
        {
            MongoDBConnection db = new MongoDBConnection();
            PrintOptions();
            int choice = 100;
            choice = Convert.ToInt32(Console.ReadLine());
            int tmp, tmp2, tmp3;
            string tmp4, tmp5;
            while (choice > 0)
            {
                switch (choice)
                {
                    case 1:
                        await ViewAll();
                        break;
                    case 2:
                        Console.Write("Enter ID : ");  tmp = Convert.ToInt32(Console.ReadLine());
                        await viewOne(tmp);
                        break;
                    case 3:
                        //Console.Write("Enter ID : ");  tmp = Convert.ToInt32(Console.ReadLine());
                        
                        break;
                    case 4:
                        //
                        break;
                    case 5:
                        BuildingModel xx = new BuildingModel();
                        Building xxo = new Building(xx);
                        xxo.readFromUser();
                        for(int i = 0; i < 3; ++i)
                        {
                            Console.WriteLine("Enter Unit No." + (i + 1) + ":");
                            Console.WriteLine("Enter Category : (Apartments - Stores - Storages)"); tmp4 = Console.ReadLine();
                            Console.Write("Enter the Area :"); tmp = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter the Address :"); tmp5 = Console.ReadLine();
                            if(tmp4 == "Apartments")
                            {
                                ApartmentModel ap = new ApartmentModel(tmp, "Available", tmp5, new List<SIPair>(), new List<int>());
                                var id1 = await db.GetNextSeqVal("ay7aga");
                                ap.Id = id1;
                                await Asset.Insert<ApartmentModel>(ap, tmp4);
                                xxo.Model.Units.Add(new SIPair(tmp4, id1));
                            }
                            else if(tmp4 == "Stores")
                            {
                                StoreModel ap = new StoreModel(tmp, "Available", tmp5, new List<SIPair>(), new List<int>());
                                var id1 = await db.GetNextSeqVal("ay7aga");
                                ap.Id = id1;
                                await Asset.Insert<StoreModel>(ap, tmp4);
                                xxo.Model.Units.Add(new SIPair(tmp4, id1));
                            }
                            else if(tmp4 == "Storages")
                            {
                                StorageModel ap = new StorageModel(tmp, "Available", tmp5, new List<SIPair>(), new List<int>());
                                var id1 = await db.GetNextSeqVal("ay7aga");
                                ap.Id = id1;
                                await Asset.Insert<StorageModel>(ap, tmp4);
                                xxo.Model.Units.Add(new SIPair(tmp4, id1));
                            }
                        }
                        await Asset.Insert<BuildingModel>(xxo.Model, this.table);
                        break;
                    case 6:
                        Console.Write("Enter the ID :"); tmp = Convert.ToInt32(Console.ReadLine());
                        await db.DeleteRecord<BuildingModel>(this.table, tmp);
                        break;
                    case 7:
                        Console.Write("Enter the ID :"); tmp = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter the Area :"); tmp2 = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter the Address :"); tmp4 = Console.ReadLine();
                        BuildingModel x = await db.LoadRecordById<BuildingModel>(this.table, tmp);
                        x.Area = tmp2;
                        x.Address = tmp4;
                        await Building.UpdateBuilding(x);
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Please Enter a valid option(0-7)");
                        break;
                }
                PrintOptions();
                choice = Convert.ToInt32(Console.ReadLine());
            }
            return "Done";
        }
    }
          
}
