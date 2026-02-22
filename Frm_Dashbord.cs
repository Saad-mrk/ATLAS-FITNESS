using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ATLASS_FITNESS.Client;
using ATLASS_FITNESS.Payments;
using ATLASS_FITNESS.sessions;
using ATLASS_FITNESS_BUISNESS;
using Guna.Charts.WinForms;

namespace ATLASS_FITNESS
{
    public partial class Frm_Dashbord : Form
    {
        // ─────────────────────────────────────────
        //  Jours de la semaine
        // ─────────────────────────────────────────
        private readonly string[] shortDays =
        {
            "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"
        };
        private readonly string[] fullDays =
        {
            "Monday", "Tuesday", "Wednesday",
            "Thursday", "Friday", "Saturday", "Sunday"
        };

        public Frm_Dashbord()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.AllowTransparency = false;
            this.BackColor = Color.White;
        }

  
        private void Frm_Dashbord_Load(object sender, EventArgs e)
        {
            LoadAttendanceChart();
            LoadKpiCards();
            LoadRecentClientsTable();
        }

        private void LoadAttendanceChart()
        {
            chartPresences.Datasets.Clear();

            Dictionary<string, int> weekData =
                ClsAttendance.GetCurrentWeekAttendances();

            var dataset = new GunaBarDataset
            {
                Label = "Weekly Attendances"
            };
            dataset.FillColors.Add(Color.FromArgb(66, 133, 244));

            for (int i = 0; i < fullDays.Length; i++)
            {
                int count = weekData.ContainsKey(fullDays[i])
                            ? weekData[fullDays[i]]
                            : 0;
                dataset.DataPoints.Add(shortDays[i], count);
            }

            chartPresences.Datasets.Add(dataset);
            chartPresences.Update();
        }


        private void LoadKpiCards()
        {
            try
            {
                // --- Total clients ---
                int totalClients = ClsClient.GetTotalClients();
                lblTotalClients.Text = totalClients.ToString();

                // --- Présences aujourd'hui ---
                int presencesToday = ClsAttendance.GetTodayAttendanceCount();
                lblPresencesToday.Text = presencesToday.ToString();

                // --- Sessions actives en ce moment ---
                lblsession.Text = 3.ToString();

                // --- Abonnements expirant dans les 7 prochains jours ---
                int expiringCount = 7;
                lblExpiringAbo.Text = expiringCount.ToString();

                // Alerte visuelle si abonnements urgents
                if (expiringCount > 0)
                {
                    lblExpiringAbo.ForeColor = Color.FromArgb(239, 68, 68);   // rouge
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur chargement KPI : " + ex.Message,
                                "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadRecentClientsTable()
        {
            DataTable clients = ClsClient.GetRecentClients(10);

            // Style d'abord (définit les colonnes)StyleDataGridView
            StyleDataGridVieww(dgvRecentClients);

            // Données ensuite
            dgvRecentClients.DataSource = clients;

     
            dgvRecentClients.CellClick -= DgvClients_CellClick;
            dgvRecentClients.CellClick += DgvClients_CellClick;
            dgvRecentClients.CellFormatting -= DgvClients_CellFormatting;
            dgvRecentClients.CellFormatting += DgvClients_CellFormatting;
        }


        private void DgvClients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Récupère l'ID stocké dans la Tag de la ligne
            if (dgvRecentClients.Rows[e.RowIndex].Tag is int clientId)
            {
                using (frmAddUpdateClient frm = new frmAddUpdateClient(clientId))
                {
                    frm.ShowDialog();
                    LoadRecentClientsTable(); // Rafraîchit après modification
                }
            }
        }


        private void guna2Button2_Click(object sender, EventArgs e)
        {
            using (Frm_scanner frm = new Frm_scanner())
            {
                frm.ShowDialog();
                LoadKpiCards();              // Rafraîchit les KPI après scan
                LoadAttendanceChart();
            }
        }

        // Scanner Sortie
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            using (Frm_Scanner_Sortie frm = new Frm_Scanner_Sortie())
            {
                frm.ShowDialog();
                LoadKpiCards();
            }
        }

        // Ajouter un client
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            using (frmAddUpdateClient frm = new frmAddUpdateClient())
            {
                frm.ShowDialog();
                LoadKpiCards();
                LoadRecentClientsTable();    // Rafraîchit le tableau
            }
        }

        // Paiement
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            using (FrmPayments frm = new FrmPayments())
            {
                frm.ShowDialog();
                LoadKpiCards();
            }
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            LoadKpiCards();
            LoadAttendanceChart();
        }


        private void StyleDataGridVieww(DataGridView dgv)
        {
            // ── Comportement général ──
            dgv.BackgroundColor = Color.FromArgb(22, 25, 33);
            dgv.GridColor = Color.FromArgb(42, 47, 64);
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // On gère manuellement
            dgv.ScrollBars = ScrollBars.Both;
            dgv.AutoGenerateColumns = false; // ← IMPORTANT : on définit les colonnes manuellement

            // ── En-têtes ──
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 34, 48);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(107, 114, 128);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgv.ColumnHeadersHeight = 42;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // ── Cellules ──
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(22, 25, 33);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(232, 234, 240);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(40, 45, 60);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.RowTemplate.Height = 48;

            // ── Lignes alternées ──
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(28, 32, 44);
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(180, 185, 200);
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(40, 45, 60);
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;

            // ── Définir les colonnes manuellement avec les vrais noms SQL ──
            dgv.Columns.Clear();

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Last_Name",
                HeaderText = "Nom",
                DataPropertyName = "Last_Name",   // ← nom exact de la colonne SQL
                Width = 160,
                MinimumWidth = 120,
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "First_Name",
                HeaderText = "Prénom",
                DataPropertyName = "First_Name",
                Width = 160,
                MinimumWidth = 120,
            });

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "registration_date",
                HeaderText = "Date d'inscription",
                DataPropertyName = "registration_date",
                Width = 150,
                MinimumWidth = 120,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "is_active",
                HeaderText = "Statut",
                DataPropertyName = "is_active",
                Width = 140,
                MinimumWidth = 110,
            });
        }
        private void DgvClients_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.Value == null) return;

            string colName = dgvRecentClients.Columns[e.ColumnIndex].Name;
            string value = e.Value.ToString();

            switch (colName)
            {
                case "is_active":
                    bool isActive = value == "True" || value == "1" || value == "true";
                    e.Value = isActive ? "● Actif" : "● Inactif";
                    e.CellStyle.ForeColor = isActive
                        ? Color.FromArgb(74, 222, 128)    // vert
                        : Color.FromArgb(248, 113, 113);  // rouge
                    e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    e.FormattingApplied = true;
                    break;

                case "registration_date":
                    if (DateTime.TryParse(value, out DateTime date))
                    {
                        e.Value = date.ToString("dd/MM/yyyy");
                        e.FormattingApplied = true;
                    }
                    break;
            }
        }


        public class ClientViewModel
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string Plan { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime ExpirationDate { get; set; }
            public string PaymentStatus { get; set; }
            public string Status { get; set; }
        }

        public class AbonnementViewModel
        {
            public int Id { get; set; }
            public string ClientName { get; set; }
            public string Plan { get; set; }
            public DateTime ExpirationDate { get; set; }
        }
    }
}