using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATLASS_FITNESS.Client
{
    public partial class FrmClientInfo : Form
    {
        private int _ClientID;
        public FrmClientInfo(int clientID)
        {
            InitializeComponent();
            _ClientID = clientID;
        }

        private void FrmClientInfo_Load(object sender, EventArgs e)
        {

            clientInfoCard1.loadClientInfo(_ClientID);
            subscribtion_card1.loadcardinfo(_ClientID);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
