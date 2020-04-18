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
    public partial class ApartmentSubMenu : Form
    {
        public static ApartmentModel searchModel;
        public static ApartmentListItem lst = new ApartmentListItem(new Apartment(), "");
        public static Panel controlPanel;
        public string table;
        public void Freeze()
        {
            pnl_MarginPanel.Enabled = false;
        }
        public void UnFreeze()
        {
            pnl_MarginPanel.Enabled = true;
        }
        public ApartmentSubMenu(string table)
        {
            InitializeComponent();
            this.table = table;
            this.Text = "Apartments";
            if (table == "Storages"){
                lbl_Menu_Title.Text = "All Storages";
                btn_NewBuilding.Text = "  New Storage";
                this.Text = "Storages";
            }else if(table == "Stores"){
                lbl_Menu_Title.Text = "All Stores";
                btn_NewBuilding.Text = "  New Store";
                this.Text = "Stores";
            }
        }

        public bool CheckFilterInput()
        {
            if (tbx_Filter_ID.Text != "" && !Validator.IsID(tbx_Filter_ID.Text))
                return false;
            if (tbx_Filter_Area.Text != "" && !Validator.IsArea(tbx_Filter_Area.Text))
                return false;
            if (tbx_Filter_Price.Text != "" && !Validator.IsPrice(tbx_Filter_Price.Text))
                return false;
            return true;
        }
        public static bool maximized;
        private async void btn_Filter_Click(object sender, EventArgs e)
        {
            try { 
                if (!CheckFilterInput())
                    return;
                searchModel = new ApartmentModel();
                if (tbx_Filter_ID.Text != "")
                    searchModel.Id = tbx_Filter_ID.Text;
                else
                    searchModel.Id = "";
                if (tbx_Filter_Area.Text != "")
                    searchModel.Area = Convert.ToInt32(tbx_Filter_Area.Text);
                else
                    searchModel.Area = -1;
                if (tbx_Filter_Price.Text != "")
                    searchModel.price = Convert.ToInt32(tbx_Filter_Price.Text);
                else
                    searchModel.price = -1;
                string full_address = tbx_Filter_Address.Text;
                List<string> detailed_address = new List<string>();
                string tmp = "";
                for(int i = 0; i < full_address.Length; ++i){
                    if (full_address[i] == '-') continue;
                    if (full_address[i] == ' ') {
                        if (tmp != "")
                            detailed_address.Add(tmp);
                        tmp = "";
                    }
                    tmp += full_address[i];
                }
                if (detailed_address.Count > 0) searchModel.City = detailed_address[0];
                else searchModel.City = "";
                if (detailed_address.Count > 1) searchModel.Governorate = detailed_address[0];
                else searchModel.Governorate = "";
                if (detailed_address.Count > 2) searchModel.Street = detailed_address[0];
                else searchModel.Street = "";
                if (detailed_address.Count > 3){
                    MessageBox.Show("The Address Format is invalid.");
                    return;
                }
                searchModel.Status = tbx_Filter_Status.Text;
                Freeze();
                await RefreshContent(table);
                UnFreeze();
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }

        private void ApartmentSubMenu_Load(object sender, EventArgs e)
        {
            controlPanel = pnl_Table_Content;
        }

        public static async Task<string> RefreshContent(string table)
        {
            if (searchModel == null)
                return "Done";
            var retList = await Apartment.getAllModels(searchModel, table);
            controlPanel.Controls.Clear();
            foreach (ApartmentModel model in retList)
            {
                Apartment b = new Apartment(model);
                ApartmentListItem c = new ApartmentListItem(b, table);
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

        private void btn_NewBuilding_Click(object sender, EventArgs e)
        {
            using (AddRecord addRecord = new AddRecord(false, "New", new SIPair("", "-0"), table))
                addRecord.ShowDialog();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try { 
                searchModel = new ApartmentModel();
                searchModel.Id = "";
                searchModel.Area = -1;
                searchModel.price = -1;
                searchModel.City = "";
                searchModel.Governorate = "";
                searchModel.Street = "";
                searchModel.Status = tbx_Filter_Status.Text;
                Freeze();
                await RefreshContent(table);
                UnFreeze();
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }
    }
}
