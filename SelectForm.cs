using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoKit
{
    public partial class SelectForm : Form
    {
        public List<Config> selected_items = new List<Config>();
        private List<Config> list;
        public bool btnClose = false;

        public SelectForm(List<Config> configList)
        {
            InitializeComponent();
            this._list_of_programs.Items.Clear();
            this.list = configList;
            foreach (Config conf in configList)
            {
                this._list_of_programs.Items.Add(conf.getName() + ((conf.isReload() == "after" || conf.isReload() == "before") ? " [ПЕРЕЗАГРУЗКА]" : ""), conf.isDefaultInstall());
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            foreach (int i in this._list_of_programs.CheckedIndices)
            {
                Console.WriteLine(i);
                selected_items.Add(list[i]);
            }
            btnClose = true;
            this.Close();
        }
    }
}
