using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATLASS_FITNESS_BUISNESS;
using Guna.UI2.WinForms;

namespace ATLASS_FITNESS
{
    public static class Clsglobal
    {
        public static ClsUser CurrentUser;
        public static bool RemembermeUsernameAndPassword(string useername, string passsword)
        {
            try
            {
                string currentdirectory = Directory.GetCurrentDirectory();
                string filePath = currentdirectory + "\\data.txt";
                if (useername == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                string datatosave = useername + "|" + passsword;
                File.WriteAllText(filePath, datatosave);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERREUR : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


        }
        public static bool GetRemembermeUsernameAndPassword(ref string username, ref string password)
        {
            try
            {
                string currentdirectory = Directory.GetCurrentDirectory();
                string filePath = currentdirectory + "\\data.txt";
                if (!File.Exists(filePath))
                {
                    return false;
                }
                string dataloaded = File.ReadAllText(filePath);
                string[] parts = dataloaded.Split('|');
                if (parts.Length != 2)
                {
                    return false;
                }
                username = parts[0];
                password = parts[1];
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERREUR : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        public static DialogResult GunaDialog(
        string text,
        string caption,
        MessageDialogIcon icon,
        MessageDialogButtons buttons,
        MessageDialogStyle style)
        {
            Guna2MessageDialog dialog = new Guna2MessageDialog();

            dialog.Text = text;
            dialog.Caption = caption;
            dialog.Icon = icon;
            dialog.Buttons = buttons;
            dialog.Style = style;

            return dialog.Show();
        }

    }
}
