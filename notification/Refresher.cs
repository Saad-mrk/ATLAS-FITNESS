using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ATLASS_FITNESS_BUISNESS;

namespace ATLASS_FITNESS
{
    public class Refresher
    {
        private ClsClient clsClien = new ClsClient();
        private ClsSubscription sub = new ClsSubscription();
        private NotificationToastManager toastManager;

        public void Refresh(NotificationToastManager toastManager)
        {
            this.toastManager = toastManager;

            try
            {
                Console.WriteLine("=== DÉBUT REFRESH ===");
                Console.WriteLine($"ToastManager null ? {toastManager == null}");

                // Mettre à jour les statuts des clients et abonnements
                clsClien.updateisactive();
                sub.IsExpiringSoon();

                // Récupérer les abonnements qui expirent
                DataTable dt = ClsSubscription.getsubcriptionexpired();

                Console.WriteLine($"Abonnements trouvés : {dt?.Rows.Count ?? 0}");

                // Créer et afficher les notifications
                CreateExpirationNotifications(dt);

                Console.WriteLine("=== FIN REFRESH ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur Refresh: {ex.Message}");
            }
        }

        public void REFRESHERR()
        {
            try
            {
                clsClien.updateisactive();
                ClsSubscription.UpdateIsActive();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur REFRESHERR: {ex.Message}");
            }
        }

        private void CreateExpirationNotifications(DataTable subscriptions)
        {
            if (subscriptions == null || subscriptions.Rows.Count == 0)
            {
                Console.WriteLine("Aucun abonnement à expirer trouvé");
                return;
            }

            DateTime today = DateTime.Today;
            DateTime tomorrow = DateTime.Today.AddDays(1);

            Console.WriteLine($"Date aujourd'hui : {today:yyyy-MM-dd}");
            Console.WriteLine($"Date demain : {tomorrow:yyyy-MM-dd}");

            foreach (DataRow row in subscriptions.Rows)
            {
                try
                {
                    DateTime endDate = Convert.ToDateTime(row["end_date"]);
                    int clientId = Convert.ToInt32(row["client_id"]);
                    int subscriptionId = Convert.ToInt32(row["id"]);

                    // Vérifier si client_name existe
                    string clientName = "Client inconnu";
                    if (subscriptions.Columns.Contains("client_name"))
                    {
                        clientName = row["client_name"]?.ToString() ?? "Client inconnu";
                    }

                    Console.WriteLine($"Client: {clientName}, Expire: {endDate:yyyy-MM-dd}");

                    // Expire AUJOURD'HUI → Icône ROUGE (Error)
                    if (endDate.Date == today)
                    {
                        Console.WriteLine("  → Expire AUJOURD'HUI");
                        CreateNotificationIfNotExists(
                            clientId,
                            subscriptionId,
                            "abonnement_expire_aujourd_hui",
                            "⚠️ Abonnement expire AUJOURD'HUI",
                            $"{clientName} - dernier jour d'accès",
                            ToolTipIcon.Error
                        );
                    }
                    // Expire DEMAIN → Icône JAUNE (Warning)
                    else if (endDate.Date == tomorrow)
                    {
                        Console.WriteLine("  → Expire DEMAIN");
                        CreateNotificationIfNotExists(
                            clientId,
                            subscriptionId,
                            "abonnement_expire_demain",
                            "📅 Abonnement expire demain",
                            $"{clientName} - penser à renouveler",
                            ToolTipIcon.Warning
                        );
                    }
                    else
                    {
                        Console.WriteLine("  → N'expire ni aujourd'hui ni demain");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur traitement abonnement: {ex.Message}");
                    continue;
                }
            }
        }

        private void CreateNotificationIfNotExists(
            int clientId,
            int subscriptionId,
            string type,
            string title,
            string message,
            ToolTipIcon icon)
        {
            try
            {
                Console.WriteLine($"  CreateNotificationIfNotExists: {title}");

                // Vérifier si la notification existe déjà
                if (NotificationExistsToday(clientId, type))
                {
                    Console.WriteLine("  → Notification ignorée (existe déjà)");
                    return;
                }

                Console.WriteLine("  → Création nouvelle notification...");

                // Créer la notification
                ClsNotification notification = new ClsNotification
                {
                    Type = type,
                    ClientId = clientId,
                    SubscriptionId = subscriptionId,
                    Title = title,
                    Message = message,
                    Icon = "users",
                    IsRead = false,
                    CreatedAt = DateTime.Now,
                    ExpiresAt = DateTime.Now.AddDays(2)
                };

                // Sauvegarder en base de données
                bool saved = notification.Save();
                Console.WriteLine($"  → Sauvegardé en BDD ? {saved}");

                if (saved)
                {
                    // Afficher le toast via NotificationToastManager
                    if (toastManager != null)
                    {
                        Console.WriteLine("  → Affichage du toast...");
                        toastManager.ShowNotification(notification, icon);
                        Console.WriteLine("  ✅ Toast affiché !");
                    }
                    else
                    {
                        Console.WriteLine("  ❌ ERREUR : toastManager est NULL !");
                    }
                }
                else
                {
                    Console.WriteLine("  ❌ ERREUR : Échec de sauvegarde en BDD");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur CreateNotificationIfNotExists: {ex.Message}");
            }
        }

        private bool NotificationExistsToday(int clientId, string type)
        {
            try
            {
                ClsNotification notificationManager = new ClsNotification();

                var notifications = notificationManager.GetAll()
                    .Where(n => n.ClientId == clientId
                             && n.Type == type
                             && n.CreatedAt.Date == DateTime.Today)
                    .ToList();

                Console.WriteLine($"    NotificationExistsToday: trouvé {notifications.Count} notification(s)");

                return notifications.Count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur NotificationExistsToday: {ex.Message}");
                return false;
            }
        }
    }
}