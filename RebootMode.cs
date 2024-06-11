using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace AutoKit
{
    internal static class RebootMode
    {
        public static async Task<bool> setAutoStart(List<Config> conf, bool noreboot = false)
        {
            // Компьютер\HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\RunOnce
            Registry.SetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce", "AutoKit", System.Reflection.Assembly.GetExecutingAssembly().Location + " restart " + Directory.GetCurrentDirectory());
            using (StreamWriter writer = new StreamWriter("temp.dat", false))
            {
                await writer.WriteLineAsync(Serialized(conf).ToString());
                writer.Close();
            }
            //File.WriteAllText("temp.dat", Serialized(conf).ToString());
            if (!noreboot)
            {
                Reboot();
            }
            return true;
            
        }

        private static JObject Serialized(List<Config> data)
        {
            JObject json = new JObject();
            json["items"] = new JArray();
            foreach (Config config in data)
            {
                JObject obj = new JObject
                {
                    { "name", config.getName() },
                    { "path", config.getPath() },
                    { "arg", config.getParametrs() },
                    { "default_install", config.isDefaultInstall() },
                    { "restart", config.isReload() }
                };
                ((JArray)json["items"]).Add(obj);

            }
            Console.WriteLine(json.ToString());
            return json;
        }

        public static List<Config> getSaveConf()
        {
            List<Config> conf = new List<Config>();

            if (File.Exists("temp.dat"))
            {
                string text = File.ReadAllText("temp.dat");

                JObject json = JObject.Parse(text);

                JArray jar = (JArray) json["items"];

                for(int i = 0; jar.Count > i; i++)
                {
                    conf.Add(new Config((string)jar[i]["name"], (string)jar[i]["path"], (string)jar[i]["arg"], (bool)jar[i]["default_install"], (string)jar[i]["restart"]));
                }

            }
            else
            {
                MessageBox.Show("Сохраненная конфигурация перед перезагрузкой была утеряна.\nЗапустите программу заново и выберите вручную необходимые программы для установки ");
                Program.isExit = true;
            }
            return conf;
        }
        
        public static void Reboot()
        {
            Process.Start("shutdown","/r /t 0");
        }
    }
}
