using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using ATLASS_FITNESS_BUISNESS;
using ATLASS_FITNESS.notification;

namespace ATLASS_FITNESS
{
    public class NotificationToastManager
    {
        private Form parentForm;
        private NotifyIcon notifyIcon;
        private Queue<ClsNotification> notificationQueue;
        private bool isShowingNotification = false;
        private Timer displayTimer;

        // Configuration
        private int displayDuration = 5000; // 5 secondest
        private ToolTipIcon currentIcon = ToolTipIcon.Info;

        public NotificationToastManager(Form parent)
        {
            parentForm = parent;
            notificationQueue = new Queue<ClsNotification>();
            InitializeNotifyIcon();
        }

        /// <summary>
        /// Initialise le NotifyIcon (icône dans la barre des tâches)
        /// </summary>
        private void InitializeNotifyIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Application;
            notifyIcon.Visible = true;

            // Événement de clic sur la notification
            notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
            notifyIcon.BalloonTipClosed += NotifyIcon_BalloonTipClosed;
        }

        /// <summary>
        /// Affiche une notification avec icône
        /// </summary>
        public void ShowNotification(ClsNotification notification, ToolTipIcon icon = ToolTipIcon.Info)
        {
            currentIcon = icon;
            notificationQueue.Enqueue(notification);

            if (!isShowingNotification)
            {
                ShowNextNotification();
            }
        }

        private void ShowNextNotification()
        {
            if (notificationQueue.Count == 0)
            {
                isShowingNotification = false;
                return;
            }

            isShowingNotification = true;
            ClsNotification notification = notificationQueue.Dequeue();

            // Configurer le NotifyIcon
            notifyIcon.BalloonTipIcon = currentIcon;
            notifyIcon.BalloonTipTitle = notification.Title ?? "Notification";
            notifyIcon.BalloonTipText = notification.Message ?? "";
            

            // Jouer le son selon l'icône
            PlayNotificationSound();

            // Afficher la notification
            notifyIcon.ShowBalloonTip(displayDuration);

            // Marquer comme lue automatiquement après affichage
            notification.MarkAsRead();

            // Démarrer le timer pour afficher la prochaine notification
            StartDisplayTimer();
        }

        private void PlayNotificationSound()
        {
            try
            {
                // Son selon le type d'icône
                switch (currentIcon)
                {
                    case ToolTipIcon.Error:
                        SystemSounds.Hand.Play();
                        break;
                    case ToolTipIcon.Warning:
                        SystemSounds.Exclamation.Play();
                        break;
                    case ToolTipIcon.Info:
                    default:
                        SystemSounds.Asterisk.Play();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lecture son: " + ex.Message);
            }
        }

        private void StartDisplayTimer()
        {
            if (displayTimer != null)
            {
                displayTimer.Stop();
                displayTimer.Dispose();
            }

            displayTimer = new Timer();
            displayTimer.Interval = displayDuration;
            displayTimer.Tick += (s, e) =>
            {
                displayTimer.Stop();
                ShowNextNotification(); // Afficher la prochaine notification
            };
            displayTimer.Start();
        }

        /// <summary>
        /// Événement quand l'utilisateur clique sur la notification
        /// </summary>
        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            // Déclencher l'événement personnalisé
            OnToastClicked?.Invoke(null);
        }

        /// <summary>
        /// Événement quand la notification se ferme
        /// </summary>
        private void NotifyIcon_BalloonTipClosed(object sender, EventArgs e)
        {
            // La notification est fermée, on peut afficher la suivante
            isShowingNotification = false;
            ShowNextNotification();
        }

        public void CloseCurrentToast()
        {
            // Avec NotifyIcon, on ne peut pas fermer manuellement
            // Mais on peut afficher la prochaine
            isShowingNotification = false;
            ShowNextNotification();
        }

        public void ClearQueue()
        {
            notificationQueue.Clear();
        }

        public void SetDisplayDuration(int milliseconds)
        {
            displayDuration = milliseconds;
        }

        public delegate void ToastClickedHandler(ClsNotification notification);
        public event ToastClickedHandler OnToastClicked;

        public void Dispose()
        {
            if (displayTimer != null)
            {
                displayTimer.Stop();
                displayTimer.Dispose();
            }

            if (notifyIcon != null)
            {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
            }

            notificationQueue.Clear();
        }
    }
}