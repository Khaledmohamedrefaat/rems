using Real_Estate_Managment_Software___GUI.DatabaseModels;
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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            /*
            MainSubMenu a = new MainSubMenu("TEST");
            ApartmentSubMenu b = new ApartmentSubMenu("TEST");
            TransactionSubMenu c = new TransactionSubMenu("TEST");
            */

        }
        //Fields
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        private static Panel controlPanel = new Panel();
        string activeFormName;
        private void OpenChildForm(Form childForm, object btnSender) {
            if (activeFormName == "Buildings" || activeFormName == "Villas" || activeFormName == "Lands" || activeFormName == "Factories")
                MainSubMenu.lst = new ListItem(new FunctionalClasses.Building(), "");
            else if (activeFormName == "Apartments" || activeFormName == "Storages" || activeFormName == "Stores")
                ApartmentSubMenu.lst = new ApartmentListItem(new FunctionalClasses.Apartment(), "");
            else if (activeFormName == "Sold" || activeFormName == "Rentals")
                TransactionSubMenu.lst = new TransactionListItem(new SoldModel(), new RentalModel(), "");
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnl_DisplayForms.Controls.Add(childForm);
            pnl_DisplayForms.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lbl_Title.Text = childForm.Text;
        }
        private void btn_Buildings_Click(object sender, EventArgs e)
        {    
            if (currentButton != null)
                currentButton.BackColor = Color.FromArgb(74, 88, 99);
            Button senderBtn = (Button)sender;
            senderBtn.BackColor = Color.FromArgb(106, 208, 224);
            currentButton = senderBtn;
            OpenChildForm(new MainSubMenu("Buildings"), sender);
            activeFormName = "Buildings";
        }
        Thread ldbx = new Thread(new ThreadStart(Loading));
        public static void Loading()
        {
            Application.Run(new LoadingBox());
        }
        private async void MainMenu_Resize(object sender, EventArgs e){
            
            try { 
            
            if(currentButton == btn_Buildings){
                MainSubMenu.maximized = this.WindowState == FormWindowState.Maximized;
                await MainSubMenu.RefreshContent(activeFormName);
            }
            else if(currentButton == btn_Apartments || currentButton == btn_Storages || currentButton == btn_Stores){
                ApartmentSubMenu.maximized = this.WindowState == FormWindowState.Maximized;
                await ApartmentSubMenu.RefreshContent(activeFormName);
            }
            else if(currentButton == btn_Lands){
                TransactionSubMenu.maximized = this.WindowState == FormWindowState.Maximized;
                await TransactionSubMenu.RefreshSoldContent();
            }
            else if(currentButton == btn_Factories)
            {
                TransactionSubMenu.maximized = this.WindowState == FormWindowState.Maximized;
                await TransactionSubMenu.RefreshRentalContent();
            }
            
            }
            catch (Exception _)
            {
                MessageBox.Show("Cannot Reach The Database. Check Your Internet Connection.");
            }
            
            
        }

        private void btn_Apartments_Click(object sender, EventArgs e)
        {
            if (currentButton != null)
                currentButton.BackColor = Color.FromArgb(74, 88, 99);
            Button senderBtn = (Button)sender;
            senderBtn.BackColor = Color.FromArgb(106, 208, 224);
            currentButton = senderBtn;
            OpenChildForm(new ApartmentSubMenu("Apartments"), sender);
            activeFormName = "Apartments";
        }

        private void btn_Storages_Click(object sender, EventArgs e)
        {
            if (currentButton != null)
                currentButton.BackColor = Color.FromArgb(74, 88, 99);
            Button senderBtn = (Button)sender;
            senderBtn.BackColor = Color.FromArgb(106, 208, 224);
            currentButton = senderBtn;
            OpenChildForm(new ApartmentSubMenu("Storages"), sender);
            activeFormName = "Storages";
        }

        private void btn_Stores_Click(object sender, EventArgs e)
        {
            if (currentButton != null)
                currentButton.BackColor = Color.FromArgb(74, 88, 99);
            Button senderBtn = (Button)sender;
            senderBtn.BackColor = Color.FromArgb(106, 208, 224);
            currentButton = senderBtn;
            OpenChildForm(new ApartmentSubMenu("Stores"), sender);
            activeFormName = "Stores";
        }

        private void lbl_Logo_Click(object sender, EventArgs e)
        {
            if (currentButton != null)
                currentButton.BackColor = Color.FromArgb(74, 88, 99);
            currentButton = null;
            pnl_DisplayForms.Controls.Clear();
            lbl_Title.Text = "HOME PAGE";
        }

        private void btn_Factories_Click(object sender, EventArgs e)
        {
            if (currentButton != null)
                currentButton.BackColor = Color.FromArgb(74, 88, 99);
            Button senderBtn = (Button)sender;
            senderBtn.BackColor = Color.FromArgb(106, 208, 224);
            currentButton = senderBtn;
            OpenChildForm(new TransactionSubMenu("Rentals"), sender);
            activeFormName = "Rentals";
        }

        private void btn_Lands_Click(object sender, EventArgs e)
        {
            if (currentButton != null)
                currentButton.BackColor = Color.FromArgb(74, 88, 99);
            Button senderBtn = (Button)sender;
            senderBtn.BackColor = Color.FromArgb(106, 208, 224);
            currentButton = senderBtn;
            OpenChildForm(new TransactionSubMenu("Sold"), sender);
            activeFormName = "Sold";
        }

        private void btn_AgriculturalLands_Click(object sender, EventArgs e)
        {
            if (currentButton != null)
                currentButton.BackColor = Color.FromArgb(74, 88, 99);
            Button senderBtn = (Button)sender;
            senderBtn.BackColor = Color.FromArgb(106, 208, 224);
            currentButton = senderBtn;
            OpenChildForm(new MainSubMenu("Lands"), sender);
            activeFormName = "Lands";
        }

        private void btn_Villas_Click(object sender, EventArgs e)
        {
            if (currentButton != null)
                currentButton.BackColor = Color.FromArgb(74, 88, 99);
            Button senderBtn = (Button)sender;
            senderBtn.BackColor = Color.FromArgb(106, 208, 224);
            currentButton = senderBtn;
            OpenChildForm(new MainSubMenu("Villas"), sender);
            activeFormName = "Villas";
        }

        private void btn_Transaction_Click(object sender, EventArgs e)
        {
            if (currentButton != null)
                currentButton.BackColor = Color.FromArgb(74, 88, 99);
            Button senderBtn = (Button)sender;
            senderBtn.BackColor = Color.FromArgb(106, 208, 224);
            currentButton = senderBtn;
            OpenChildForm(new MainSubMenu("Factories"), sender);
            activeFormName = "Factories";
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            
        }
    }
}
