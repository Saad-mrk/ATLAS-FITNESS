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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace ATLASS_FITNESS.Payments
{
    public partial class FrmPayments : Form
    {
        DataTable dtPayments = new DataTable();
        

        public FrmPayments()
        {
            InitializeComponent();
            guna2Panel2.SizeChanged += (s, e) =>
            {
                guna2Panel1.Width = guna2Panel2.Width;
            };
        }

        private void FrmPayments_Load(object sender, EventArgs e)
        {
            guna2DateTimePicker1.MaxDate = DateTime.Now.Date; // Date de fin par défaut : aujourd'hui
            guna2DateTimePicker1.Value = DateTime.Now.Date;
            guna2DateTimePicker2.MaxDate = DateTime.Now.Date; // Date de début par défaut : aujourd'hui
            guna2DateTimePicker2.Value = DateTime.Now.Date.AddDays(-30); // Par défaut, afficher les paiements des 30 derniers jours
            dtPayments = ClsPayment.GetAllPayments();
            if (dtPayments != null)
            {
                MessageBox.Show("Payments loaded successfully!"); // Affiche un message de succès
               guna2DataGridView1.DataSource = dtPayments;
                // Optionnel : Ajuster les colonnes du DataGridView pour une meilleure lisibilité
                guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //mofifier les en-têtes de colonnes si nécessaire
                guna2DataGridView1.RowTemplate.Height = 40;
                guna2DataGridView1.ColumnHeadersHeight = 50;
            }
        }
        

        private void guna2CirclePictureBox1_MouseEnter(object sender, EventArgs e)
        {
            guna2CirclePictureBox1.BackColor =                              //this color 37; 150; 190
                         guna2CirclePictureBox1.FillColor = Color.FromArgb(37, 150, 190); // Couleur d'origine
        }

        private void guna2CirclePictureBox1_MouseLeave(object sender, EventArgs e)
        {
            guna2CirclePictureBox1.BackColor = Color.Transparent; // Couleur d'origine
        }


        private void guna2DateTimePicker1_MouseHover(object sender, EventArgs e)
        {
            guna2DateTimePicker1.FillColor = Color.FromArgb(37, 150, 190); // Couleur d'origine
        }

        private void guna2DateTimePicker2_MouseHover(object sender, EventArgs e)
        {
            guna2DateTimePicker2.FillColor = Color.FromArgb(37, 150, 190); // Couleur d'origine
        }
       
        private void guna2DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime startDate = guna2DateTimePicker2.Value.Date;
            DateTime endDate = guna2DateTimePicker1.Value.Date;

            // Optionnel : vérifier que start <= end
            if (startDate > endDate)
            {
                MessageBox.Show("La date de début doit être inférieure ou égale à la date de fin.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dtPayments = ClsPayment.GetPaymentsByDateRange(startDate, endDate);

            if (dtPayments != null && dtPayments.Rows.Count > 0)
            {
                guna2DataGridView1.DataSource = dtPayments;
            }
            else
            {
                guna2DataGridView1.DataSource = null;
            }

        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime startDate = guna2DateTimePicker2.Value.Date;
            DateTime endDate = guna2DateTimePicker1.Value.Date;
            if (startDate > endDate)
            {
                MessageBox.Show("La date de début doit être inférieure ou égale à la date de fin.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dtPayments = ClsPayment.GetPaymentsByDateRange(startDate, endDate);

            if (dtPayments != null && dtPayments.Rows.Count > 0)
            {
                guna2DataGridView1.DataSource = dtPayments;
            }
            else
            {
                guna2DataGridView1.DataSource = null;
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2ComboBox1.SelectedItem.ToString() == "status")
            {
                dtPayments.DefaultView.RowFilter = "";
            }
            else if (guna2ComboBox1.SelectedItem.ToString() == "Paid")
            {
                dtPayments.DefaultView.RowFilter = "status = 'Paid'";
            }
            else if (guna2ComboBox1.SelectedItem.ToString() == "Unpaid")
            {
                dtPayments.DefaultView.RowFilter = "status = 'Unpaid'";
            }


        }

       
            private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Replace("'", "''"); // éviter erreur quotes

            if (string.IsNullOrWhiteSpace(keyword))
            {
                dtPayments.DefaultView.RowFilter = ""; // reset filtre
            }
            else
            {
                dtPayments.DefaultView.RowFilter =
                    $"full_name LIKE '%{keyword}%'";
            }
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            guna2DateTimePicker1.Value = DateTime.Now.Date;
            guna2DateTimePicker2.Value = DateTime.Now.Date.AddDays(-30); // Par défaut, afficher les paiements des 30 derniers jours

        }
    }
}

