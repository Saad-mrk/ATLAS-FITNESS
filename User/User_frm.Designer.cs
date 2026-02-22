namespace ATLASS_FITNESS.User
{
    partial class User_frm
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
            this.personCard1 = new ATLASS_FITNESS.PersonCard();
            this.SuspendLayout();
            // 
            // personCard1
            // 
            this.personCard1.Location = new System.Drawing.Point(0, 0);
            this.personCard1.Name = "personCard1";
            this.personCard1.Size = new System.Drawing.Size(688, 429);
            this.personCard1.TabIndex = 0;
            // 
            // User_frm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.personCard1);
            this.Name = "User_frm";
            this.Text = "User_frm";
            this.ResumeLayout(false);

        }

        #endregion

        private PersonCard personCard1;
    }
}