using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using ATLASS_FITNESS.Notifications;
using ATLASS_FITNESS_BUISNESS;
using Guna.UI2.WinForms;

namespace ATLASS_FITNESS
{
    public partial class Form1 : Form
    {
        bool isMenuExpanded = true;
        int expandedWidth = 190;
        int collapsedWidth = 55;
        private Refresher refresher;
        Login Login = new Login();

        // ============ GESTIONNAIRES DE NOTIFICATIONS ============
        private ClsNotification notificationManager;
        private Frm_notification currentNotificationForm = null;
        private Timer notificationRefreshTimer;
        private NotificationToastManager toastManager;
        private SessionsNotificationManager sessionsNotificationManager;

        public Form1()
        {
            InitializeComponent();
            loadImageUser();
            refresher = new Refresher();
            InitializeNotificationSystem();
        }

        private void InitializeNotificationSystem()
        {
            notificationManager = new ClsNotification();
            toastManager = new NotificationToastManager(this);

            if (toastManager != null)
            {
                toastManager.OnToastClicked += (notif) => OpenNotificationForm();
            }

            // Initialiser le gestionnaire de notifications de sessions
            sessionsNotificationManager = new SessionsNotificationManager(toastManager);
            sessionsNotificationManager.NotificationCreated += (s, e) => UpdateNotificationBadge();
            sessionsNotificationManager.Start();

            // Timer pour rafraîchir le badge toutes les 30 secondes
            notificationRefreshTimer = new Timer();
            notificationRefreshTimer.Interval = 30000;
            notificationRefreshTimer.Tick += (s, e) => UpdateNotificationBadge();
            notificationRefreshTimer.Start();
        }

