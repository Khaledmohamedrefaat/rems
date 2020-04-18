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
            label3.Text = apartment.Model.Id.ToString();
            label4.Text = apartment.Model.Area.ToString();
            string full_address = "";
            full_address += apartment.Model.City;
            full_address += " - ";
            full_address += apartment.Model.Governorate;
            full_address += " - ";
            full_address += apartment.Model.Street;
            label5.Text = full_address;
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
                    if (apartment.Model.assetID != null)
                    {
                        chk.Id = apartment.Model.assetID.Id;
                        chk.Area = chk.price = -1;
                        chk.City = chk.Governorate = chk.Street = chk.Status = "";
                        string parTable = apartment.Model.assetID.Typ;
                        if (parTable != "")
                        {
                            Freeze();
                            var retList = await Building.getAllModels(chk, parTable);
                            UnFreeze();
                            if (retList.Count != 0)
                            {
                                BuildingModel par = retList[0];
                                foreach (SIPair unit in par.Units)
                                    if (unit.Id == apartment.Model.Id)
                                    {
                                        par.Units.Remove(unit);
                                        break;
                                    }
                                Freeze();
                                await Building.UpdateBuilding(par, parTable);
                                UnFreeze();
                            }
                        }
                    }
                
                    Freeze();
                    await ApartmentSubMenu.RefreshContent(table);
                    UnFreeze();
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
            
            if (table == "Apartments")
                tmp.Apartments.Add(apartment);
            else if (table == "Storages"){
                StorageModel s = new StorageModel();
                s.Id = apartment.Model.Id;
                s.Mongo_id = apartment.Model.Mongo_id;
                s.City = apartment.Model.City;
                s.Governorate = apartment.Model.Governorate;
                s.Street = apartment.Model.Street;
                s.Area = apartment.Model.Area;
                s.assetID = apartment.Model.assetID;
                s.ImagesIds = apartment.Model.ImagesIds;
                s.price = apartment.Model.price;
                s.Status = apartment.Model.Status;
                s.Units = apartment.Model.Units;
                s.orderID = apartment.Model.orderID;
                tmp.Storages.Add(new Storage(s));
            }
            else if (table == "Stores")
            {
                StoreModel s = new StoreModel();
                s.Id = apartment.Model.Id;
                s.Mongo_id = apartment.Model.Mongo_id;
                s.City = apartment.Model.City;
                s.Governorate = apartment.Model.Governorate;
                s.Street = apartment.Model.Street;
                s.Area = apartment.Model.Area;
                s.assetID = apartment.Model.assetID;
                s.ImagesIds = apartment.Model.ImagesIds;
                s.price = apartment.Model.price;
                s.Status = apartment.Model.Status;
                s.Units = apartment.Model.Units;
                s.orderID = apartment.Model.orderID;
                tmp.Stores.Add(new Store(s));
            }
            using (ApartInfo infoRecord = new ApartInfo(tmp))
                infoRecord.ShowDialog();
        }
    }
}
