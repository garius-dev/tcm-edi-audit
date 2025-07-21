namespace tcm_edi_audit_core
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var splash = new frmPreLoading();
            splash.Show();
            splash.Refresh();

            var mainForm = new frmHome();
            mainForm.Load += (s, e) => splash.Close();

            Application.Run(mainForm);
        }
    }
}