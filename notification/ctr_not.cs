using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATLASS_FITNESS_BUISNESS;

namespace ATLASS_FITNESS.notification
{
    public partial class ctr_not : UserControl
    {
        private ClsNotification notification;

        public ctr_not()
        {
            InitializeComponent();
        }

        public ClsNotification Notification
        {
            get { return notification; }
            set
            {
                notification = value;
                DisplayNotification();
            }
        }

        // Méthode pour afficher les données de la notification
        private void DisplayNotification()
        {
            if (notification != null)
            {
                string icon = notification.Icon;
                Image img = (Image)Properties.Resources.ResourceManager.GetObject(icon);
                guna2PictureBox1.Image = img;
                lbltitle.Text = notification.Title;
                lblmessage.Text = notification.Message;
                lbltime.Text = GetTimeAgo();

                // Changer la couleur ou le style si non lu
                if (!notification.IsRead)
                {
                    this.BackColor = Color.FromArgb(240, 248, 255); // Bleu clair
                    // ou mettre le texte en gras
                    lbltitle.Font = new Font(lbltitle.Font, FontStyle.Bold);
                }
                else
                {
                    this.BackColor = Color.White;
                    lbltitle.Font = new Font(lbltitle.Font, FontStyle.Regular);
                }

                // Appliquer une couleur selon la priorité
                Color borderColor = GetNotificationColor();
                // Vous pouvez dessiner une barre colorée sur le côté gauche
                // ou changer la couleur d'un panel indicateur
            }
        }


        // Obtenir la couleur selon le type
        private Color GetNotificationColor()
        {
            if (notification == null) return Color.Gray;

            switch (notification.Type)
            {
                case ClsNotification.TYPE_ABONNEMENT_EXPIRE:
                case ClsNotification.TYPE_PAIEMENT_RETARD:
                case ClsNotification.TYPE_SEANCE_ANNULEE:
                    return Color.FromArgb(220, 53, 69); // Rouge

                case ClsNotification.TYPE_NOUVELLE_INSCRIPTION:
                case ClsNotification.TYPE_PAIEMENT_RECU:
                case ClsNotification.TYPE_RENOUVELLEMENT:
                    return Color.FromArgb(40, 167, 69); // Vert

                case ClsNotification.TYPE_SEANCE_DEBUT:
                case ClsNotification.TYPE_CAPACITE_MAX:
                case ClsNotification.TYPE_RAPPEL_MEDICAL:
                    return Color.FromArgb(255, 193, 7); // Orange

                case ClsNotification.TYPE_PROMOTION:
                case ClsNotification.TYPE_ANNIVERSAIRE:
                    return Color.FromArgb(0, 123, 255); // Bleu

                case ClsNotification.TYPE_SYSTEME:
                case ClsNotification.TYPE_FEEDBACK:
                default:
                    return Color.Gray;
            }
        }

        // Formater le temps écoulé
        private string GetTimeAgo()
        {
            if (notification == null) return "";

            TimeSpan timeSpan = DateTime.Now - notification.CreatedAt;

            if (timeSpan.TotalMinutes < 1)
                return "À l'instant";
            else if (timeSpan.TotalMinutes < 60)
                return Math.Floor(timeSpan.TotalMinutes) + " min";
            else if (timeSpan.TotalHours < 24)
                return Math.Floor(timeSpan.TotalHours) + " h";
            else if (timeSpan.TotalDays < 7)
                return Math.Floor(timeSpan.TotalDays) + " j";
            else if (timeSpan.TotalDays < 30)
                return Math.Floor(timeSpan.TotalDays / 7) + " sem";
            else
                return Math.Floor(timeSpan.TotalDays / 30) + " mois";
        }

        // Événement clic sur la notification
        private void ctr_not_Click(object sender, EventArgs e)
        {
            if (notification != null && !notification.IsRead)
            {
                // Marquer comme lu
                notification.MarkAsRead();
                if (notification != null && !notification.IsRead)
                {
                    // Marquer comme lu
                    notification.MarkAsRead();
                    DisplayNotification();
                }


                // Rafraîchir l'affichage
                DisplayNotification();


                // Déclencher un événement pour informer le parent
                OnNotificationClicked?.Invoke(notification);
            }
            if (notification.Title.ToLower().Contains("séance"))
            {

                OpenSessionsPage();
            }
        }

        // Événement personnalisé pour le clic
        public delegate void NotificationClickedHandler(ClsNotification notification);
        public event NotificationClickedHandler OnNotificationClicked;

        private void ctr_not_Load(object sender, EventArgs e)
        {
            // Attacher l'événement Click à tous les contrôles enfants
            this.Click += ctr_not_Click;
            foreach (Control ctrl in this.Controls)
            {
                ctrl.Click += ctr_not_Click;
            }
        }

        // Méthode pour dessiner une barre colorée sur le côté gauche (optionnel)
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (notification != null)
            {
                Color barColor = GetNotificationColor();
                using (SolidBrush brush = new SolidBrush(barColor))
                {
                    // Dessiner une barre de 4px sur le côté gauche
                    e.Graphics.FillRectangle(brush, 0, 0, 4, this.Height);
                }
            }
        }

  
        private void OpenSessionsPage()
        {
            // Trouver le Form1 parent
            Form1 mainForm = Application.OpenForms["Form1"] as Form1;

            mainForm.guna2Button3.PerformClick(); // Simuler clic sur le bouton "Sessions"

            
        }

    }
}
