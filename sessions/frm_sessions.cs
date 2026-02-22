using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATLASS_FITNESS_BUISNESS;

namespace ATLASS_FITNESS.sessions
{
    public partial class frm_sessions : Form
    {
        
        // day of now
         string jour = DateTime.Now.DayOfWeek.ToString();
        int totalSessions = 0;
         int sessionsEnCours = 0;
         int sessionsTerminees = 0;
         int sessionsAvenir = 0;

        public frm_sessions()
        {
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frm_sessions_Load(object sender, EventArgs e)
        {
            
            MessageBox.Show("Loading sessions for " + jour);
            DataTable dt = ClsSessions.GetAllSessions(jour);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No sessions found for today.");
                return;
            }
            foreach (DataRow row in dt.Rows)
            {
                pnl_sess sessionPanel = new pnl_sess();
                string coachName = row["CoachFullName"].ToString();
                string sessionType = row["session_type"].ToString();
                TimeSpan startTime = (TimeSpan)row["start_time"];
                TimeSpan endTime = (TimeSpan)row["end_time"];
                sessionPanel.loadpanel(coachName, sessionType, startTime, endTime);
                flowLayoutPanel1.Controls.Add(sessionPanel);
                    totalSessions++;
                if (DateTime.Now.TimeOfDay > startTime)
                {
                    if (DateTime.Now.TimeOfDay < endTime)
                    {
                        sessionsEnCours++;
                    }
                    else
                    {
                        sessionsTerminees++;


                    }
                }


                }
            label4.Text = totalSessions.ToString();
            label5.Text = sessionsEnCours.ToString();
        }
}
}
