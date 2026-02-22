using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATLASS_FITNESS.Client;
using ATLASS_FITNESS.Person;
using ATLASS_FITNESS.Subscrbtion;

namespace ATLASS_FITNESS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           


            Application.Run( new Form1());
        }
    }
}
