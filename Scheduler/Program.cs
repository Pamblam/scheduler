using System.Globalization;

namespace Scheduler{
    internal static class Program{
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(){
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            string language = CultureInfo.CurrentUICulture.Name;
            Application.Run(new Form_login(language));
        }
    }
}