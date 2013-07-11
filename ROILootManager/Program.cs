using System;
using System.Threading;
using System.Windows.Forms;
using log4net;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace ROILootManager {
    static class Program {
        private static ILog logger = LogManager.GetLogger(typeof(Program));
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            GDriveManager.initialize();

            Application.Run(new frmMain());

            PropertyManager.getManager().close();

            logger.Info("Closing...");
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e) {
            MessageBox.Show(e.Exception.Message, "Unhandled Thread Exception");
            logger.Error("Unhandled application error.", e.Exception);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            MessageBox.Show((e.ExceptionObject as Exception).Message, "Unhandled UI Exception");
            logger.Error("Unhandled application error.", (e.ExceptionObject as Exception));
        }
    }
}
