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
    public partial class MainSubMenu : Form
    {
        public static BuildingModel searchModel;
        public static ListItem lst = new ListItem(new Building(), "");
        public static Panel controlPanel;
        public string table;
        public MainSubMenu(string table)
        {
            InitializeComponent();
            this.Text = table;
            this.table = table;
            lbl_Menu_Title.Text = "All " + table;
            btn_NewBuilding.Text = "  New " + table.Remove(table.Length - 1);
        }

        private void MainSubMenu_Load(object sender, EventArgs e)
        {
            controlPanel = pnl_Table_Content;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public static bool maximized;

        public bool CheckFilterInput(){
            if (tbx_Filter_ID.Text != "" && !Validator.IsID(tbx_Filter_ID.Text))
                return false;
            return true;
        }


        private async void btn_Filter_Click(object sender, EventArgs e){
            try { 
            if (!CheckFilterInput())
                return;
            searchModel = new BuildingModel();
            
            if (tbx_Filter_ID.Text != "")
                searchModel.Id = Convert.ToInt32(tbx_Filter_ID.Text);
            else
                searchModel.Id = -1;
            /*
            if (tbx_Filter_Area.Text != "")
                searchModel.Area = Convert.ToInt32(tbx_Filter_Area.Text);
            else
            */
                searchModel.Area = -1;
            /*
            if (tbx_Filter_Price.Text != "")
                searchModel.price = Convert.ToInt32(tbx_Filter_Price.Text);
            else
            */
                searchModel.price = -1;
            searchModel.Address = tbx_Filter_Address.Text;
            searchModel.Status = "";
            ldbx.Start();
            await RefreshContent(table);
            ldbx.Abort();
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }

        public static async Task<string> RefreshContent(string table){
            try { 
            var retList = await Building.getAllModels(searchModel, table);
            controlPanel.Controls.Clear();
            foreach (BuildingModel model in retList){
                Building b = new Building(model);
                ListItem c = new ListItem(b, table);
                c.Dock = DockStyle.Top;
                c.LoadLabels();
                if (retList.Count >= 7 && !maximized)
                    c.Fix();
                if (retList.Count > 10 && maximized)
                    c.Fix();
                controlPanel.Controls.Add(c);
            }
            return "Done";
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
                return "false";
            }
        }

        private async void btn_NewBuilding_Click(object sender, EventArgs e)
        {
            try { 
            MongoDBConnection db = new MongoDBConnection();
            ldbx.Start();
            var nextId = await db.GetNextSeqVal("Images");
            ldbx.Abort();
            using (AddRecord addRecord = new AddRecord(true, "Main", new SIPair(table, nextId), table))
                addRecord.ShowDialog();
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }

        private void MainSubMenu_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try { 
            searchModel = new BuildingModel();
            searchModel.Id = -1;
            searchModel.Area = -1;
            searchModel.price = -1;
            searchModel.Address = "";
            searchModel.Status = "";
            

            ldbx.Start();
            await RefreshContent(table);
            ldbx.Abort();
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }
        static Thread ldbx = new Thread(new ThreadStart(Loading));
        public static void Loading()
        {
            Application.Run(new LoadingBox());
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
