using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace claer_double_link
{
    public partial class Form1 : Form
    {
        List<string> list_patch_file = new List<string>();
        List<string> list_name_file = new List<string>();
        List<string> list_double = new List<string>();
        string patch = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text|*.txt";
            list_patch_file.Clear();
            list_name_file.Clear();
            list_double.Clear();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string patchfile = fileDialog.FileName;
                MessageBox.Show(patchfile);
                string[] line_in_file = File.ReadAllLines(patchfile);
                string temp = "";
                foreach(string line in line_in_file)
                {
                    
                    if (list_name_file.Count > 0)
                    {
                        temp = line.Substring(line.LastIndexOf("/") + 1);
                        if (!list_name_file.Contains(temp))
                        {
                            list_name_file.Add(temp);
                            list_patch_file.Add(line);
                        }
                        else
                        {
                            list_double.Add(temp);
                        }
                    }
                    else
                    {
                        temp = line.Substring(line.LastIndexOf("/") + 1);
                        list_name_file.Add(temp);
                        list_patch_file.Add(line);
                    }
                }
                listBox1.Items.Clear();
                listBox1.Items.AddRange(list_double.ToArray());
                MessageBox.Show(list_double.Count.ToString());
                patch = patchfile;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (patch != null)
            {
                string patchfile = patch;
                do
                {
                    patchfile = patchfile.Substring(0, patchfile.LastIndexOf(".txt")) + "_new.txt";
                } while (File.Exists(patchfile));
                File.WriteAllLines(patchfile, list_patch_file.ToArray());
                MessageBox.Show("OK");
            }
        }
    }
}
