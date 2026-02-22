using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design;
using System.Windows.Forms;
using ATLASS_FITNESS_BUISNESS;

namespace ATLASS_FITNESS.Subscrbtion
{
    public partial class Subscribtion_card : UserControl
    {
        private int _clientid = -1;
        public int SubscrptionID
        {
            get { return _clientid; }
            set { _clientid = value; }
        }
        public ClsSubscription ClientInfo { get; set; }
        public Subscribtion_card()
        {
            InitializeComponent();
        }
        public void loadcardinfo(int clientid)
        {
            if (clientid != -1)
            {
                _fillcardinfo(clientid);
            }
            else
            {
                _resetcardinfo();
            }
        }
        private void _fillcardinfo(int clientid)
        {
            _clientid = clientid;
            MessageBox.Show("Loading subscription info for client ID: " + clientid, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClsSubscription subs = ClsSubscription.getSubscriptionByClientID(clientid);
            if (subs != null)
            {
                startday.Text = subs.StartDate.ToShortDateString();
                end.Text = subs.EndDate.ToShortDateString();
                creat.Text = subs.createdby.ToString();
                if (subs.coqchesID != 0) {
                    coach.Text = subs.coqchesID.ToString();
                }
                else
                {
                    coach.Text = "N/A";
                }
               
                active.Text = subs.IS_Active;
                if (subs.IS_Active == "ACTIVE")
                {
                    active.ForeColor = Color.Green;
                }
                else
                {
                    active.ForeColor = Color.Red;
                }



            }
            else
            {
                MessageBox.Show("No subscription found for client ID: " + clientid, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _resetcardinfo();
            }
        }
            private void _resetcardinfo()
            {
                startday.Text = "   ??????";
                end.Text = "   ??????";
                creat.Text = "   ??????";
                coach.Text = "   ??????";
                active.Text = "   ??????";
            }
        } }
