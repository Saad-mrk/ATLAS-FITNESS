using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATLASS_FITNESS_BUISNESS;
using Guna.UI2.WinForms;

namespace ATLASS_FITNESS
{
    public partial class Frm_Profilcs : Form
    {
        ClsUser clsuse = Clsglobal.CurrentUser;
        public Frm_Profilcs()
        {
            InitializeComponent();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            //ouvrir fichier pour chamger la photo
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Charger l'image sélectionnée dans le PictureBox
                string selectedImagePath = openFileDialog.FileName;
                guna2CirclePictureBox1.Image = Image.FromFile(selectedImagePath);
            }

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            clsuse.UserName = txtusername.Text;
            clsuse.UserPerson.phone = txtphone.Text;
            clsuse.UserPerson.Email = txtemail.Text;
            if (clsuse.UpdateUserInfo())
            {
                MessageBox.Show("Profile updated successfully!");
            }
            else
            {
                MessageBox.Show("Failed to update profile. Please try again.");


            }
        }
            private void _FILLdata()
            {
                txtusername.Text = clsuse.UserName;
                txtemail.Text = clsuse.UserPerson.Email;
                txtphone.Text = clsuse.UserPerson.phone;
                if (!string.IsNullOrEmpty(clsuse.UserPerson.ImagePath))
                {
                    try
                    {
                        guna2CirclePictureBox1.Image = Image.FromFile(clsuse.UserPerson.ImagePath);
                    }
                    catch (Exception ex)
                    {
                    Clsglobal.GunaDialog("Failed to load profile picture: ",  "Error", MessageDialogIcon.Information, MessageDialogButtons.OK,
                                            MessageDialogStyle.Light);
                    }
                }
        }

        private void Frm_Profilcs_Load(object sender, EventArgs e)
        {
            _FILLdata();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }
