using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EarthQuakeLib;

namespace RMTProcessing
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new RMTProcessingMainForm());
            }
            catch (Exception ex)
            {
                Log.e("Main exception :"+ex.Message);
            }
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.e(String.Format("CurrentDomain_UnhandledException: sender={0}, e={1}", sender.ToString(),e.ToString()));
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Log.e(String.Format("Application_ThreadException: sender={0}, e={1}", sender.ToString(),e.ToString()));
        }
    }
}
