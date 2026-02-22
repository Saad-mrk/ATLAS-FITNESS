using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ATLASS_FITNESS_BUISNESS;
using DocumentFormat.OpenXml.Drawing;

namespace ATLASS_FITNESS
{
    public partial class frm_history : Form
    {
        private DataTable dtAttendances;

        public frm_history()
        {
            InitializeComponent();

            // ✅ S'ABONNER À L'ÉVÉNEMENT
            AttendanceEvents.AttendanceRecorded += OnAttendanceRecorded;
        }

        private void frm_history_Load_1(object sender, EventArgs e)
        {
            LoadAttendances();
        }

        // ✅ GESTIONNAIRE D'ÉVÉNEMENT
        private void OnAttendanceRecorded(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => LoadAttendances()));
            }
            else
            {
                LoadAttendances();
            }
        }

        private void LoadAttendances()
        {
            try
            {
                dtAttendances = ClsAttendance.GetAttendancesForDisplay();
                guna2DataGridView1.DataSource = dtAttendances;

                if (dtAttendances != null && dtAttendances.Rows.Count > 0)
                {
                    FormatDataGridView();
                    lblTotal.Text = $"Total: {dtAttendances.Rows.Count} présence(s)";
                }
                else
                {
                    lblTotal.Text = "Total: 0 présence";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur:\n{ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ SE DÉSABONNER LORS DE LA FERMETURE
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            AttendanceEvents.AttendanceRecorded -= OnAttendanceRecorded;
            base.OnFormClosing(e);
        }

        private void FormatDataGridView()
        {
            guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            guna2DataGridView1.MultiSelect = false;
            guna2DataGridView1.ReadOnly = true;
            guna2DataGridView1.AllowUserToAddRows = false;
            guna2DataGridView1.RowHeadersVisible = false;
            guna2DataGridView1.ScrollBars = ScrollBars.Both;
            guna2DataGridView1.BorderStyle = BorderStyle.None;
            guna2DataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            guna2DataGridView1.GridColor = Color.FromArgb(240, 240, 240);
            guna2DataGridView1.BackgroundColor = Color.White;

            // Padding & Font
            guna2DataGridView1.DefaultCellStyle.Padding = new Padding(8, 0, 8, 0);
            guna2DataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            guna2DataGridView1.DefaultCellStyle.BackColor = Color.White;
            guna2DataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(26, 26, 46);
            guna2DataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(238, 243, 254);
            guna2DataGridView1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(26, 26, 46);

            // Header style — bleu clair (#f0f4ff)
            guna2DataGridView1.ColumnHeadersHeight = 42;
            guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            guna2DataGridView1.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(240, 244, 255);
            guna2DataGridView1.ThemeStyle.HeaderStyle.ForeColor = Color.FromArgb(136, 136, 136);
            guna2DataGridView1.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // Rows
            guna2DataGridView1.RowTemplate.Height = 40;
            guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = Color.FromArgb(248, 250, 255);

            if (guna2DataGridView1.Columns.Count == 0) return;

            // Cacher ID
            if (guna2DataGridView1.Columns["ID"] != null)
                guna2DataGridView1.Columns["ID"].Visible = false;

            // Client
            if (guna2DataGridView1.Columns["Client"] != null)
            {
                guna2DataGridView1.Columns["Client"].HeaderText = "CLIENT";
                guna2DataGridView1.Columns["Client"].MinimumWidth = 100;
                guna2DataGridView1.Columns["Client"].FillWeight = 25;
                guna2DataGridView1.Columns["Client"].DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                guna2DataGridView1.Columns["Client"].DefaultCellStyle.ForeColor = Color.FromArgb(26, 26, 46);
            }

            // Téléphone
            if (guna2DataGridView1.Columns["Telephone"] != null)
            {
                guna2DataGridView1.Columns["Telephone"].HeaderText = "TÉLÉPHONE";
                guna2DataGridView1.Columns["Telephone"].MinimumWidth = 100;
                guna2DataGridView1.Columns["Telephone"].FillWeight = 15;
                guna2DataGridView1.Columns["Telephone"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                guna2DataGridView1.Columns["Telephone"].DefaultCellStyle.ForeColor = Color.FromArgb(136, 136, 136);
            }

            // Session — badge bleu
            if (guna2DataGridView1.Columns["Session"] != null)
            {
                guna2DataGridView1.Columns["Session"].HeaderText = "SESSION";
                guna2DataGridView1.Columns["Session"].MinimumWidth = 80;
                guna2DataGridView1.Columns["Session"].FillWeight = 15;
                guna2DataGridView1.Columns["Session"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                guna2DataGridView1.Columns["Session"].DefaultCellStyle.ForeColor = Color.FromArgb(79, 142, 247);
                guna2DataGridView1.Columns["Session"].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }

            // Date
            if (guna2DataGridView1.Columns["Date"] != null)
            {
                guna2DataGridView1.Columns["Date"].HeaderText = "DATE";
                guna2DataGridView1.Columns["Date"].MinimumWidth = 90;
                guna2DataGridView1.Columns["Date"].FillWeight = 12;
                guna2DataGridView1.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                guna2DataGridView1.Columns["Date"].DefaultCellStyle.ForeColor = Color.FromArgb(170, 170, 170);
            }

            // Entrée — vert
            if (guna2DataGridView1.Columns["HeureEntree"] != null)
            {
                guna2DataGridView1.Columns["HeureEntree"].HeaderText = "ENTRÉE";
                guna2DataGridView1.Columns["HeureEntree"].MinimumWidth = 70;
                guna2DataGridView1.Columns["HeureEntree"].FillWeight = 10;
                guna2DataGridView1.Columns["HeureEntree"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                guna2DataGridView1.Columns["HeureEntree"].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                guna2DataGridView1.Columns["HeureEntree"].DefaultCellStyle.ForeColor = Color.FromArgb(39, 174, 96);
            }

            // Sortie — rouge
            if (guna2DataGridView1.Columns["HeureSortie"] != null)
            {
                guna2DataGridView1.Columns["HeureSortie"].HeaderText = "SORTIE";
                guna2DataGridView1.Columns["HeureSortie"].MinimumWidth = 70;
                guna2DataGridView1.Columns["HeureSortie"].FillWeight = 10;
                guna2DataGridView1.Columns["HeureSortie"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                guna2DataGridView1.Columns["HeureSortie"].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                guna2DataGridView1.Columns["HeureSortie"].DefaultCellStyle.ForeColor = Color.FromArgb(231, 76, 60);
            }

            // Statut
            if (guna2DataGridView1.Columns["Statut"] != null)
            {
                guna2DataGridView1.Columns["Statut"].HeaderText = "STATUT";
                guna2DataGridView1.Columns["Statut"].MinimumWidth = 80;
                guna2DataGridView1.Columns["Statut"].FillWeight = 10;
                guna2DataGridView1.Columns["Statut"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                guna2DataGridView1.Columns["Statut"].DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }

            // Durée
            if (guna2DataGridView1.Columns["Duree"] != null)
            {
                guna2DataGridView1.Columns["Duree"].HeaderText = "DURÉE";
                guna2DataGridView1.Columns["Duree"].MinimumWidth = 60;
                guna2DataGridView1.Columns["Duree"].FillWeight = 8;
                guna2DataGridView1.Columns["Duree"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                guna2DataGridView1.Columns["Duree"].DefaultCellStyle.ForeColor = Color.FromArgb(170, 170, 170);
            }

            ApplyStatutColors();
        }

        // Colorer les lignes selon le statut
        private void ApplyStatutColors()
        {
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                if (row.Cells["Statut"]?.Value == null) continue;

                string statut = row.Cells["Statut"].Value.ToString().Trim();

                if (statut == "Présent")
                {
                    row.Cells["Statut"].Style.ForeColor = Color.FromArgb(39, 174, 96);   // vert
                    row.Cells["Statut"].Style.BackColor = Color.FromArgb(234, 250, 241); // fond vert clair
                }
                else if (statut == "Sorti")
                {
                    row.Cells["Statut"].Style.ForeColor = Color.FromArgb(230, 126, 34);  // orange
                    row.Cells["Statut"].Style.BackColor = Color.FromArgb(254, 249, 231); // fond orange clair
                }
            }
        }



        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAttendances();
        }

        private void btnFilterToday_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtAttendances != null && dtAttendances.Rows.Count > 0)
                {
                    string today = DateTime.Now.ToString("dd/MM/yyyy");
                    dtAttendances.DefaultView.RowFilter = $"Date = '{today}'";
                    lblTotal.Text = $"Aujourd'hui: {dtAttendances.DefaultView.Count} présence(s)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur:\n{ex.Message}", "Erreur");
            }
        }

        private void btnFilterSession_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtAttendances != null)
                {
                    dtAttendances.DefaultView.RowFilter = "";
                    LoadAttendances();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur:\n{ex.Message}", "Erreur");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtAttendances != null && dtAttendances.Rows.Count > 0)
                {
                    string filter = txtSearch.Text.Trim();

                    if (string.IsNullOrEmpty(filter))
                    {
                        dtAttendances.DefaultView.RowFilter = "";
                    }
                    else
                    {
                        string safeFilter = filter.Replace("'", "''");
                        dtAttendances.DefaultView.RowFilter =
                            $"Client LIKE '%{safeFilter}%' OR Telephone LIKE '%{safeFilter}%' OR Session LIKE '%{safeFilter}%'";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur:\n{ex.Message}", "Erreur");
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Frm_scanner frmm = new Frm_scanner();
            frmm.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Frm_Scanner_Sortie sor = new Frm_Scanner_Sortie();
            sor.Show();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            // Cette méthode est liée à l'événement TextChanged de txtSearch
            try
            {
                if (dtAttendances != null && dtAttendances.Rows.Count > 0)
                {
                    string filter = txtSearch.Text.Trim();
                    if (string.IsNullOrEmpty(filter))
                    {
                        dtAttendances.DefaultView.RowFilter = "";
                    }
                    else
                    {
                        string safeFilter = filter.Replace("'", "''");
                        dtAttendances.DefaultView.RowFilter =
                            $"Client LIKE '%{safeFilter}%' OR Telephone LIKE '%{safeFilter}%' OR Session LIKE '%{safeFilter}%'";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur:\n{ex.Message}", "Erreur");
            }
        }

        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( guna2ComboBox2.SelectedItem == "Présent")
            {
                dtAttendances.DefaultView.RowFilter = "Statut = 'Présent'";
                lblTotal.Text = $"Présent: {dtAttendances.DefaultView.Count} présence(s)";


            }
            else if (guna2ComboBox2.SelectedItem == "Sorti")
            {
                dtAttendances.DefaultView.RowFilter = "Statut = 'Sorti'";
                lblTotal.Text = $"Sorti: {dtAttendances.DefaultView.Count} présence(s)";
            }
             else if (guna2ComboBox2.SelectedItem == "status")
            {
                try
                {
                    if (dtAttendances != null)
                    {
                        dtAttendances.DefaultView.RowFilter = "";
                        LoadAttendances();
                        lblTotal.Text = $"Total: {dtAttendances.Rows.Count} présence(s)";

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur:\n{ex.Message}", "Erreur");
                }
            }
        }
        private void filtredate()
        {
            try
            {
                if (dtAttendances != null && dtAttendances.Rows.Count > 0)
                {
                    string selectedDatestart = guna2DateTimePicker2.Value.ToString("dd/MM/yyyy");
                    string selectedDateend = guna2DateTimePicker1.Value.ToString("dd/MM/yyyy");
                    dtAttendances.DefaultView.RowFilter = $"Date >= '{selectedDatestart}' AND Date <= '{selectedDateend}'";
                    lblTotal.Text = $"Du {selectedDatestart} au {selectedDateend}: {dtAttendances.DefaultView.Count} présence(s)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur:\n{ex.Message}", "Erreur");
            }
        }
        private void guna2DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            // Filtrer par date
            filtredate();
        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            filtredate();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            guna2DateTimePicker2.Value = DateTime.Now;
            guna2DateTimePicker1.Value = DateTime.Now.AddDays(-30);
             filtredate();

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}