using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AutoKit
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        static public bool isExit = false;
        static public bool isRestart = false;
        [STAThread]
       static void Main(string[] args)
        {
            Console.WriteLine(args);
            try
            {
                if (args[0] == "restart")
                {
                    isRestart = true;
                    Directory.SetCurrentDirectory(args[1]);
                }
            }
            catch
            {

            }
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Logger.Init();
            new MainEnter();
            
            if (!isExit)
            {
                    Application.Run();
            }
            
        }
    }
}
