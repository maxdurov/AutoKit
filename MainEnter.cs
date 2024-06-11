using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace AutoKit
{
    internal class MainEnter : Form
    {

        public MainEnter()
        {
         
            Main();
            
        }

        async public void Main()
        {
            if (!Program.isRestart) {
            object gp = getPrograms();
                if (gp.GetType() == typeof(List<Config>))
                {

                    SelectForm form = new SelectForm((List<Config>)gp);
                    form.ShowDialog();
                    if (!form.btnClose)
                    {
                        Console.WriteLine("Close");
                        Program.isExit = true;
                        return;
                    }
                    LogWin logwin = new LogWin();
                    logwin.countProg.Text = "0/" + form.selected_items.Count;
                    logwin.pbar.Maximum = form.selected_items.Count;
                    logwin.Show();

                    List<Config> list_of_items = new List<Config>(form.selected_items);




                    for (int i = 0; i < form.selected_items.Count; i++)
                    {
                        if (form.selected_items[i].isReload() == "before")
                        {
                            if (list_of_items.Count == 0)
                            {

                            }
                            else
                            {
                                list_of_items.Remove(list_of_items[i]);
                                await RebootMode.setAutoStart(list_of_items, true);
                            }
                        }


                        logwin.countProg.Text = (i + 1) + "/" + form.selected_items.Count;
                        logwin.pbar.Value = i + 1;
                        logwin.logbox.Text += "[" + DateTime.Now + "] " + "Начата установка \"" + form.selected_items[i].getName() + "\"\n";
                        Logger.Log("Начата установка \"" + form.selected_items[i].getName() + "\"");
                        await installAction(form.selected_items[i], logwin.logbox);
                        logwin.logbox.Text += "[" + DateTime.Now + "] " + "Установка завершена\n";
                        Logger.Log("Установка завершена");
                        list_of_items.Remove(form.selected_items[i]);


                        if (form.selected_items[i].isReload() == "after")
                        {
                            if (list_of_items.Count == 0)
                            {
                                RebootMode.Reboot();
                                Program.isExit = true;
                                Application.Exit();
                            }
                            else
                            {
                                await RebootMode.setAutoStart(list_of_items);
                                Program.isExit = true;
                                Application.Exit();
                            }
                            return;
                        }

                        if (form.selected_items[i].isReload() == "before")
                        {
                            Program.isExit = true;
                            Application.Exit();
                            return;
                        }
                    }

                    logwin.btnClose.Enabled = true;
                }
                
            }
            else
            {
                List<Config> configs = RebootMode.getSaveConf();
                if (configs.Count == 0)
                {
                    Program.isExit = true;
                    Application.Exit();
                    return;
                }
                LogWin logwin = new LogWin();
                logwin.countProg.Text = "0/" + configs.Count;
                logwin.pbar.Maximum = configs.Count;
                logwin.Show();

                List<Config> list_of_items = new List<Config>(configs);




                for (int i = 0; i < configs.Count; i++)
                {
                    if (configs[i].isReload() == "before")
                    {
                        if (list_of_items.Count == 0)
                        {
                           
                        }
                        else
                        {
                            list_of_items.Remove(configs[i]);
                           await RebootMode.setAutoStart(list_of_items, true);
                        }
                    }

                    logwin.countProg.Text = (i + 1) + "/" + configs.Count;
                    logwin.pbar.Value = i + 1;
                    logwin.logbox.Text += "[" + DateTime.Now + "] " + "Начата установка \"" + configs[i].getName() + "\"\n";
                    Logger.Log("Начата установка \"" + configs[i].getName() + "\"");
                    await installAction(configs[i], logwin.logbox);
                    logwin.logbox.Text += "[" + DateTime.Now + "] " + "Установка завершена\n";
                    Logger.Log("Установка завершена");
                    list_of_items.Remove(configs[i]);

                    if (configs[i].isReload() == "after")
                    {
                        if(list_of_items.Count == 0)
                        {
                            RebootMode.Reboot();
                            Program.isExit = true;
                            Application.Exit();
                        }
                        else
                        {
                            await RebootMode.setAutoStart(list_of_items);
                            Program.isExit = true;
                            Application.Exit();
                        }
                        return;
                    }

                    if (configs[i].isReload() == "before")
                    {
                        Program.isExit = true;
                        Application.Exit();
                        return;
                    }
                }

                logwin.btnClose.Enabled = true;
            }
        }

        async private Task<bool> installAction(Config act, RichTextBox log)
        {
            bool err = false;
            Process proc = new Process();
            proc.StartInfo.FileName = act.getPath();
            proc.StartInfo.Arguments = act.getParametrs();
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;

            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.CreateNoWindow = true;

            proc.StartInfo.StandardOutputEncoding = Encoding.GetEncoding("CP866");
            proc.StartInfo.StandardErrorEncoding = Encoding.GetEncoding("CP866");

            proc.OutputDataReceived += (s, e) => { log.BeginInvoke((MethodInvoker) (() => { log.AppendText("[" + DateTime.Now + "] " + e.Data + "\n"); Logger.Log(e.Data); } )); };
            proc.ErrorDataReceived += (s, e) => { log.BeginInvoke((MethodInvoker)(() => { log.AppendText("[" + DateTime.Now + "] " + e.Data + "\n"); Logger.Log(e.Data); } )); };

            try
            {
                proc.Start();

            }
            catch(Exception ex)
            {
                Logger.Log("Error: " + ex.Message);
                MessageBox.Show("При запуске процесса произошла ошибка!\nОшибка: \n" + ex.Message, "Ошибка", MessageBoxButtons.OK);
                err = true;
            }

            if (!err)
            {
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
                await Task.Run(() => { proc.WaitForExit(); });
            }
            



            return true; 

        }



        object getPrograms()
        {
            if (File.Exists("config.json"))
            {
                string text = File.ReadAllText("config.json");
                string[] text_arr = text.Split('\n');
                string parsed_text = "";
                for (int i = 0; text_arr.Length > i; i++)
                {
                    if(text_arr[i].Length >= 2)
                    {
                        if (text_arr[i].Substring(0, 2) != "//")
                        {
                            parsed_text += text_arr[i];
                        }
                    }
                    else
                    {
                        parsed_text += text_arr[i];
                    }
                }

                Console.WriteLine(parsed_text);
                Logger.Log(parsed_text);

                JObject json_obj;

                try
                {
                   json_obj = JObject.Parse(parsed_text);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Возникла ошибка при чтении конфигурационного файла. Проверьте конфигурационный файл.\nОшибка:\n" + e.Message, "Ошибка", MessageBoxButtons.OK);
                    Logger.Log("Возникла ошибка при чтении конфигурационного файла. Проверьте конфигурационный файл.\nОшибка:\n" + e.Message);
                    Program.isExit = true;
                    Application.Exit();
                    return false;
                }

                if((bool)json_obj["config_is_ready"] == false)
                {
                    MessageBox.Show("Конфигурационный файл не настроен.\nНастройте конфигурационный файл config.json", "Уведомление", MessageBoxButtons.OK);
                    Logger.Log("Конфигурационный файл не настроен. Настройте конфигурационный файл config.json");
                    Program.isExit = true;
                    Application.Exit();
                    return false;
                }

                JArray arr_programm = (JArray) json_obj["programs_list"];
                
                List<Config> conf = new List<Config>();
                for (int i = 0; arr_programm.Count > i; i++)
                {
                    Console.WriteLine(arr_programm[i]["name"]);
                    Logger.Log((string)arr_programm[i]["name"]);
                    conf.Add(new Config((string)arr_programm[i]["name"], (string)arr_programm[i]["path"], (string)arr_programm[i]["arg"], (bool)arr_programm[i]["default_install"], (string)arr_programm[i]["restart"]));
                }

                return conf;
            }
            else
            {
                File.Create("config.json").Close();
                File.WriteAllText("config.json", Config.getConfigExample());
                MessageBox.Show("Создан новый конфигурационный файл config.json\nНастройте его.", "Уведомление", MessageBoxButtons.OK);
                Logger.Log("Создан новый конфигурационный файл config.json\nНастройте его.");
                Program.isExit = true;
                Application.Exit();
                return false;
            }
            
        }

    }
}
