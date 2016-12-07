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
    public partial class ChangedFiles : Form
    {
        public ChangedFiles()
        {
            InitializeComponent();
            label2.Visible = false;
        }

        void OpenFile(string path)
        {
            string filePath = @dataGridView1.CurrentCell.Value.ToString();
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Please click on File Name");
                return;
            }
            string argument = "/select, \"" + filePath + "\"";
            System.Diagnostics.Process.Start("explorer.exe", argument);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.Cancel)
            {
                MessageBox.Show("Cancel Clicked");
                return;
            }

            if (string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                MessageBox.Show(fbd.SelectedPath);
                return;
            }

            string[] files = Directory.GetFiles(fbd.SelectedPath, "*.*", SearchOption.AllDirectories);

            FileInfo oFileInfo;
            DateTime dtCreationTime;
            int i = 0;

            // Display all the files.
            foreach (string file in files)
            {
                oFileInfo = new FileInfo(file);
                dtCreationTime = oFileInfo.LastWriteTime;
                if (dtCreationTime > dateTimePicker1.Value)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = file;
                    dataGridView1.Rows[i].Cells[1].Value = string.Concat(dtCreationTime.ToLongDateString(),dtCreationTime.ToLongTimeString());
                    i++;
                }
            }
            label2.Visible = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count> 0 && e.RowIndex != -1)
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                {
                    OpenFile(dataGridView1.CurrentCell.Value.ToString());
                }
            }
        }
    }
}
