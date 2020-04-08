using MongoDB.Bson;
using MongoDB.Driver;
using Real_Estate_Management_Software.ConnectionClasses;
using Real_Estate_Management_Software.DatabaseModels;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Milestone2Sample test = new Milestone2Sample();
            await test.Run();
            Close();
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
            
            try
            {
            


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
          
        }
            catch (Exception)
            {
                Console.WriteLine("Insertion Failed. Check Your Connection or Input\n");
            }

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
                Console.Write("Do you want to [S]ell or [R]ent ? : ");  choice = Console.ReadLine()[0];
                choice = char.ToUpper(choice);
                while(choice != 'S' && choice != 'R'){
                    Console.Write("Please Enter a valid choice(S, E) : "); choice = Console.ReadLine()[0];
                    choice = char.ToUpper(choice);
                }
                string change = choice == 'S' ? "Sold" : "Rented";
                var update = Builders<BuildingModel>.Update.Set("Status", change);
                var filter = Builders<BuildingModel>.Filter.Eq("Id", id);
                Console.WriteLine("***********************");
                var collection = db.db.GetCollection<BuildingModel>("Buildings");
                collection.UpdateOne(filter, update);
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
       
        
          
}
