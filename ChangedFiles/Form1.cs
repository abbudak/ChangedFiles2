using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChangedFiles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            DialogResult result = fbd.ShowDialog();

            if (string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                MessageBox.Show(fbd.SelectedPath);
                return;
            }
            string[] files = Directory.GetFiles(fbd.SelectedPath, "*.*", SearchOption.AllDirectories);

            // Display all the files.
            foreach (string file in files)
            {
                FileInfo oFileInfo = new FileInfo(file);
                DateTime dtCreationTime = oFileInfo.CreationTime;
                if (dtCreationTime > dateTimePicker1.Value)
                {
                    listBox1.Items.Add(file + "-" + dtCreationTime.ToShortDateString());
                }
                
            }
        }
    }
}
