using System;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.Forms;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Service;

namespace Restaurant_Ordering_and_Management_System
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

            AppServices services;
            try
            {
                // Composition root: the only place that builds the concrete services.
                services = AppServices.CreateDefault();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("The application could not start:\n" + ex.Message, "Startup Error");
                return;
            }

            Application.Run(new FormLogIn(services));
        }
    }
}
