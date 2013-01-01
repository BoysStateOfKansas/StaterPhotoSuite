using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        private List<Stater> staters = new List<Stater>();
        private List<Stater> photoList = new List<Stater>();
        private List<Stater> check = new List<Stater>();
        private int counter = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog();
            _openFileDialog.FileName = "SNPCC.csv";
            _openFileDialog.Filter = "csv files (*.csv)|*.csv";
            _openFileDialog.DefaultExt = "csv";
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
                    staters.Add(new Stater(row[1], row[2]));
                    char[] bar = row[0].ToCharArray();
                    string bs = Convert.ToString(bar[0]) + Convert.ToString(bar[1]) + Convert.ToString(bar[2]) + Convert.ToString(bar[3]);
                    staters[staters.Count - 1].Barcode = Convert.ToInt32(bs);
                    staters[staters.Count - 1].City = row[3];
                    staters[staters.Count - 1].County = row[4];
                }

                csv.Close();

            }
            _openFileDialog.FileName = "MasterPhotoList.csv";
            _openFileDialog.Filter = "csv files (*.csv)|*.csv";
            _openFileDialog.DefaultExt = "csv";
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
                    if (entering == true)
                    {
                        photoList.Add(new Stater(row[1], row[0]));
                        char[] bar = row[2].ToCharArray();
                        string bs = Convert.ToString(bar[0]) + Convert.ToString(bar[1]) + Convert.ToString(bar[2]) + Convert.ToString(bar[3]);
                        photoList[photoList.Count - 1].Barcode = Convert.ToInt32(bs);
                    }
                }

                csv.Close();


            }
            SaveFileDialog MySaveFileDialog = new SaveFileDialog();

            MySaveFileDialog.DefaultExt = "csv";
            if (MySaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream myStream = MySaveFileDialog.OpenFile())
                {
                    if (myStream != null)
                    {
                        List<Stater> temp = new List<Stater>();
                        for (int i = 0; i < photoList.Count; i++)
                        {
                            temp.Add(new Stater(photoList[i].FirstName, photoList[i].LastName));
                            temp[temp.Count - 1].Barcode = photoList[i].Barcode;
                        }

                        for (int i = 0; i < photoList.Count; i++)
                        {
                            for (int j = 0; j < staters.Count; j++)
                            {
                                if (photoList[i].Barcode == staters[j].Barcode)
                                {
                                    staters.RemoveAt(j);
                                }
                            }
                        }
                        for (int i = 0; i < photoList.Count; i++)
                        {
                            for (int j = 0; j < staters.Count; j++)
                            {
                                if (photoList[i].Barcode == staters[j].Barcode)
                                {
                                    staters.RemoveAt(j);
                                }
                            }
                        }

                        using (StreamWriter myWritter = new StreamWriter(myStream, System.Text.Encoding.ASCII))
                        {

                            for (int i = 0; i < staters.Count; i++)
                            {
                                myWritter.WriteLine(staters[i].FirstName + "," + staters[i].LastName + "," + staters[i].Barcode + "," + staters[i].City + "," + staters[i].County);

                            }


                        }
                    }
                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog _openFileDialog = new OpenFileDialog();
            _openFileDialog.FileName = "MasterPhotoList.csv";
            _openFileDialog.Filter = "csv files (*.csv)|*.csv";
            _openFileDialog.DefaultExt = "csv";
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
                    if (entering == true)
                    {
                        photoList.Add(new Stater(row[1], row[0]));
                        char[] bar = row[2].ToCharArray();
                        string bs = Convert.ToString(bar[0]) + Convert.ToString(bar[1]) + Convert.ToString(bar[2]) + Convert.ToString(bar[3]);
                        photoList[photoList.Count - 1].Barcode = Convert.ToInt32(bs);
                    }
                }

                csv.Close();


            }
            
            _openFileDialog.FileName = "Missing Staters.csv";
            _openFileDialog.Filter = "csv files (*.csv)|*.csv";
            _openFileDialog.DefaultExt = "csv";
            _openFileDialog.FilterIndex = 2;
            _openFileDialog.AddExtension = true;
            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = _openFileDialog.FileName;
                CsvFileReader csv = new CsvFileReader(fileName);
                char[] _separators = new char[] { '\n', '"', ',' };
                counter = 0;
                CsvRow row = new CsvRow();
                bool entering = true;
                while (entering == true)
                {
                    entering = csv.ReadRow(row);
                    if (entering == true)
                    {
                        check.Add(new Stater(row[0], row[1]));
                        char[] bar = row[2].ToCharArray();
                        string bs = Convert.ToString(bar[1]) + Convert.ToString(bar[1]) + Convert.ToString(bar[2]) + Convert.ToString(bar[3]);
                        
                            check[counter].Barcode = Convert.ToInt32(bs);
                            check[counter].City = row[3];
                            check[counter].County = row[4];
                        
                    }
                    counter++;
                }

                csv.Close();


            }

            SaveFileDialog MySaveFileDialog = new SaveFileDialog();

            MySaveFileDialog.DefaultExt = "csv";
            if (MySaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (Stream myStream = MySaveFileDialog.OpenFile())
                {
                    if (myStream != null)
                    {
                        for (int i = 0; i < counter-1; i++)
                        {
                            for (int j = 0; j < photoList.Count; j++)
                            {
                                bool contains = photoList.Contains(check[i]);
                                if (contains==true)
                                {
                                    check.RemoveAt(i);
                                }
                            }
                        }
                        

                        using (StreamWriter myWritter = new StreamWriter(myStream, System.Text.Encoding.ASCII))
                        {

                            for (int i = 0; i < check.Count; i++)
                            {
                                myWritter.WriteLine(check[i].FirstName + "," + check[i].LastName + "," + check[i].Barcode + "," + check[i].City + "," + check[i].County);

                            }


                        }
                    }
                }

            }
        }
    }
}

