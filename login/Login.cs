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

namespace ATLASS_FITNESS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            string username = "";
            string password = "";
            if (Clsglobal.GetRemembermeUsernameAndPassword(ref username, ref password))
            {
                txtusername.Text = username;
                txtpassword.Text = password;
                guna2ToggleSwitch1.Checked = true;
            }
            else
            {
                guna2ToggleSwitch1.Checked = false;
            }
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text.Trim();
            string password = txtpassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Veuillez saisir le nom d'utilisateur et le mot de passe.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {       
                ClsUser user = ClsUser.GetUserInfoByUsernameAndPassword(username, password);
                if (user == null)
                {
                    MessageBox.Show("Nom d'utilisateur ou mot de passe invalide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtpassword.Clear();        
                    txtpassword.Focus();
                    return;
                }
                else { 
                    if (guna2ToggleSwitch1.Checked)
                    {
                        Clsglobal.RemembermeUsernameAndPassword(username, password);
                    }
                    else
                    {
                        Clsglobal.RemembermeUsernameAndPassword("", "");
                    }
                }

                if (!user.IsActive)
                {
                    MessageBox.Show("Ce compte est désactivé. Veuillez contacter l'administrateur.", "Accès refusé", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Clsglobal.CurrentUser = user;

                MessageBox.Show("Connexion réussie !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Ouvrir la fenêtre principale et fermer Login quand elle se ferme
                this.Hide();
                var main = new Loading();
                main.FormClosed += (s, args) => this.Close();
                main.Show();
            }
            catch (Exception)
            {
                MessageBox.Show("Une erreur est survenue lors de la connexion.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
