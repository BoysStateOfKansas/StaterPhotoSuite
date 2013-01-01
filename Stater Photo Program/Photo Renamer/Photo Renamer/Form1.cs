using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Photo_Renamer
{
    public partial class Form1 : Form
    {
        //The dialogs to open and save files
        private FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
        private OpenFileDialog _openFileDialog = new OpenFileDialog();
        //seperators to convert input from the database into useable code
        private char[] _separators = new char[] { ' ', '\n' };
        private List<Stater> staters = new List<Stater>();

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialog.SelectedPath;
                FileInfo[] files = new DirectoryInfo(path).GetFiles("*.JPG");
                for (int i = 0; i < files.Length; i++)
                {
                    string newFile = String.Format("{0}{1}.{2}", staters[i].LastName+"_", staters[i].FirstName, "JPG");
                    string fullNewPath = files[i].DirectoryName;
                    File.Move(files[i].FullName, Path.Combine(fullNewPath, newFile));
                }
            }


        }

        private void loadSNPSPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _openFileDialog.FileName = "SNPCC.csv";
            _openFileDialog.Filter = "csv files (*.csv)|*.csv";
            _openFileDialog.DefaultExt = "txt";
            _openFileDialog.FilterIndex = 2;
            _openFileDialog.AddExtension = true;
            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = _openFileDialog.FileName;
                CsvFileReader csv = new CsvFileReader(fileName);
                char[] _separators = new char[] { '\n', '"', ',' };
                CsvRow row = new CsvRow();
                bool entering = true;
                while (entering == true)
                {
                    entering = csv.ReadRow(row);
                    staters.Add(new Stater(row[1], row[0]));
                    char[] bar = row[2].ToCharArray();
                    string bs = Convert.ToString(bar[0]) + Convert.ToString(bar[1]) + Convert.ToString(bar[2]) + Convert.ToString(bar[3]);
                    staters[staters.Count - 1].Barcode = Convert.ToInt32(bs);
                    staters[staters.Count - 1].City = row[3];
                    staters[staters.Count - 1].County = row[4];
                }

                csv.Close();
                staters.RemoveAt(staters.Count - 1);
                List<Stater> testing = staters;
            }

        }
    }
}
