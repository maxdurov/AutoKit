using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AutoKit
{
    internal class Logger
    {
        static string path = "";
        public static void Init()
        {
            if (Directory.Exists("logs"))
            {
               string[] list_files = Directory.GetFiles("logs");

               path = @"logs\log_" + (DateTime.Now.ToString("dd_MM_yyyy__hh_mm_ss") +".txt");
                if(list_files.Length > 10)
                {
                    for(int i = 0; i < list_files.Length-10; i++)
                    {
                        File.Delete(list_files[i]);
                    }
                }

                File.Create(path).Close();
            }
            else
            {
                Directory.CreateDirectory("logs");
                Init();
            }
        }

        public static void Log(string message)
        {
            File.AppendAllText(path, "\n[" + DateTime.Now + "] " + message);
        }
    }
}