        private void loadImageUser()
        {
            if (Clsglobal.CurrentUser != null)
            {
                MessageBox.Show("Welcome back, " + Clsglobal.CurrentUser.UserName + "!",
                    "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ImageUser.Text = Clsglobal.CurrentUser.UserName.ToString();
                string path = Clsglobal.CurrentUser.UserPerson._ImagePath;

                if (System.IO.File.Exists(path))
                {
                    ImageUser.Image = Image.FromFile(path);
                }
            }
        }

        // ============ MISE À JOUR DU BADGE ============
        private void UpdateNotificationBadge()
        {
            try
            {
                if (notificationManager == null) return;

                int unreadCount = notificationManager.GetAll().Count(n => !n.IsRead);

                if (unreadCount > 0)
                {
                    lblNotificationBadge.Text = unreadCount > 99 ? "99+" : unreadCount.ToString();
                    lblNotificationBadge.Visible = true;
                    lblNotificationBadge.BackColor = Color.Red;
                    lblNotificationBadge.ForeColor = Color.White;
                    lblNotificationBadge.AutoSize = false;
                    lblNotificationBadge.Width = 20;
                    lblNotificationBadge.Height = 20;
                    lblNotificationBadge.TextAlign = ContentAlignment.MiddleCenter;
                    btnNotification.Image = Properties.Resources.notification_bell;
                }
                else
                {
                    lblNotificationBadge.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur UpdateNotificationBadge: {ex.Message}");
            }
        }

        // ============ GESTION DU POPUP DE NOTIFICATIONS ============
        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            if (currentNotificationForm != null && !currentNotificationForm.IsDisposed)
            {
                CloseNotificationForm();
                return;
            }
            else
            {
                OpenNotificationForm();
            }

        }

        private void OpenNotificationForm()
        {
            try
            {
                currentNotificationForm = new Frm_notification();
                currentNotificationForm.FormBorderStyle = FormBorderStyle.None;
                currentNotificationForm.StartPosition = FormStartPosition.Manual;

                Point buttonLocation = btnNotification.PointToScreen(Point.Empty);
                currentNotificationForm.Location = new Point(
                    buttonLocation.X + btnNotification.Width - currentNotificationForm.Width,
                    buttonLocation.Y + btnNotification.Height + 5
                );

                currentNotificationForm.Show();
                currentNotificationForm.Owner = this;

                currentNotificationForm.Deactivate += NotificationForm_Deactivate;
                this.Deactivate += MainForm_Deactivate;

                currentNotificationForm.FormClosed += (s, e) =>
                {

                    currentNotificationForm = null;
                    UpdateNotificationBadge();
                    this.Deactivate -= MainForm_Deactivate;
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur ouverture notifications: {ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NotificationForm_Deactivate(object sender, EventArgs e)
        {
            if( Frm_notification.isDialogOpen) return;

            Task.Delay(100).ContinueWith(t =>
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => CloseNotificationForm()));
                }
                else
                {
                    CloseNotificationForm();
                }
            });
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            CloseNotificationForm();
        }

        private void CloseNotificationForm()
        {
            try
            {
                if (currentNotificationForm != null && !currentNotificationForm.IsDisposed)
                {
                    currentNotificationForm.Deactivate -= NotificationForm_Deactivate;
                    this.Deactivate -= MainForm_Deactivate;
                    currentNotificationForm.Close();
                    currentNotificationForm = null;
                    UpdateNotificationBadge();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur CloseNotificationForm: {ex.Message}");
            }
        }

        // ============ NAVIGATION MENU ============
        private void guna2CirclePictureBox2_Click_1(object sender, EventArgs e)
        {
            if (isMenuExpanded)
            {
                guna2Panel1.Visible = false;
                guna2Panel1.Width = collapsedWidth;
            }
            else
            {
                guna2Panel1.Width = expandedWidth;
guna2Panel1.Visible = true;            }
            isMenuExpanded = !isMenuExpanded;
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            _resetmenu();
            lbltitle.Text = "Client list";
            LoadFormInPanel(new Client.Client());
            picture.Image = Properties.Resources.wellness_program;
            guna2Button4.ForeColor = Color.Blue;
            guna2Button4.Image = Properties.Resources.users_bleu;

            if (toastManager != null && refresher != null)
            {
                refresher.Refresh(toastManager);
            }
            string text = "client";
                string cp = "successfully loaded";
            Clsglobal.GunaDialog(text,cp,MessageDialogIcon.Information,MessageDialogButtons.OK,
                         MessageDialogStyle.Light);
        }


        private void LoadFormInPanel(Form frm)
        {
            CloseNotificationForm();

            // 1. Libérer proprement la mémoire de l'ancien formulaire
            if (guna2Panel_med.Controls.Count > 0)
            {
                Form oldForm = guna2Panel_med.Controls[0] as Form;
                if (oldForm != null)
                {
                    oldForm.Close();
                    oldForm.Dispose();
                }
            }

            guna2Panel_med.Controls.Clear();

            // 2. Paramétrer le nouveau formulaire
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            // 3. Forcer l'opacité au cas où
            frm.Opacity = 1;
            frm.BackColor = Color.White; // Ou la couleur de votre choix

            guna2Panel_med.Controls.Add(frm);
            frm.Show();

            // 4. Forcer le rafraîchissement visuel du conteneur
            guna2Panel_med.Update();
            guna2Panel_med.Refresh();
        }


        public void guna2Button3_Click(object sender, EventArgs e)
        {
            _resetmenu();
            lbltitle.Text = "Session List";
            LoadFormInPanel(new sessions.frm_sessions());
            guna2Button3.ForeColor = Color.Blue;
            picture.Image = Properties.Resources.trainer;

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            _resetmenu();
            picture.Image = Properties.Resources.payment_method;
            lbltitle.Text = "Payment List";
            guna2Button2.ForeColor = Color.Blue;
            LoadFormInPanel(new Payments.FrmPayments());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            _resetmenu();
            lbltitle.Text = "Dashbord";
            guna2Button1.ForeColor = Color.Blue;
            picture.Image = Properties.Resources.dashboard__1_;
            LoadFormInPanel(new Frm_Dashbord());
        }

        private void _resetmenu()
        {
            guna2Button1.ForeColor = Color.White;
            guna2Button2.ForeColor = Color.White;
            guna2Button3.ForeColor = Color.White;
            guna2Button4.ForeColor = Color.White;
            guna2Button5.ForeColor = Color.White;
            guna2Button4.Image = Properties.Resources.users;
            ImageUser.ForeColor = Color.White;
            guna2Button6.ForeColor = Color.White;
            guna2Button7.ForeColor = Color.White;

        }


        private void guna2Button5_Click(object sender, EventArgs e)
        {
            _resetmenu();
            guna2Button5.ForeColor = Color.Blue;
            picture.Image = Properties.Resources.time_management;
            lbltitle.Text = "Attendance History\r\n";
                LoadFormInPanel(new frm_history());
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            _resetmenu();
            guna2Button7.ForeColor = Color.Blue;
            picture.Image = Properties.Resources.cogwheel;
            lbltitle.Text = "Settings";
            LoadFormInPanel(new frm_settings());
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            _resetmenu();
            guna2Button6.ForeColor = Color.Blue;
            picture.Image = Properties.Resources.assessment;
            lbltitle.Text = "Reports";
        }
        private void ImageUser_Click(object sender, EventArgs e)
        {
            _resetmenu();
            ImageUser.ForeColor = Color.Blue;
            picture.Image = Properties.Resources.profile;
            LoadFormInPanel(new Frm_Profilcs());


            lbltitle.Text = "Profile";
        }
        private void guna2Button9_Click(object sender, EventArgs e)
        {
            DialogResult result =Clsglobal.GunaDialog(
     "You will be signed out now.",
     "Sign Out",
     MessageDialogIcon.Warning,
     MessageDialogButtons.OKCancel,
     MessageDialogStyle.Light
 );
            if (result == DialogResult.Cancel)
            {
                return;
            }


            Clsglobal.CurrentUser = null;
            Login.Show();
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
            UpdateNotificationBadge();
            LoadFormInPanel(new Frm_Dashbord());

            if (toastManager != null && refresher != null)
            {
                refresher.Refresh(toastManager);
            }
        }

        // ============ NETTOYAGE ============
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (sessionsNotificationManager != null)
                {
                    sessionsNotificationManager.Dispose();
                    sessionsNotificationManager = null;
                }

                if (notificationRefreshTimer != null)
                {
                    notificationRefreshTimer.Stop();
                    notificationRefreshTimer.Dispose();
                    notificationRefreshTimer = null;
                }

                if (toastManager != null)
                {
                    toastManager.Dispose();
                    toastManager = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur OnFormClosing: {ex.Message}");
            }

            base.OnFormClosing(e);
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {

            try
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("=== TEST SIMPLE EMAIL ===");
                Console.WriteLine("========================================\n");

                // TEST 1 : Configuration
                Console.WriteLine("[TEST 1] Configuration");
                Console.WriteLine("Sender Email: saaadfifa@gmail.com");
                Console.WriteLine("Password: uzls bfdc frev lybz");
                Console.WriteLine("");

                // TEST 2 : Créer un email simple SANS pièce jointe
                Console.WriteLine("[TEST 2] Création email simple (sans QR Code)");

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("saaadfifa@gmail.com", "ATLASS_FITNESS");
                mail.To.Add("saaadfifa@gmail.com"); // ✅ ENVOYEZ À VOUS-MÊME pour tester
                mail.Subject = "Test ATLASS FITNESS";
                mail.Body = "Ceci est un email de test.\n\nSi vous recevez ceci, la configuration fonctionne !";
                mail.IsBodyHtml = false;

                Console.WriteLine("Email créé");
                Console.WriteLine($"De: {mail.From}");
                Console.WriteLine($"À: {mail.To[0]}");
                Console.WriteLine("");

                // TEST 3 : Envoyer
                Console.WriteLine("[TEST 3] Envoi...");

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("saaadfifa@gmail.com", "ulcz krwy auzh fszi");
                smtp.EnableSsl = true;
                smtp.Timeout = 30000;

                smtp.Send(mail);

                Console.WriteLine("✅✅✅ EMAIL ENVOYÉ AVEC SUCCÈS! ✅✅✅");
                Console.WriteLine("Vérifiez votre boîte de réception: saaadfifa@gmail.com");
                Console.WriteLine("========================================\n");

                MessageBox.Show("✅ Email de test envoyé avec succès!\n\nVérifiez votre boîte: saaadfifa@gmail.com",
                    "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);

                mail.Dispose();
                smtp.Dispose();
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"\n❌ ERREUR SMTP");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"StatusCode: {ex.StatusCode}");
                Console.WriteLine($"InnerException: {ex.InnerException?.Message}");
                Console.WriteLine("========================================\n");

                MessageBox.Show($"❌ Erreur SMTP:\n\n{ex.Message}\n\nStatusCode: {ex.StatusCode}",
                    "Erreur SMTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ ERREUR");
                Console.WriteLine($"Type: {ex.GetType().Name}");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"StackTrace:\n{ex.StackTrace}");
                Console.WriteLine("========================================\n");

                MessageBox.Show($"❌ Erreur:\n\n{ex.Message}",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}