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
using ATLASS_FITNESS.notification;
using System.Drawing.Text;

namespace ATLASS_FITNESS
{
    public partial class Frm_notification : Form
    {
        private ClsNotification notificationManager;
        public static bool isDialogOpen=false;

        public Frm_notification()
        {
            InitializeComponent();
            notificationManager = new ClsNotification();
        }

        private void Frm_notification_Load(object sender, EventArgs e)
        {
            // Charger toutes les notifications au démarrage
            LoadNotifications(true);
            UpdateNotificationCount();
        }

        // Charger les notifications dans le FlowLayoutPanel
        private void LoadNotifications(bool includeRead)
        {
            // Supposons que votre FlowLayoutPanel s'appelle flowLayoutPanelNotifications
            flowLayoutPanel1.Controls.Clear();

            List<ClsNotification> notifications = notificationManager.GetAll();

            // Filtrer selon le bouton cliqué
            if (!includeRead)
            {
                notifications = notifications.Where(n => !n.IsRead).ToList();
            }

            // Trier par date (plus récent en premier)
            notifications = notifications.OrderByDescending(n => n.CreatedAt).ToList();

            if (notifications.Count == 0)
            {
                // Afficher un message si aucune notification
                Label lblEmpty = new Label();
                lblEmpty.Text = includeRead ? "Aucune notification" : "Aucune notification non lue";
                lblEmpty.ForeColor = Color.Gray;
                lblEmpty.Font = new Font("Segoe UI", 10, FontStyle.Italic);
                lblEmpty.AutoSize = true;
                lblEmpty.Padding = new Padding(20);
                flowLayoutPanel1.Controls.Add(lblEmpty);
                return;
            }

            // Ajouter chaque notification
            foreach (var notif in notifications)
            {
                ctr_not notifControl = new ctr_not();
                notifControl.Notification = notif;
                notifControl.Width = flowLayoutPanel1.Width - 25;
                notifControl.Margin = new Padding(5, 5, 5, 5);

                // Événement lors du clic sur une notification
                notifControl.OnNotificationClicked += NotifControl_OnNotificationClicked;

                flowLayoutPanel1.Controls.Add(notifControl);
            }
        }

        // Gestionnaire d'événement lors du clic sur une notification
        private void NotifControl_OnNotificationClicked(ClsNotification notification)
        {
            // Rafraîchir l'affichage
            if (guna2Button4.FillColor == Color.White) // Bouton "All" actif
            {
                LoadNotifications(true);
            }
            else // Bouton "NON LU" actif
            {
                LoadNotifications(false);
            }

            // Mettre à jour le compteur
            UpdateNotificationCount();
        }

        // Bouton "All" - Afficher toutes les notifications
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            guna2Button4.FillColor = Color.White;
            guna2Button3.FillColor = Color.Transparent;

            LoadNotifications(true); // Charger toutes les notifications
        }

        // Bouton "NON LU" - Afficher uniquement les non lues
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            guna2Button3.FillColor = Color.White;
            guna2Button4.FillColor = Color.Transparent;

            LoadNotifications(false); // Charger uniquement les non lues
        }

        // Mettre à jour le compteur de notifications non lues
        private void UpdateNotificationCount()
        {
            int unreadCount = notificationManager.GetAll().Count(n => !n.IsRead);

            // Supposons que vous avez un Label pour afficher le nombre
            // Par exemple lblNotificationCount ou intégré dans lblTousLu
            if (unreadCount > 0)
            {
                //lblTousLu.Text = $"Tous lu ({unreadCount})"; // ou le nom de votre label
                //lblTousLu.Visible = true;
            }
            else
            {
             //   lblTousLu.Text = "Tous lu ✓";
            }
        }

        // Marquer toutes les notifications comme lues (clic sur "Tous lu")
       

        // Optionnel: Rafraîchir automatiquement toutes les X secondes
        private void InitializeAutoRefresh()
        {
            Timer refreshTimer = new Timer();
            refreshTimer.Interval = 30000; // 30 secondes
            refreshTimer.Tick += (s, e) =>
            {
                if (guna2Button4.FillColor == Color.White)
                {
                    LoadNotifications(true);
                }
                else
                {
                    LoadNotifications(false);
                }
                UpdateNotificationCount();
            };
            refreshTimer.Start();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            isDialogOpen = true;

        DialogResult result = MessageBox.Show(
                "Voulez-vous marquer toutes les notifications comme lues ?",
                "Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                notificationManager.MarkAllAsRead();

                // Rafraîchir l'affichage
                if (guna2Button4.FillColor == Color.White) // Bouton "All" actif
                {
                    LoadNotifications(true);
                }
                else // Bouton "NON LU" actif
                {
                    LoadNotifications(false);
                }

                UpdateNotificationCount();

                MessageBox.Show("Toutes les notifications ont été marquées comme lues.",
                               "Succès",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information);
            }
            isDialogOpen = false;

        }

        private void Frm_notification_Load_1(object sender, EventArgs e)
        {
            LoadNotifications(true);
        }
    }
}