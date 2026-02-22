using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATLASS_FITNESS_BUISNESS;
using Guna.UI2.WinForms;

namespace ATLASS_FITNESS
{
    public partial class PersonCard : UserControl
    {
        private ClsPerson person;
        private int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }
        }
        public ClsPerson selectedperson()
        {
                       return person;
        }
        public PersonCard()
        {
            InitializeComponent();
        }

        public void loadPersonInfo(int personid)
        {
            if (personid != -1)
            {
                _FillPersonInfo(personid);
            }
            else
            {
                _resetPersonInfo();
            }
        }
        private void _FillPersonInfo(int personid)
        {
            person = ClsPerson.GetPersonByID(personid);
            _PersonID = personid;



            if (person != null)
            {
                guna2TextBox1.Text = "  "+ _PersonID.ToString();
                firstname.Text = person.FirstName.ToString();
                Lastname.Text = person.LastName;
                Phone.Text = person.phone;
                Address.Text = person.Address;
                Email.Text = person.Email;
                date.Value = person.birthday;
                string imagePath = person.ImagePath?.Trim();

                if (string.IsNullOrWhiteSpace(imagePath))
                {
                    MessageBox.Show("Le chemin de l'image est vide ou null." +person.ImagePath);
                }
                else if (!System.IO.File.Exists(imagePath))
                {
                    MessageBox.Show("Le fichier image n'existe pas :\n" + imagePath);
                }
                else
                {
                    image.ImageLocation = imagePath;
                }

            }
            else
            {
                MessageBox.Show("No person found with ID: " + personid, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _resetPersonInfo();
            }
        }
    private void _resetPersonInfo()
        {
            _PersonID = -1;
           
            firstname.Text = "";
            Lastname.Text = "";
            Phone.Text = "";
            Address.Text = "";
            date.Text = DateTime.Now.ToShortDateString();
            image.ImageLocation = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }
    }

}
