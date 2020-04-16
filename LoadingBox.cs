using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Real_Estate_Managment_Software___GUI
{
    public partial class LoadingBox : Form
    {
        public LoadingBox()
        {
            InitializeComponent();
        }

        private void LoadingBox_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}
