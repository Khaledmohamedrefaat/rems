using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Real_Estate_Managment_Software___GUI.DatabaseModels;
using System.Threading;

namespace Real_Estate_Managment_Software___GUI
{
    public partial class TransactionListItem : UserControl
    {
        SoldModel soldModel = new SoldModel();
        RentalModel rentModel = new RentalModel();
        string table;
        public TransactionListItem(SoldModel soldModel, RentalModel rentModel, string table)
        {
            InitializeComponent();
            this.soldModel = soldModel;
            btn_Delete.Visible = btn_Add.Visible = false;
            this.table = table;
            this.rentModel = rentModel;
        }
        public void Freeze()
        {
            pnl_Main.Enabled = false;
        }
        public void UnFreeze()
        {
            pnl_Main.Enabled = true;
        }
        public void LoadLabels()
        {
            if(table == "Sold")
            {
                label3.Text = soldModel.Name.ToString();
                label4.Text = soldModel.NationalID.ToString();
                label5.Text = soldModel.AssetId.Typ + " - " + soldModel.AssetId.Id.ToString();
            }
            else
            {
                label3.Text = rentModel.Name.ToString();
                label4.Text = rentModel.NationalID.ToString();
                label5.Text = rentModel.AssetId.Typ + " - " + rentModel.AssetId.Id.ToString();
            }
        }
        public void Fix()
        {
            pnl_Main.ColumnStyles[4].Width -= 1.5f;
        }

        public void HandleMouseEnter()
        {
            if (IsMouseOver(this))
            {
                btn_Add.Visible = btn_Delete.Visible = true;
                panel8.BackColor = panel9.BackColor = panel10.BackColor = panel11.BackColor = panel16.BackColor = btn_Delete.BackColor = Color.FromArgb(237, 248, 226);
            }
        }
        public void HandleMouseLeave()
        {
            if (!IsMouseOver(this))
            {
                btn_Add.Visible = btn_Delete.Visible = false;
                panel8.BackColor = panel9.BackColor = panel10.BackColor = panel11.BackColor = panel16.BackColor = btn_Delete.BackColor = Color.FromArgb(255, 255, 255);
            }
        }
        private bool IsMouseOver(Control control)
        {
            return control.ClientRectangle.Contains(control.PointToClient(MousePosition));
        }


        private async void btn_Delete_Click(object sender, EventArgs e)
        {
            try { 
            if ((MessageBox.Show("Are You Sure You Want To Delete This Record ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
            {
                if (table == "Sold")
                {
                        Freeze();
                    MongoDBConnection db = new MongoDBConnection();
                    await db.DeleteRecord<SoldModel>(table, soldModel.Id);
                    await TransactionSubMenu.RefreshSoldContent();
                        UnFreeze();
                    }
                else
                {
                        Freeze();
                        MongoDBConnection db = new MongoDBConnection();
                    await db.DeleteRecord<SoldModel>(table, rentModel.Id);
                    await TransactionSubMenu.RefreshRentalContent();
                        UnFreeze();
                    }
            }
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }

        private void btn_Delete_MouseEnter(object sender, EventArgs e)
        {
            TransactionSubMenu.lst.HandleMouseLeave();
            TransactionSubMenu.lst.HandleMouseEnter();
            this.HandleMouseEnter();
            TransactionSubMenu.lst = this;
        }

        private void btn_Delete_MouseLeave(object sender, EventArgs e)
        {
            MainSubMenu.lst.HandleMouseLeave();
            MainSubMenu.lst.HandleMouseEnter();
            this.HandleMouseLeave();
            TransactionSubMenu.lst = this;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if(table == "Sold")
            {
                using (SellInfo sellInfo = new SellInfo(soldModel, "ListItem"))
                    sellInfo.ShowDialog();
            }
            else
            {
                using (RentInfo sellInfo = new RentInfo(rentModel, "ListItem"))
                    sellInfo.ShowDialog();
            }
        }
    }
}
