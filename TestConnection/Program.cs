using System;
using System.Windows.Forms;

namespace TestConnection
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Connect to DB
            TooSharp.Connection.connectionInstance = new TooSharp.Connection("contacts");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmContactList());

        }
    }
}
