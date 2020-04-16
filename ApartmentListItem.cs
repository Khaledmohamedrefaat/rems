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
using Real_Estate_Managment_Software___GUI.FunctionalClasses;
using System.Threading;

namespace Real_Estate_Managment_Software___GUI
{
    public partial class ApartmentListItem : UserControl
    {
        Apartment apartment = new Apartment();
        public string table;
        public ApartmentListItem(Apartment apartment, string table)
        {
            InitializeComponent();
            btn_Delete.Visible = btn_Add.Visible = false;
            this.apartment = apartment;
            this.table = table;
        }
        Thread ldbx = new Thread(new ThreadStart(Loading));
        public static void Loading()
        {
            Application.Run(new LoadingBox());
        }
        public void LoadLabels()
        {
            label3.Text = apartment.Model.Id.ToString();
            label4.Text = apartment.Model.Area.ToString();
            label5.Text = apartment.Model.Address.ToString();
            label6.Text = apartment.Model.price.ToString();
            label7.Text = apartment.Model.Status.ToString();
        }
        public void Fix()
        {
            pnl_Main.ColumnStyles[6].Width -= 1.5f;
        }

        private async void btn_Delete_Click(object sender, EventArgs e)
        {
            try { 
            MongoDBConnection db = new MongoDBConnection();
            if ((MessageBox.Show("Are You Sure You Want To Delete The Unit ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
            {
                await Asset.Delete(apartment, table, apartment.Model.Id);
                BuildingModel chk = new BuildingModel();
                chk.Id = apartment.Model.assetID.Id;
                chk.Area = chk.price = -1;
                chk.Address = chk.Status = "";
                string parTable = apartment.Model.assetID.Typ;
                ldbx.Start();
                var retList = await Building.getAllModels(chk, parTable);
                ldbx.Abort();
                if (retList.Count != 0){
                    BuildingModel par = retList[0];
                        foreach(SIPair unit in par.Units)
                            if(unit.Id == apartment.Model.Id){
                                par.Units.Remove(unit);
                                break;
                            }
                    ldbx.Start();
                    await Building.UpdateBuilding(par, parTable);
                    ldbx.Abort();
                }
                ldbx.Start();
                await ApartmentSubMenu.RefreshContent(table);
                ldbx.Abort();
            }
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
        }
        public void HandleMouseEnter()
        {
            if (IsMouseOver(this))
            {
                btn_Add.Visible = btn_Delete.Visible = true;
                panel8.BackColor = panel9.BackColor = panel10.BackColor = panel11.BackColor = panel12.BackColor = panel15.BackColor = panel16.BackColor = btn_Delete.BackColor = Color.FromArgb(237, 248, 226);
            }
        }
        public void HandleMouseLeave()
        {
            if (!IsMouseOver(this))
            {
                btn_Add.Visible = btn_Delete.Visible = false;
                panel8.BackColor = panel9.BackColor = panel10.BackColor = panel11.BackColor = panel12.BackColor =  panel15.BackColor = panel16.BackColor = btn_Delete.BackColor = Color.FromArgb(255, 255, 255);
            }
        }
        private bool IsMouseOver(Control control)
        {
            return control.ClientRectangle.Contains(control.PointToClient(MousePosition));
        }

        private void btn_Delete_MouseEnter(object sender, EventArgs e)
        {
            ApartmentSubMenu.lst.HandleMouseLeave();
            ApartmentSubMenu.lst.HandleMouseEnter();
            this.HandleMouseEnter();
            ApartmentSubMenu.lst = this;
        }

        private void btn_Delete_MouseLeave(object sender, EventArgs e)
        {
            ApartmentSubMenu.lst.HandleMouseLeave();
            ApartmentSubMenu.lst.HandleMouseEnter();
            this.HandleMouseEnter();
            ApartmentSubMenu.lst = this;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Building tmp = new Building();
            tmp.Apartments.Add(apartment);
            using (ApartInfo infoRecord = new ApartInfo(tmp))
                infoRecord.ShowDialog();
        }
    }
}
