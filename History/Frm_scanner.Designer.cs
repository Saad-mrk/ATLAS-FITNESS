namespace ATLASS_FITNESS
{
    partial class Frm_scanner
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
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2CustomGradientPanel1 = new Guna.UI2.WinForms.Guna2CustomGradientPanel();
            this.picture_fichier = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btntekecharger_fichier = new Guna.UI2.WinForms.Guna2Button();
            this.lblarreter = new Guna.UI2.WinForms.Guna2Button();
            this.btndemarer = new Guna.UI2.WinForms.Guna2Button();
            this.pictureBoxCamera = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.guna2CustomGradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_fichier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.label1);
            this.guna2Panel1.Controls.Add(this.guna2PictureBox1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(905, 122);
            this.guna2Panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(270, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Scanner QR Code";
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.Image = global::ATLASS_FITNESS.Properties.Resources.barcode_scan;
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(48, 12);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(85, 90);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 2;
            this.guna2PictureBox1.TabStop = false;
            // 
            // guna2CustomGradientPanel1
            // 
            this.guna2CustomGradientPanel1.Controls.Add(this.picture_fichier);
            this.guna2CustomGradientPanel1.Controls.Add(this.btntekecharger_fichier);
            this.guna2CustomGradientPanel1.Controls.Add(this.lblarreter);
            this.guna2CustomGradientPanel1.Controls.Add(this.btndemarer);
            this.guna2CustomGradientPanel1.Controls.Add(this.pictureBoxCamera);
            this.guna2CustomGradientPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2CustomGradientPanel1.Location = new System.Drawing.Point(0, 122);
            this.guna2CustomGradientPanel1.Name = "guna2CustomGradientPanel1";
            this.guna2CustomGradientPanel1.Size = new System.Drawing.Size(905, 424);
            this.guna2CustomGradientPanel1.TabIndex = 1;
            this.guna2CustomGradientPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2CustomGradientPanel1_Paint);
            // 
            // picture_fichier
            // 
            this.picture_fichier.Image = global::ATLASS_FITNESS.Properties.Resources.qr_code_scan;
            this.picture_fichier.ImageRotate = 0F;
            this.picture_fichier.Location = new System.Drawing.Point(24, 95);
            this.picture_fichier.Name = "picture_fichier";
            this.picture_fichier.Size = new System.Drawing.Size(178, 208);
            this.picture_fichier.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picture_fichier.TabIndex = 4;
            this.picture_fichier.TabStop = false;
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
            this.btntekecharger_fichier.Location = new System.Drawing.Point(24, 330);
            this.btntekecharger_fichier.Name = "btntekecharger_fichier";
            this.btntekecharger_fichier.Size = new System.Drawing.Size(180, 45);
            this.btntekecharger_fichier.TabIndex = 3;
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
            this.lblarreter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblarreter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblarreter.ForeColor = System.Drawing.Color.White;
            this.lblarreter.Location = new System.Drawing.Point(497, 330);
            this.lblarreter.Name = "lblarreter";
            this.lblarreter.Size = new System.Drawing.Size(180, 45);
            this.lblarreter.TabIndex = 2;
            this.lblarreter.Text = "Arrater";
            this.lblarreter.Click += new System.EventHandler(this.lblarreter_Click);
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
            this.btndemarer.Location = new System.Drawing.Point(275, 330);
            this.btndemarer.Name = "btndemarer";
            this.btndemarer.Size = new System.Drawing.Size(180, 45);
            this.btndemarer.TabIndex = 1;
            this.btndemarer.Text = "demmeerer";
            this.btndemarer.Click += new System.EventHandler(this.btndemarer_Click);
            // 
            // pictureBoxCamera
            // 
            this.pictureBoxCamera.Image = global::ATLASS_FITNESS.Properties.Resources.qr_code_scan;
            this.pictureBoxCamera.ImageRotate = 0F;
            this.pictureBoxCamera.Location = new System.Drawing.Point(302, 61);
            this.pictureBoxCamera.Name = "pictureBoxCamera";
            this.pictureBoxCamera.Size = new System.Drawing.Size(336, 227);
            this.pictureBoxCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxCamera.TabIndex = 0;
            this.pictureBoxCamera.TabStop = false;
            // 
            // Frm_scanner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 546);
            this.Controls.Add(this.guna2CustomGradientPanel1);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "Frm_scanner";
            this.Text = "Frm_scanner";
            this.Load += new System.EventHandler(this.Frm_scanner_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.guna2CustomGradientPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picture_fichier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCamera)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2CustomGradientPanel guna2CustomGradientPanel1;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2PictureBox pictureBoxCamera;
        private Guna.UI2.WinForms.Guna2Button lblarreter;
        private Guna.UI2.WinForms.Guna2Button btndemarer;
        private Guna.UI2.WinForms.Guna2PictureBox picture_fichier;
        private Guna.UI2.WinForms.Guna2Button btntekecharger_fichier;
    }
}