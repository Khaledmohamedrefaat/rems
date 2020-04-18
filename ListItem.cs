using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Real_Estate_Managment_Software___GUI.FunctionalClasses;
using Real_Estate_Managment_Software___GUI.DatabaseModels;

namespace Real_Estate_Managment_Software___GUI
{
    public partial class ListItem : UserControl
    {
        Building building = new Building();
        public string table;
        public ListItem(Building building, string table)
        {
            InitializeComponent();
            btn_Delete.Visible = btn_Add.Visible = false;
            this.building = building;
            this.table = table;
        }
        public void Freeze()
        {
            pnl_Main.Enabled = false;
        }
        public void UnFreeze()
        {
            pnl_Main.Enabled = true;
        }
        public void LoadLabels(){
            label3.Text = building.Model.Id.ToString();
            label4.Text = building.Model.Units.Count.ToString();
            string full_address = "";
            full_address += building.Model.City;
            full_address += " - ";
            full_address += building.Model.Governorate;
            full_address += " - ";
            full_address += building.Model.Street;
            label5.Text = full_address;
        }

        public void Fix(){
            pnl_Main.ColumnStyles[4].Width -= 1.5f;
        }

        private void pnl_Main_MouseEnter(object sender, EventArgs e)
        {
            MainSubMenu.lst.HandleMouseLeave();
            MainSubMenu.lst.HandleMouseEnter();
            this.HandleMouseEnter();
            MainSubMenu.lst = this;
        }

        private void pnl_Main_MouseLeave(object sender, EventArgs e)
        {
            MainSubMenu.lst.HandleMouseLeave();
            MainSubMenu.lst.HandleMouseEnter();
            this.HandleMouseEnter();
            MainSubMenu.lst = this;
        }
        public void HandleMouseEnter(){
            if (IsMouseOver(this)){
                btn_Add.Visible = btn_Delete.Visible = true;
                panel8.BackColor = panel9.BackColor = panel10.BackColor = panel11.BackColor = panel16.BackColor = btn_Delete.BackColor = Color.FromArgb(237, 248, 226);
            }
        }
        public void HandleMouseLeave(){
            if (!IsMouseOver(this)){
                btn_Add.Visible = btn_Delete.Visible = false;
                panel8.BackColor = panel9.BackColor = panel10.BackColor = panel11.BackColor = panel16.BackColor = btn_Delete.BackColor = Color.FromArgb(255, 255, 255);
            }
        }
        private bool IsMouseOver(Control control)
        {
            return control.ClientRectangle.Contains(control.PointToClient(MousePosition));
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            using (InfoRecord infoRecord = new InfoRecord(building, table))
                infoRecord.ShowDialog();
        }

        private void panel16_Enter(object sender, EventArgs e)
        {
            MainSubMenu.lst.HandleMouseLeave();
            this.HandleMouseEnter();
            MainSubMenu.lst = this;
        }

        private void panel16_Leave(object sender, EventArgs e)
        {
            MainSubMenu.lst.HandleMouseLeave();
            this.HandleMouseLeave();
            MainSubMenu.lst = this;
        }

        private async void btn_Delete_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are You Sure You Want To Delete The Unit ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
            {
                MongoDBConnection db = new MongoDBConnection();
                Freeze();
                await Asset.Delete(building, table, building.Model.Id);
                await MainSubMenu.RefreshContent(table);
                UnFreeze(); 
            }
        }

        private void panel10_MouseEnter(object sender, EventArgs e)
        {
            MainSubMenu.lst.HandleMouseLeave();
            MainSubMenu.lst.HandleMouseEnter();
            this.HandleMouseEnter();
            MainSubMenu.lst = this;
        }

        private void btn_Delete_MouseLeave(object sender, EventArgs e)
        {
            MainSubMenu.lst.HandleMouseLeave();
            MainSubMenu.lst.HandleMouseEnter();
            this.HandleMouseLeave();
            MainSubMenu.lst = this;
        }
    }
}
