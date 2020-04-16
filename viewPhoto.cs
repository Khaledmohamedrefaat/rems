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

namespace Real_Estate_Managment_Software___GUI
{
    
    public partial class viewPhoto : Form
    {
        List<string> ImagesBase64 = new List<string>();
        int index = 0;
        public viewPhoto(List<string> ImagesBase64)
        {
            InitializeComponent();
            this.ImagesBase64 = ImagesBase64;
            index = 0;
        }
        private void DisplayImage()
        {
            if (index == 0)
                btn_prv.Visible = false;
            else
                btn_prv.Visible = true;
            if (index == ImagesBase64.Count - 1)
                btn_next.Visible = false;
            else
                btn_next.Visible = true;
            byte[] bytes = Convert.FromBase64String(ImagesBase64[index]);

            Image imagetmp;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                imagetmp = Image.FromStream(ms);
            }
            pictureBox1.Image = imagetmp;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btn_prv_Click(object sender, EventArgs e)
        {
            index--;
            DisplayImage();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            index++;
            DisplayImage();
        }

        private void viewPhoto_Load(object sender, EventArgs e)
        {
            DisplayImage();
        }
    }
    
}
