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

namespace ATLASS_FITNESS.Person
{
    public partial class FrmAddUpdatePerson : Form
    {
        
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private int _PersonID = -1;
        ClsPerson _Person;
        private OpenFileDialog openFileDialog1 = new OpenFileDialog();
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;

        public FrmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
    
        public FrmAddUpdatePerson(int personid)
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            _PersonID = personid;

        }
        private void _ResetDefualtValues()
        {
            if (_Mode == enMode.AddNew)
            {
                lbltitle.Text = "Add New Person";
                _Person = new ClsPerson();
            }

            else
            {
                lbltitle.Text = "Update Person";


            }
            pbPersonImage.Image = Properties.Resources.Male_512;
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtPhone.Text = "";
            dtpDateOfBirth.Value = DateTime.Now;
            if (pbPersonImage.Image == Properties.Resources.Male_512)
            {
                llRemoveImage.Visible = true;
                llSetImage.Visible = false;
            }
            else
            {
                llRemoveImage.Visible = false;
                llSetImage.Visible = true;
            }

        }
        private void _LoadPersonData()
        {
            _Person = ClsPerson.GetPersonByID(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("Person not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                lbltitle.Text = "Update Person";
                label1.Text = _Person.PersonID.ToString();
                txtFirstName.Text = _Person.FirstName;
                txtSecondName.Text = _Person.LastName;
                txtPhone.Text = _Person.phone;
                txtEmail.Text = _Person.Email;
                txtAddress.Text = _Person.Address;
                dtpDateOfBirth.Value = _Person.birthday;
                if (string.IsNullOrEmpty(_Person.ImagePath))
                {
                    pbPersonImage.Image = Properties.Resources.Male_512;

                }
                else
                {
                    pbPersonImage.ImageLocation = _Person.ImagePath;
                }


            }
        }
        private void pbPersonImage_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (pbPersonImage.Image == Properties.Resources.Male_512)
            {
                llRemoveImage.Visible = true;
                llSetImage.Visible = false;
            }
            else
            {
                llRemoveImage.Visible = false;
                llSetImage.Visible = true;
            }
        }

        private void FrmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if (_PersonID!=-1)
            {
                _LoadPersonData();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool ValidateFields()
        {
            // Validation du prénom
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter the first name", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstName.Focus();
                return false;
            }

            // Validation du nom
            if (string.IsNullOrWhiteSpace(txtSecondName.Text))
            {
                MessageBox.Show("Please enter the last name", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSecondName.Focus();
                return false;
            }

            // Validation du téléphone
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Please enter the phone number", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhone.Focus();
                return false;
            }

            // Validation de l'email (optionnel - vérification du format)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, emailPattern))
                {
                    MessageBox.Show("Please enter a valid email address", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.Focus();
                    return false;
                }
            }

            // Validation de la date de naissance
            if (dtpDateOfBirth.Value > DateTime.Now)
            {
                MessageBox.Show("Date of birth cannot be in the future", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpDateOfBirth.Focus();
                return false;
            }

            return true;
        }

        private int _validationAttempts = 0;

        public void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
            {
                _validationAttempts++;

                if (_validationAttempts >= 3)
                {
                    MessageBox.Show("Too many validation errors. Please check your data carefully.",
                        "Validation Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    _validationAttempts = 0; // Reset le compteur
                    Application.Exit(); // Ferme le formulaire
                    return;
                }

                return; // Attend que l'utilisateur corrige et reclique
            }

            // Si la validation réussit, on reset le compteur
            _validationAttempts = 0;

            _Person.FirstName = txtFirstName.Text;
            _Person.LastName = txtSecondName.Text;
            _Person.phone = txtPhone.Text;
            _Person.Email = txtEmail.Text;
            _Person.Address = txtAddress.Text;
            _Person.birthday = dtpDateOfBirth.Value;
            _Person.ImagePath = pbPersonImage.ImageLocation;

            if (_Person.save())
            {
                label1.Text = _Person.PersonID.ToString();
                MessageBox.Show("Person saved successfully", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (_Mode == enMode.AddNew)
                {
                    _Mode = enMode.Update;
                }
                lbltitle.Text = "Update Person";
                DataBack?.Invoke(this, _Person.PersonID);
            }
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Récupérer le chemin du fichier sélectionné
                string selectedFilePath = openFileDialog1.FileName;

                // Charger l'image dans la PictureBox
                pbPersonImage.Load(selectedFilePath);

                // Afficher le lien "Remove"
                llRemoveImage.Visible = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.Image = Properties.Resources.Male_512;    
            llRemoveImage.Visible = false;


        }
    }
}