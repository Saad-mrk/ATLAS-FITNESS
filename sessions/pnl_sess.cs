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

namespace ATLASS_FITNESS.sessions
{
    public partial class pnl_sess : UserControl
    {
        int terimine = 0;
        int encours = 0;
        int avenir = 0;
        public pnl_sess()
        {
            InitializeComponent();
        }
        public void loadpanel(string coachname, string type , TimeSpan start_time , TimeSpan end_time  )

        {
            if (DateTime.Now.TimeOfDay > start_time)
            {
                if (DateTime.Now.TimeOfDay < end_time)
                {
                    
                    label1.Text = "En cours";
                    label1.ForeColor = Color.FromArgb(255, 152, 0); ;
                    encours++;
                }
                else
                {
                   
                    label1.Text = "Terminée";
                    label1.ForeColor = Color.FromArgb(158, 158, 158); ;
                    terimine++;
                }
            }
            else
            {
                
                label1.Text = "À venir";
                label1.ForeColor = Color.FromArgb(76, 175, 80); ;
                avenir++;
            }
            lblcoach.Text = coachname;
            lbltype.Text = type;
            lblsatart.Text = start_time.ToString(@"hh\:mm");
        }

       
    }
}
