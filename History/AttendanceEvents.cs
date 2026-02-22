using System;

namespace ATLASS_FITNESS
{
    public static class AttendanceEvents
    {
        // Événement déclenché après chaque scan
        public static event EventHandler AttendanceRecorded;

        // Méthode pour déclencher l'événement
        public static void OnAttendanceRecorded()
        {
            AttendanceRecorded?.Invoke(null, EventArgs.Empty);
        }
    }
}