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
using Guna.UI2.WinForms;

namespace ATLASS_FITNESS.Client
{
    public partial class ClientInfoCard : UserControl
    {
        private ClsClient _client;
                private int _ClientID = -1;
        public  int ClientID
        {
            get { return _ClientID; }
           
        }

        public ClientInfoCard()
        {
            InitializeComponent();
        }

        private void _FillClientInfo()
        {
            personCard1.loadPersonInfo(_client.PERSONID);
            MessageBox.Show("Loading client info for ID: " +_client.PERSONID);
            guna2TextBox1.Text= _ClientID.ToString();
            guna2TextBox2.Text = _client.registration.ToShortDateString();
            guna2TextBox5.Text = _client.last_seance.ToShortDateString();
            if (_client.Is_Active)
            {
                guna2TextBox4.Text = "Yes";
                guna2TextBox4.ForeColor = Color.Green;

            }
            else
            {
                guna2TextBox4.Text = "No";
                guna2TextBox4.ForeColor = Color.Red;
            }
        }
         public void loadClientInfo(int clientID)
        {
            if (clientID != -1)
            {
                _client = ClsClient.GetClientbyid(clientID);
                _ClientID = clientID;


                _FillClientInfo();
            }
            else
            {
                _ResetClientInfo();
            }
        }
        private void _ResetClientInfo()
        {
            
            label1.Text = "???";

            label2.Text = "???";
            label3.Text = "???";
        }
    }
}
