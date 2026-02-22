using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATLASS_FITNESS
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (guna2CircleProgressBar1.Value==100)
            {
                timer1.Stop();
                Form1 login = new Form1();
                login.Show();
                this.Hide();

            }
            guna2CircleProgressBar1.Value += 2;
            label_val.Text = guna2CircleProgressBar1.Value.ToString() + "%";
        }

        private void Loading_Load(object sender, EventArgs e)
        {

            guna2ShadowForm1.SetShadowForm(this);
            guna2CircleProgressBar1.Value = 0;
            timer1.Start();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
