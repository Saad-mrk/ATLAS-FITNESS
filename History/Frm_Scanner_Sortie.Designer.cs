namespace ATLASS_FITNESS
{
    partial class Frm_Scanner_Sortie
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btntekecharger_fichier = new Guna.UI2.WinForms.Guna2Button();
            this.lblarreter = new Guna.UI2.WinForms.Guna2Button();
            this.btndemarer = new Guna.UI2.WinForms.Guna2Button();
            this.picture_fichier = new Guna.UI2.WinForms.Guna2PictureBox();
            this.pictureBoxCamera = new Guna.UI2.WinForms.Guna2PictureBox();
            this.dataGridViewPresents = new Guna.UI2.WinForms.Guna2DataGridView();
            this.lbltotale = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picture_fichier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPresents)).BeginInit();
            this.SuspendLayout();
            // 
            // btntekecharger_fichier
            // 
            this.btntekecharger_fichier.BorderRadius = 15;
            this.btntekecharger_fichier.BorderThickness = 1;
            this.btntekecharger_fichier.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btntekecharger_fichier.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btntekecharger_fichier.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btntekecharger_fichier.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btntekecharger_fichier.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btntekecharger_fichier.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btntekecharger_fichier.ForeColor = System.Drawing.Color.White;
            this.btntekecharger_fichier.Location = new System.Drawing.Point(12, 232);
            this.btntekecharger_fichier.Name = "btntekecharger_fichier";
            this.btntekecharger_fichier.Size = new System.Drawing.Size(180, 45);
            this.btntekecharger_fichier.TabIndex = 8;
            this.btntekecharger_fichier.Text = "Scanner depuis un fichier ";
            this.btntekecharger_fichier.Click += new System.EventHandler(this.btntekecharger_fichier_Click);
            // 
            // lblarreter
            // 
            this.lblarreter.BorderRadius = 15;
            this.lblarreter.BorderThickness = 1;
            this.lblarreter.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.lblarreter.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.lblarreter.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.lblarreter.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.lblarreter.Enabled = false;
            this.lblarreter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblarreter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblarreter.ForeColor = System.Drawing.Color.White;
            this.lblarreter.Location = new System.Drawing.Point(586, 249);
            this.lblarreter.Name = "lblarreter";
            this.lblarreter.Size = new System.Drawing.Size(180, 45);
            this.lblarreter.TabIndex = 7;
            this.lblarreter.Text = "Arrater";
            this.lblarreter.Click += new System.EventHandler(this.lblarreter_Click_1);
            // 
            // btndemarer
            // 
            this.btndemarer.BorderRadius = 15;
            this.btndemarer.BorderThickness = 1;
            this.btndemarer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btndemarer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btndemarer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btndemarer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btndemarer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btndemarer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btndemarer.ForeColor = System.Drawing.Color.White;
            this.btndemarer.Location = new System.Drawing.Point(346, 249);
            this.btndemarer.Name = "btndemarer";
            this.btndemarer.Size = new System.Drawing.Size(180, 45);
            this.btndemarer.TabIndex = 6;
            this.btndemarer.Text = "demmeerer";
            this.btndemarer.Click += new System.EventHandler(this.btndemarer_Click_1);
            // 
            // picture_fichier
            // 
            this.picture_fichier.Image = global::ATLASS_FITNESS.Properties.Resources.qr_code_scan;
            this.picture_fichier.ImageRotate = 0F;
            this.picture_fichier.Location = new System.Drawing.Point(34, 12);
            this.picture_fichier.Name = "picture_fichier";
            this.picture_fichier.Size = new System.Drawing.Size(178, 208);
            this.picture_fichier.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picture_fichier.TabIndex = 9;
            this.picture_fichier.TabStop = false;
            // 
            // pictureBoxCamera
            // 
            this.pictureBoxCamera.Image = global::ATLASS_FITNESS.Properties.Resources.qr_code_scan;
            this.pictureBoxCamera.ImageRotate = 0F;
            this.pictureBoxCamera.Location = new System.Drawing.Point(406, 12);
            this.pictureBoxCamera.Name = "pictureBoxCamera";
            this.pictureBoxCamera.Size = new System.Drawing.Size(293, 171);
            this.pictureBoxCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCamera.TabIndex = 5;
            this.pictureBoxCamera.TabStop = false;
            // 
            // dataGridViewPresents
            // 
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.dataGridViewPresents.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPresents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewPresents.ColumnHeadersHeight = 4;
            this.dataGridViewPresents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewPresents.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewPresents.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewPresents.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dataGridViewPresents.Location = new System.Drawing.Point(0, 300);
            this.dataGridViewPresents.Name = "dataGridViewPresents";
            this.dataGridViewPresents.RowHeadersVisible = false;
            this.dataGridViewPresents.RowHeadersWidth = 51;
            this.dataGridViewPresents.RowTemplate.Height = 24;
            this.dataGridViewPresents.Size = new System.Drawing.Size(800, 150);
            this.dataGridViewPresents.TabIndex = 10;
            this.dataGridViewPresents.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dataGridViewPresents.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dataGridViewPresents.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dataGridViewPresents.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dataGridViewPresents.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dataGridViewPresents.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dataGridViewPresents.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dataGridViewPresents.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dataGridViewPresents.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridViewPresents.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewPresents.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridViewPresents.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dataGridViewPresents.ThemeStyle.HeaderStyle.Height = 4;
            this.dataGridViewPresents.ThemeStyle.ReadOnly = false;
            this.dataGridViewPresents.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dataGridViewPresents.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewPresents.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewPresents.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dataGridViewPresents.ThemeStyle.RowsStyle.Height = 24;
            this.dataGridViewPresents.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dataGridViewPresents.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // lbltotale
            // 
            this.lbltotale.AutoSize = true;
            this.lbltotale.Location = new System.Drawing.Point(714, 105);
            this.lbltotale.Name = "lbltotale";
            this.lbltotale.Size = new System.Drawing.Size(44, 16);
            this.lbltotale.TabIndex = 11;
            this.lbltotale.Text = "label1";
            // 
            // Frm_Scanner_Sortie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbltotale);
            this.Controls.Add(this.dataGridViewPresents);
            this.Controls.Add(this.picture_fichier);
            this.Controls.Add(this.btntekecharger_fichier);
            this.Controls.Add(this.lblarreter);
            this.Controls.Add(this.btndemarer);
            this.Controls.Add(this.pictureBoxCamera);
            this.Name = "Frm_Scanner_Sortie";
            this.Text = "Frm_Scanner_Sortie";
            this.Load += new System.EventHandler(this.Frm_Scanner_Sortie_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picture_fichier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPresents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox picture_fichier;
        private Guna.UI2.WinForms.Guna2Button btntekecharger_fichier;
        private Guna.UI2.WinForms.Guna2Button lblarreter;
        private Guna.UI2.WinForms.Guna2Button btndemarer;
        private Guna.UI2.WinForms.Guna2PictureBox pictureBoxCamera;
        private Guna.UI2.WinForms.Guna2DataGridView dataGridViewPresents;
        private System.Windows.Forms.Label lbltotale;
    }
}