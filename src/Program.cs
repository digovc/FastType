using System;
using System.Windows.Forms;

namespace FastType
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InterceptKeys.Init();
            TypeWriter.Init();

            Application.Run(new FormMain());

            InterceptKeys.Deinit();
        }
    }
}