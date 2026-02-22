using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ATLASS_FITNESS_BUISNESS;

namespace ATLASS_FITNESS.Notifications
{
    public class SessionsNotificationManager : IDisposable
    {
        private ClsNotification notificationManager;
        private Timer sessionCheckTimer;
        private NotificationToastManager toastManager;

        private HashSet<string> processedNotifications;
        private DateTime lastCleanTime;

        public event EventHandler NotificationCreated;

        public SessionsNotificationManager(NotificationToastManager toastManager)
        {
            this.notificationManager = new ClsNotification();
            this.toastManager = toastManager;
            this.processedNotifications = new HashSet<string>();
            this.lastCleanTime = DateTime.Now;

            InitializeTimer();
        }

        private void InitializeTimer()
        {
            sessionCheckTimer = new Timer();
            sessionCheckTimer.Interval = 30000; // 30 secondes
            sessionCheckTimer.Tick += SessionCheckTimer_Tick;
        }

        public void Start()
        {
            sessionCheckTimer.Start();
            CheckAndCreateSessionNotifications();
        }

        public void Stop()
        {
            sessionCheckTimer?.Stop();
        }

        private void SessionCheckTimer_Tick(object sender, EventArgs e)
        {
            CheckAndCreateSessionNotifications();
            NotificationCreated?.Invoke(this, EventArgs.Empty);
        }

        public void CheckAndCreateSessionNotifications()
        {
            try
            {
                CleanProcessedNotifications();

                string jour = DateTime.Now.DayOfWeek.ToString();
                DataTable dt = ClsSessions.GetAllSessions(jour);

                if (dt == null || dt.Rows.Count == 0)
                    return;

                foreach (DataRow row in dt.Rows)
                {
                    int sessionId = Convert.ToInt32(row["id"]);
                    string sessionType = row["session_type"].ToString();
                    TimeSpan startTime = (TimeSpan)row["start_time"];
                    TimeSpan endTime = (TimeSpan)row["end_time"];
                    TimeSpan currentTime = DateTime.Now.TimeOfDay;

                    CheckAndNotify(sessionId, sessionType, startTime, currentTime, 30);
                    CheckAndNotify(sessionId, sessionType, startTime, currentTime, 15);
                    CheckAndNotifySessionStart(sessionId, sessionType, startTime, currentTime);
                    CheckAndNotifySessionEnd(sessionId, sessionType, endTime, currentTime);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur sessions: {ex.Message}");
            }
        }

        private void CheckAndNotify(int sessionId, string sessionType, TimeSpan startTime, TimeSpan currentTime, int minutesBefore)
        {
            TimeSpan notifyTime = startTime.Subtract(TimeSpan.FromMinutes(minutesBefore));
            TimeSpan diff = currentTime - notifyTime;

            // ✅ fenêtre élargie à 10 minutes
            if (diff >= TimeSpan.Zero && diff < TimeSpan.FromMinutes(10))
            {
                string key = $"{sessionId}_{minutesBefore}_{DateTime.Now:yyyyMMdd}";

                if (!processedNotifications.Contains(key))
                {
                    CreateSessionStartNotification(sessionId, sessionType, startTime.ToString(@"hh\:mm"), minutesBefore);
                    processedNotifications.Add(key);
                }
            }
        }

        private void CheckAndNotifySessionStart(int sessionId, string sessionType, TimeSpan startTime, TimeSpan currentTime)
        {
            TimeSpan diff = currentTime - startTime;

            if (diff >= TimeSpan.Zero && diff < TimeSpan.FromMinutes(10))
            {
                string key = $"{sessionId}_start_{DateTime.Now:yyyyMMdd}";

                if (!processedNotifications.Contains(key))
                {
                    CreateSessionBeginNotification(sessionId, sessionType, startTime.ToString(@"hh\:mm"));
                    processedNotifications.Add(key);
                }
            }
        }

        private void CheckAndNotifySessionEnd(int sessionId, string sessionType, TimeSpan endTime, TimeSpan currentTime)
        {
            TimeSpan diff = currentTime - endTime;

            if (diff >= TimeSpan.Zero && diff < TimeSpan.FromMinutes(10))
            {
                string key = $"{sessionId}_end_{DateTime.Now:yyyyMMdd}";

                if (!processedNotifications.Contains(key))
                {
                    CreateSessionEndNotification(sessionId, sessionType, endTime.ToString(@"hh\:mm"));
                    processedNotifications.Add(key);
                }
            }
        }

        private void CreateSessionStartNotification(int sessionId, string sessionName, string startTime, int minutesBefore)
        {
            if (!NotificationExistsToday(sessionId, ClsNotification.TYPE_SEANCE_DEBUT, minutesBefore))
            {
                ClsNotification notification = new ClsNotification
                {
                    Type = ClsNotification.TYPE_SEANCE_DEBUT,
                    SessionId = sessionId,
                    Title = $"Séance dans {minutesBefore} minutes",
                    Message = $"{sessionName} - démarre à {startTime}",
                    Icon = "interval",
                    ExpiresAt = DateTime.Now.AddHours(2)
                };

                if (notification.Save())
                    toastManager?.ShowNotification(notification, ToolTipIcon.Info);
            }
        }

        private void CreateSessionBeginNotification(int sessionId, string sessionName, string startTime)
        {
            if (!NotificationExistsToday(sessionId, ClsNotification.TYPE_SEANCE_DEBUT, 0))
            {
                ClsNotification notification = new ClsNotification
                {
                    Type = ClsNotification.TYPE_SEANCE_DEBUT,
                    SessionId = sessionId,
                    Title = "Séance en cours",
                    Message = $"{sessionName} - a commencé à {startTime}",
                    Icon = "interval",
                    ExpiresAt = DateTime.Now.AddHours(3)
                };

                if (notification.Save())
                    toastManager?.ShowNotification(notification, ToolTipIcon.Info);
            }
        }

        private void CreateSessionEndNotification(int sessionId, string sessionName, string endTime)
        {
            if (!NotificationExistsToday(sessionId, ClsNotification.TYPE_SEANCE_TERMINEE, 0))
            {
                ClsNotification notification = new ClsNotification
                {
                    Type = ClsNotification.TYPE_SEANCE_TERMINEE,
                    SessionId = sessionId,
                    Title = "Séance terminée",
                    Message = $"{sessionName} - terminée à {endTime}",
                    Icon = "interval",
                    ExpiresAt = DateTime.Now.AddHours(6)
                };

                if (notification.Save())
                    toastManager?.ShowNotification(notification, ToolTipIcon.Info);
            }
        }

        private bool NotificationExistsToday(int sessionId, string type, int minutesBefore)
        {
            try
            {
                var today = DateTime.Today;

                var notifications = notificationManager.GetAll()
                    .Where(n => n.SessionId == sessionId &&
                                n.Type == type &&
                                n.CreatedAt >= today)
                    .ToList();

                if (minutesBefore > 0)
                    return notifications.Any(n => n.Message.Contains($"dans {minutesBefore} minutes"));

                return notifications.Any();
            }
            catch
            {
                return false;
            }
        }

        private void CleanProcessedNotifications()
        {
            if ((DateTime.Now - lastCleanTime).TotalHours >= 1)
            {
                processedNotifications.Clear();
                lastCleanTime = DateTime.Now;
            }
        }

     
        

        public void Dispose()
        {
            Stop();
            sessionCheckTimer?.Dispose();
            processedNotifications?.Clear();
        }
    }
}