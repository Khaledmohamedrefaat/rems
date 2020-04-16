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
    public partial class TransactionSubMenu : Form
    {
        public static SoldModel searchSoldModel;
        public static RentalModel searchrentModel;
        public static TransactionListItem lst = new TransactionListItem(new SoldModel(), new RentalModel(), "Sold");
        public static Panel controlPanel;
        public string table;
        public static TableLayoutPanel pnl;
        public static void Freeze()
        {
            pnl.Enabled = false;
        }
        public static void UnFreeze()
        {
            pnl.Enabled = true;
        }
        public TransactionSubMenu(string table)
        {
            InitializeComponent();
            this.Text = "Transactions";
            controlPanel = pnl_Table_Content;
            this.table = table;
            comboBox1.SelectedIndex = 0;
            if (table == "Rentals")
                lbl_Menu_Title.Text = "All " + table;
            pnl = pnl_MarginPanel;
        }
        public static bool maximized;
        public bool CheckFilterInput()
        {
            if (tbx_Filter_ID.Text != "" && !Validator.IsName(tbx_Filter_ID.Text))
                return false;
            if (tbx_Filter_Address.Text != "" && !Validator.IsID(tbx_Filter_Address.Text))
                return false;
            return true;
        }
        public static async Task<string> RefreshSoldContent()
        {
            try {
                if (searchSoldModel == null)
                    return "Done";
                Freeze();
            var retList = await Sold.getAllModels(searchSoldModel);
                UnFreeze();
                controlPanel.Controls.Clear();
            foreach (SoldModel model in retList)
            {
                Sold b = new Sold(model);
                TransactionListItem c = new TransactionListItem(model, new RentalModel(), "Sold");
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
        public static async Task<string> RefreshRentalContent()
        {
            if (searchrentModel == null)
                return "Done";
            var retList = await Rental.getAllModels(searchrentModel);
            controlPanel.Controls.Clear();
            foreach (RentalModel model in retList)
            {
                Rental b = new Rental(model);
                TransactionListItem c = new TransactionListItem(new SoldModel(), model, "Rentals");
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
        private async void btn_Filter_Click(object sender, EventArgs e)
        {
            try {
                if (controlPanel == null)
                    return;
                if (!CheckFilterInput())
                return;
            if (table == "Sold")
            {
                searchSoldModel = new SoldModel();
                searchSoldModel.Name = tbx_Filter_ID.Text;
                if(tbx_Filter_Address.Text != "")
                    searchSoldModel.AssetId = new SIPair(comboBox1.SelectedItem.ToString(), Convert.ToInt32(tbx_Filter_Address.Text));
                    Freeze(); 
                    await RefreshSoldContent();
                    UnFreeze();
                }
            else
            {
                searchrentModel = new RentalModel();
                searchrentModel.Name = tbx_Filter_ID.Text;
                if (tbx_Filter_Address.Text != "")
                    searchrentModel.AssetId = new SIPair(comboBox1.SelectedItem.ToString(), Convert.ToInt32(tbx_Filter_Address.Text));
                    Freeze();
                    await RefreshRentalContent();
                    UnFreeze();
                }
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try { 
            if (table == "Sold")
            {
                searchSoldModel = new SoldModel();
                searchSoldModel.Name = tbx_Filter_ID.Text;
                    Freeze();
                    await RefreshSoldContent();
                    UnFreeze();
                }
            else
            {
                searchrentModel = new RentalModel();
                searchrentModel.Name = tbx_Filter_ID.Text;
                    Freeze();
                    await RefreshRentalContent();
                    UnFreeze();
                }
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }
    }
}
