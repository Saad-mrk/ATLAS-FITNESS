using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ATLASS_FITNESS.Person;
using ATLASS_FITNESS_BUISNESS;
using ClosedXML.Excel;

namespace ATLASS_FITNESS.Client
{
    public partial class Client : Form
    {
        private DataTable _dataclient;
        private DataTable _fullDataClient; // Toutes les données

        // Variables de pagination
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalRecords = 0;
        private int totalPages = 0;

        public Client()
        {
            InitializeComponent();
        }
      
        private void Client_Load(object sender, EventArgs e)
        {
            try
            {
                pnlfooterfiil.Parent= guna2Panel1;
                pnlfooterfiil.Dock = DockStyle.Fill;



                LoadClientData();
                cmsClient.Cursor = Cursors.Hand;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors du chargement des clients : {ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadClientData()
        {
            _fullDataClient = ClsClient.GetAllClient();

            if (_fullDataClient == null || _fullDataClient.Rows.Count == 0)
            {
                MessageBox.Show("Aucun client trouvé.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            totalRecords = _fullDataClient.Rows.Count;
            CalculateTotalPages();

            ConfigureDataGridView();
            DisplayCurrentPage();
            UpdatePaginationControls();
        }

        private void CalculateTotalPages()
        {
            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
        }

        private void DisplayCurrentPage()
        {
            // Calculer les index de début et fin
            int startIndex = (currentPage - 1) * pageSize;
            int endIndex = Math.Min(startIndex + pageSize, totalRecords);

            // Créer une table pour la page actuelle
            _dataclient = _fullDataClient.Clone();

            // Copier uniquement les lignes de la page actuelle
            for (int i = startIndex; i < endIndex; i++)
            {
                _dataclient.ImportRow(_fullDataClient.Rows[i]);
            }
            if (_dataclient.Rows.Count == 0)
            {
                MessageBox.Show("Aucun client à afficher pour cette page.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Remplir le DataGridView
            FillDataGridView();
        }

        private void ConfigureDataGridView()
        {
            // ✅ Vider les colonnes existantes pour éviter les doublons
            DGVClient.Columns.Clear();
            DGVClient.Rows.Clear();
            DGVClient.RowTemplate.Height = 40;
            DGVClient.ColumnHeadersHeight = 50;

            DGVClient.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DGVClient.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            DGVClient.DefaultCellStyle.Padding = new Padding(5, 2, 5, 2);
            DGVClient.AutoGenerateColumns = false;

            // ✅ CHANGEMENT ICI : Remplir automatiquement la largeur disponible
            DGVClient.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            DGVClient.AllowUserToAddRows = false;

            // Créer les colonnes
            DGVClient.Columns.Add("ClientID", "ID");
            DGVClient.Columns.Add("Last_Name", "Nom");
            DGVClient.Columns.Add("First_Name", "Prénom");
            DGVClient.Columns.Add("Phone", "Téléphone");
            DGVClient.Columns.Add("Registration_Date", "Inscription");
            DGVClient.Columns.Add("Last_Seance", "Dernière séance");

            // Colonne checkbox
            DataGridViewCheckBoxColumn isActiveCol = new DataGridViewCheckBoxColumn
            {
                Name = "Is_Active",
                HeaderText = "Actif"
            };
            DGVClient.Columns.Add(isActiveCol);

            // Colonne image
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn
            {
                Name = "Image",
                HeaderText = "Photo",
                ImageLayout = DataGridViewImageCellLayout.Zoom
            };
            DGVClient.Columns.Add(imgCol);
        }
        private void FillDataGridView()
        {
            // Vider les lignes existantes
            DGVClient.Rows.Clear();

            foreach (DataRow row in _dataclient.Rows)
            {
                Image img = LoadClientImage(row["image_path"]);

                DGVClient.Rows.Add(
                    row["ClientID"],
                    row["Last_Name"],
                    row["First_Name"],
                    row["phone"],
                    ConvertToDate(row["registration_date"]),
                    ConvertToDate(row["last_seance"]),
                    row["is_active"] != DBNull.Value && Convert.ToBoolean(row["is_active"]),
                    img
                );
            }
        }

        private void UpdatePaginationControls()
        {
            // Mettre à jour le label d'information
            
                int startRecord = (currentPage - 1) * pageSize + 1;
                int endRecord = Math.Min(currentPage * pageSize, totalRecords);

                lblPageInfo.Text = $"Page {currentPage} sur {totalPages} ({startRecord}-{endRecord} sur {totalRecords})";
            

            // Activer/Désactiver les boutons
            if (n1 != null)
                n1.Enabled = currentPage > 1;

            if (btnprec != null)
                btnprec.Enabled = currentPage > 1;

            if (btsuivant != null)
                btsuivant.Enabled = currentPage < totalPages;

            if (n5 != null)
                n5.Enabled = currentPage < totalPages;
        }

       
     

        private void cmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                currentPage = 1; // Retour à la première page
                CalculateTotalPages();
                DisplayCurrentPage();
                UpdatePaginationControls();
            
        }

        // ========== MÉTHODES EXISTANTES ==========

        private Image LoadClientImage(object imagePath)
        {
            try
            {
                if (imagePath != DBNull.Value && !string.IsNullOrEmpty(imagePath.ToString()))
                {
                    string path = imagePath.ToString();
                    if (System.IO.File.Exists(path))
                    {
                        return Image.FromFile(path);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur chargement image : {ex.Message}");
            }

            return null;
        }

        private string ConvertToDate(object dateValue)
        {
            try
            {
                if (dateValue != DBNull.Value)
                {
                    return Convert.ToDateTime(dateValue).ToString("dd/MM/yyyy");
                }
            }
            catch { }

            return "N/A";
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            if (DGVClient.CurrentRow == null)
            {
                MessageBox.Show("Veuillez sélectionner un client.", "Attention",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DGVClient.CurrentRow.Cells["ClientID"].Value == null)
            {
                MessageBox.Show("ID client invalide.", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int clientId = Convert.ToInt32(DGVClient.CurrentRow.Cells["ClientID"].Value);
            FrmClientInfo frm = new FrmClientInfo(clientId);
            frm.ShowDialog();
        }

        private void DGVClient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            frmAddUpdateClient frm = new frmAddUpdateClient();
            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();
        }

        private void _refreshload()
        {
            currentPage = 1; // Retour à la première page après actualisation
            LoadClientData();
            this.Refresh();
        }

        private void Frm_DataBack(object sender, int personID)
        {
            MessageBox.Show($"ID de la personne reçue : {personID}", "Data Back",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            _refreshload();
        }

       

  
        // Ces méthodes semblent être des raccourcis pour aller directement à une page spécifique
        private void guna2Button8_Click_1(object sender, EventArgs e)
        {
            currentPage = 2;
            DisplayCurrentPage();
            UpdatePaginationControls();

        }

        private void n1_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            DisplayCurrentPage();
            UpdatePaginationControls();

        }

        private void guna2Button7_Click_1(object sender, EventArgs e)
        {
            currentPage = 3;
            DisplayCurrentPage();
            UpdatePaginationControls();


        }

        private void guna2Button6_Click_1(object sender, EventArgs e)
        {
            currentPage = 4;
            DisplayCurrentPage();
            UpdatePaginationControls();

        }

        private void n5_Click_1(object sender, EventArgs e)
        {
            currentPage = totalPages;
            DisplayCurrentPage();
            UpdatePaginationControls();

        }

        private void btsuivant_Click_1(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                DisplayCurrentPage();
                UpdatePaginationControls();
            }

        }

        private void btnprec_Click_1(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayCurrentPage();
                UpdatePaginationControls();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchClient.Text.ToLower();

            foreach (DataGridViewRow row in DGVClient.Rows)
            {
                bool visible =
                    row.Cells["First_Name"].Value.ToString().ToLower().Contains(keyword) ||
                    row.Cells["Last_Name"].Value.ToString().ToLower().Contains(keyword);

                row.Visible = visible;
            }

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_fullDataClient == null || _fullDataClient.Rows.Count == 0)
                {
                    MessageBox.Show("Aucune donnée à exporter !");
                    return;
                }

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Workbook|*.xlsx";
                    sfd.FileName = "Clients_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            // 🔥 Créer une copie sans la colonne image_path
                            DataTable exportTable = _fullDataClient.Copy();

                            if (exportTable.Columns.Contains("image_path"))
                                exportTable.Columns.Remove("image_path");

                            workbook.Worksheets.Add(exportTable, "Clients");

                            workbook.SaveAs(sfd.FileName);
                        }

                        MessageBox.Show("Exportation réussie !",
                            "Succès",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.ToString());
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            int clientId = Convert.ToInt32(DGVClient.CurrentRow.Cells["ClientID"].Value);

            frmAddUpdateClient frm = new frmAddUpdateClient(clientId);
            frm.DataBack += Frm_DataBack;
            frm.ShowDialog();
        }
    }
}