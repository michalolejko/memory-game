using Memory;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace memory_game
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(FormManager.InitGameForm());
        }
    }
}
