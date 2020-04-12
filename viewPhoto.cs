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

namespace Real_Estate_Management_Software
{
    public partial class viewPhoto : Form
    {
        List<string> ImagesBase64 = new List<string>();
        int index = 0;
        public viewPhoto(List<string> ImagesBase64)
        {
            InitializeComponent();
            this.ImagesBase64 = ImagesBase64;
        }

        private void viewPhoto_Load(object sender, EventArgs e)
        {
            index = 0;
            DisplayImage();
            button9.Visible = false;
            if (ImagesBase64.Count == 1)
                button1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            index++;
            DisplayImage();
            if (index == ImagesBase64.Count - 1)
                button1.Visible = false;
            if (index > 0)
                button9.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            index--;
            DisplayImage();
            if (index == 0)
                button9.Visible = false;
            if (index < ImagesBase64.Count - 1)
                button1.Visible = true;
        }
        private void DisplayImage(){
            byte[] bytes = Convert.FromBase64String(ImagesBase64[index]);

            Image imagetmp;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                imagetmp = Image.FromStream(ms);
            }
            pictureBox1.Image = imagetmp;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
