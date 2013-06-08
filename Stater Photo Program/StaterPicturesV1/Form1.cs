using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using StaterOrganizer.Properties;
using System.Threading;


namespace StaterOrganizer
{
    public partial class Form1 : Form
    {
        //Fields
        #region fields
        //The dialogs to open and save files
        private SaveFileDialog MySaveFileDialog = new SaveFileDialog();
        //seperators to convert input from the database into useable code
        private char[] _separators = new char[] { ' ', '\n' };
        //Lists used to maintain record creation functions
        private List<Stater> staters = new List<Stater>();
        private List<int> pictureBarcodes = new List<int>();
        //booleans for error and user stability
        private bool SNPhasBeenLoaded = false;
        private bool error = false;
        private bool workSaved = true;
        //program that is running
        private String programRunning = "";
        //CheckBoxes for the activities functions
        private CheckBox b = new CheckBox();
        private CheckBox c = new CheckBox();
        private CheckBox t = new CheckBox();
        //labels of the the checkboxes
        private Label band = new Label();
        private Label chorus = new Label();
        private Label talent = new Label();
        private Label activities = new Label();
        TextBox act = new TextBox();
        #endregion

        //Initialize Form1
        public Form1()
        {
            InitializeComponent();

        }

        //execute programming on load
        private void Form1_Load(object sender, EventArgs e)
        {

            Picture.KeyUp += new KeyEventHandler(listBox1_KeyPress);
            listBox1.Click += new EventHandler(listBox1_Click);

        }

        //Exit function
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkExitStatus())
            {
                this.Dispose();
            }
        }

        //Method to create records
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Picture.Text != "")
            {
                loadPictureList();
                if (SNPhasBeenLoaded == true)
                {
                    if (programRunning == "City Photos")
                    {
                        MySaveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                        MySaveFileDialog.DefaultExt = "txt";
                        MySaveFileDialog.FilterIndex = 2;
                        MySaveFileDialog.RestoreDirectory = true;
                        if (MySaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            using (Stream myStream = MySaveFileDialog.OpenFile())
                            {
                                if (myStream != null)
                                {
                                    using (StreamWriter myWritter = new StreamWriter(myStream, System.Text.Encoding.ASCII))
                                    {
                                        bool writenCity = false;

                                        for (int j = 0; j < pictureBarcodes.Count; j++)
                                        {
                                            for (int i = 0; i < staters.Count; i++)
                                            {
                                                if (staters[i].Barcode == pictureBarcodes[j])
                                                {

                                                    if (writenCity == false)
                                                    {
                                                        myWritter.WriteLine("City: " + staters[i].City);
                                                        writenCity = true;
                                                    }

                                                    myWritter.WriteLine(staters[i].FirstName + " " + staters[i].LastName);
                                                    break;

                                                }
                                            }
                                        }

                                    }
                                }
                            }
                            Picture.Text = "";
                            label1.Text = "Picture Roster Has Been Created.";
                            workSaved = true;
                            listBox1.Items.Clear();
                            Picture.Focus();
                        }
                    }
                    if (programRunning == "Individual Stater Photos")
                    {
                        MySaveFileDialog.DefaultExt = "csv";
                        if (MySaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            using (Stream myStream = MySaveFileDialog.OpenFile())
                            {
                                if (myStream != null)
                                {
                                    using (StreamWriter myWritter = new StreamWriter(myStream, System.Text.Encoding.ASCII))
                                    {
                                        for (int j = 0; j < pictureBarcodes.Count; j++)
                                        {
                                            for (int i = 0; i < staters.Count; i++)
                                            {
                                                if (staters[i].Barcode == pictureBarcodes[j])
                                                {

                                                    myWritter.WriteLine(staters[i].LastName + "," + staters[i].FirstName + "," + staters[i].Barcode + "," + staters[i].City + "," + staters[i].County);
                                                    break;

                                                }
                                            }
                                        }

                                    }
                                }
                            }
                            Picture.Text = "";
                            label1.Text = "Picture Roster Has Been Created.";
                            listBox1.Items.Clear();
                            Picture.Focus();
                        }
                    }
                    if (programRunning == "Stater Registration")
                    {
                        var organizedStaterList = staters.OrderBy(x => x.City).ToList();
                        #region Creating Band List
                        MySaveFileDialog.DefaultExt = "csv";
                        MySaveFileDialog.FileName = "Band Members";
                        if (MySaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            using (Stream myStream = MySaveFileDialog.OpenFile())
                            {
                                if (myStream != null)
                                {
                                    using (StreamWriter myWritter = new StreamWriter(myStream, System.Text.Encoding.ASCII))
                                    {
                                        myWritter.WriteLine("Stater Pin" + "," + "First Name" + "," + "Last Name" + "," + "City" + "," + "County");

                                        foreach (Stater Stater in organizedStaterList)
                                        {
                                            if (Stater.Band)
                                            {
                                                myWritter.WriteLine(Stater.Barcode + "," + Stater.FirstName + "," + Stater.LastName + "," + Stater.City + "," + Stater.County);
                                            }
                                        }

                                    }
                                }
                            }
                            workSaved = true;
                        }
                        #endregion
                        #region Creating Chorus List
                        MySaveFileDialog.DefaultExt = "csv";
                        MySaveFileDialog.FileName = "Choir Members";
                        if (MySaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            using (Stream myStream = MySaveFileDialog.OpenFile())
                            {
                                if (myStream != null)
                                {
                                    using (StreamWriter myWritter = new StreamWriter(myStream, System.Text.Encoding.ASCII))
                                    {
                                        myWritter.WriteLine("Stater Pin" + "," + "First Name" + "," + "Last Name" + "," + "City" + "," + "County");

                                        foreach (Stater Stater in organizedStaterList)
                                        {
                                            if (Stater.Chorus)
                                            {
                                                myWritter.WriteLine(Stater.Barcode + "," + Stater.FirstName + "," + Stater.LastName + "," + Stater.City + "," + Stater.County);
                                            }
                                        }

                                    }
                                }
                            }
                            workSaved = true;
                        }
                        #endregion
                        #region Creating Talent Show List
                        MySaveFileDialog.DefaultExt = "csv";
                        MySaveFileDialog.FileName = "Talent Show Participants";
                        if (MySaveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            using (Stream myStream = MySaveFileDialog.OpenFile())
                            {
                                if (myStream != null)
                                {
                                    using (StreamWriter myWritter = new StreamWriter(myStream, System.Text.Encoding.ASCII))
                                    {
                                        myWritter.WriteLine("Stater Pin" + "," + "First Name" + "," + "Last Name" + "," + "City" + "," + "County" + "," + "Act");

                                        foreach (Stater Stater in organizedStaterList)
                                        {
                                            if (Stater.Talent)
                                            {
                                                myWritter.WriteLine(Stater.Barcode + "," + Stater.FirstName + "," + Stater.LastName + "," + Stater.City + "," + Stater.County + "," + Stater.Act);
                                            }
                                        }

                                    }
                                }
                            }
                            workSaved = true;
                        }
                        #endregion
                        Picture.Text = "";
                        label1.Text = "Activity Spread Sheets Have Been Created.";
                        t.Checked = false;
                        b.Checked = false;
                        c.Checked = false;
                        act.Text = "";
                        listBox1.Items.Clear();
                        Picture.Focus();
                    }
                }

            }
            else
            {
                label1.Text = "No Data Has Been Entered.";
            }
        }

        //Method to Show activity
        private void listBox1_Click(object sender, EventArgs e)
        {
            if (programRunning == "Stater Registration")
            {
                try
                {
                    int pinIndex = listBox1.SelectedIndex;
                    int pin = pictureBarcodes[pinIndex];
                    int staterIndex = staters.FindIndex(x => x.Barcode == pin);
                    b.Checked = staters[staterIndex].Band;
                    c.Checked = staters[staterIndex].Chorus;
                    t.Checked = staters[staterIndex].Talent;
                    act.Text = staters[staterIndex].Act;
                    Picture.Focus();
                }
                catch
                {
                }
            }
            else
            {
                Picture.Focus();
            }
        }

        //Method to dynamically update user interface.
        private void listBox1_KeyPress(object sender, KeyEventArgs e)
        {
            List<int> pinsToRemove = new List<int>();
            bool duplicateDetected = false;
            listBox1.Items.Clear();
            workSaved = false;

            if (SNPhasBeenLoaded == true)
            {
                if (e.KeyValue == (char)Keys.Return)
                {
                    loadPictureList();
                    if (programRunning == "City Photos" || programRunning == "Individual Stater Photos")
                    {
                        Picture.Clear();
                        listBox1.Items.Clear();
                        if (checkForDuplicateUserEntry())
                        {
                            duplicateDetected = true;
                        }
                        foreach (int pin in pictureBarcodes)
                        {
                            int index = staters.FindIndex(x => x.Barcode == pin);
                            if (index != -1)
                            {
                                label1.Text = "Stater Found";
                                if (duplicateDetected)
                                {
                                    label1.Text = "Barcode has already been entered.";
                                }
                                listBox1.Items.Add(staters[index].FirstName + " " + staters[index].LastName);
                                Picture.Text += pin.ToString() + "\n";
                            }
                            else
                            {
                                label1.Text = "Stater not found for " + pin;
                            }
                        }
                        workSaved = false;
                        createBackup();
                        Picture.Focus();
                        Picture.SelectionStart = Picture.Text.Length + 1;
                    }
                    if (programRunning == "Stater Registration")
                    {
                        Picture.Clear();
                        listBox1.Items.Clear();
                        if (checkForDuplicateUserEntry())
                        {
                            duplicateDetected = true;
                        }
                        foreach (int pin in pictureBarcodes)
                        {
                            int index = staters.FindIndex(x => x.Barcode == pin);
                            if (index != -1)
                            {
                                b.Checked = staters[index].Band;
                                c.Checked = staters[index].Chorus;
                                t.Checked = staters[index].Talent;
                                act.Text = staters[index].Act;
                                label1.Text = "Stater Found";
                                if (duplicateDetected)
                                {
                                    label1.Text = "Barcode has already been entered.";
                                }
                                listBox1.Items.Add(staters[index].FirstName + " " + staters[index].LastName);
                                Picture.Text += pin.ToString() + "\n";
                            }
                            else
                            {
                                b.Checked = false;
                                c.Checked = false;
                                t.Checked = false;
                                act.Text = "";
                                label1.Text = "Stater not found for " + pin;
                            }
                        }
                        workSaved = false;
                        createBackup();
                        Picture.Focus();
                        Picture.SelectionStart = Picture.Text.Length + 1;
                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    }
                }
            }

        }

        //Method to load and convert barcodes from the textbox to stater barcodes
        private void loadPictureList()
        {
            try
            {
                pictureBarcodes.Clear();

                string[] staterbarcode = Picture.Text.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < staterbarcode.Length; i++)
                {
                    pictureBarcodes.Add(Convert.ToInt32(staterbarcode[i]));
                }
            }
            catch
            {
            }


        }

        //Clears the current lists
        private void clearCurrentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkClearStatus())
            {
                b.Checked = false;
                c.Checked = false;
                t.Checked = false;
                act.Text = "";
                Picture.Text = "";
                listBox1.Items.Clear();
                Picture.Focus();
            }
        }

        //Method to Load SNP for City Photos
        private void createSNPFromcsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkSwitchStatus())
            {
                Picture.Text = "";
                listBox1.Items.Clear();
                listBox1.SelectionMode = SelectionMode.None;
                programRunning = "City Photos";
                this.Text = programRunning;
                error = false;
                this.Controls.Remove(b);
                this.Controls.Remove(c);
                this.Controls.Remove(t);
                this.Controls.Remove(band);
                this.Controls.Remove(chorus);
                this.Controls.Remove(talent);
                this.Controls.Remove(activities);
                this.Size = new Size(282, 326);
                Picture.Text = "";
                listBox1.Items.Clear();
                Picture.Focus();
                staters.Clear();
                string pathstring = loadStaterList();
                CsvFileReader csv = new CsvFileReader(pathstring);
                char[] _separators = new char[] { '\n', '"', ',' };
                CsvRow row = new CsvRow();
                bool entering = true;
                while (entering == true)
                {
                    entering = csv.ReadRow(row);
                    if (row.Count != 5)
                    {
                        label1.Size = new Size(200, 30);
                        label1.Location = new Point(73, 24);
                        label1.Text = pathstring + " invalid format;\nCorrect Format: First Name, Last Name, Barcode";
                        error = true;

                    }
                    staters.Add(new Stater(row[1], row[2]));
                    char[] bar = row[0].ToCharArray();
                    string bs = Convert.ToString(bar[0]) + Convert.ToString(bar[1]) + Convert.ToString(bar[2]) + Convert.ToString(bar[3]);
                    staters[staters.Count - 1].Barcode = Convert.ToInt32(bs);
                    staters[staters.Count - 1].City = row[3];
                    if (staters.Count() > 1 && staters[staters.Count - 1].Barcode == staters[staters.Count - 2].Barcode)
                    {
                        staters.RemoveAt(staters.Count - 1);
                        break;
                    }
                }
                csv.Close();
                rows();
                SNPhasBeenLoaded = true;
                checkStaterList();
                if (error == false)
                {
                    label1.Size = new Size(130, 30);
                    label1.Location = new Point(73, 24);
                    label1.Text = "Stater list successfully loaded.";

                    Picture.Enabled = true;
                    button1.Enabled = true;
                    Picture.Focus();
                }
                else
                {
                    label1.Size = new Size(130, 30);
                    label1.Location = new Point(73, 24);
                    label1.Text = "The stater list contains an error";
                }
            }
        }

        //Method to load SNP for Stater Photos
        private void loadSNPForStaterPhotosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkSwitchStatus())
            {
                Picture.Text = "";
                listBox1.Items.Clear();
                error = false;
                programRunning = "Individual Stater Photos";
                listBox1.SelectionMode = SelectionMode.None;
                this.Text = programRunning;
                this.Controls.Remove(b);
                this.Controls.Remove(c);
                this.Controls.Remove(t);
                this.Controls.Remove(band);
                this.Controls.Remove(chorus);
                this.Controls.Remove(talent);
                this.Controls.Remove(activities);
                this.Size = new Size(282, 326);
                Picture.Text = "";
                listBox1.Items.Clear();
                Picture.Focus();
                staters.Clear();
                createStaterObjects();
                if (error == false)
                {
                    label1.Size = new Size(130, 30);
                    label1.Location = new Point(73, 24);
                    label1.Text = "Stater list successfully loaded.";
                    Picture.Enabled = true;
                    button1.Enabled = true;
                    Picture.Focus();
                }
                else
                {
                    label1.Size = new Size(130, 30);
                    label1.Location = new Point(73, 24);
                    label1.Text = "The stater list contains an error";
                }
            }
        }

        //Method to load SNP for Activities Registration
        private void loadSNPForActivitiesRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkSwitchStatus())
            {
                Picture.Text = "";
                listBox1.Items.Clear();
                error = false;
                programRunning = "Stater Registration";
                listBox1.SelectionMode = SelectionMode.One;
                this.Text = programRunning;
                this.Controls.Remove(b);
                this.Controls.Remove(c);
                this.Controls.Remove(t);
                this.Controls.Remove(band);
                this.Controls.Remove(chorus);
                this.Controls.Remove(talent);
                this.Controls.Remove(activities);
                this.Size = new Size(282, 326);
                Picture.Text = "";
                label1.Text = "Activity Spread Sheets Have Been Created.";
                listBox1.Items.Clear();
                Picture.Focus();
                setUpCheckBoxes();
                staters.Clear();
                createStaterObjects();
                SNPhasBeenLoaded = true;
                if (error == false)
                {
                    label1.Size = new Size(130, 30);
                    label1.Location = new Point(73, 24);
                    label1.Text = "Stater list successfully loaded.";

                    Picture.Enabled = true;
                    button1.Enabled = true;
                    Picture.Focus();
                }
                else
                {
                    label1.Size = new Size(130, 30);
                    label1.Location = new Point(73, 24);
                    label1.Text = "The stater list contains an error";
                }
            }
        }

        //Method to hard code row barcodes.
        private void rows()
        {
            staters.Add(new Stater("Row", "1"));
            staters[staters.Count - 1].Barcode = 0001;
            staters.Add(new Stater("Row", "2"));
            staters[staters.Count - 1].Barcode = 0002;
            staters.Add(new Stater("Row", "3"));
            staters[staters.Count - 1].Barcode = 0003;
            staters.Add(new Stater("Row", "4"));
            staters[staters.Count - 1].Barcode = 0004;
            staters.Add(new Stater("Row", "5"));
            staters[staters.Count - 1].Barcode = 0005;
            staters.Add(new Stater("Row", "6"));
            staters[staters.Count - 1].Barcode = 0006;
            staters.Add(new Stater("Row", "7"));
            staters[staters.Count - 1].Barcode = 0007;
            staters.Add(new Stater("Row", "8"));
            staters[staters.Count - 1].Barcode = 0008;
            staters.Add(new Stater("Row", "9"));
            staters[staters.Count - 1].Barcode = 0009;
            staters.Add(new Stater("Row", "10"));
            staters[staters.Count - 1].Barcode = 0010;
        }

        //Method to set up Check Boxes
        private void setUpCheckBoxes()
        {
            this.Size = new Size(700, 326);
            b.Location = new Point(370, 106);
            c.Location = new Point(370, 156);
            t.Location = new Point(370, 206);
            band.Location = new Point(337, 110);
            chorus.Location = new Point(329, 160);
            talent.Location = new Point(302, 210);
            activities.Location = new Point(320, 50);
            act.Location = new Point(302, 240);
            act.Size = new Size(200, 20);
            band.Text = "Band:";
            chorus.Text = "Chorus:";
            talent.Text = "Talent Show:";
            activities.Text = "Stater's Activities:";
            b.Click += new EventHandler(bandBox);
            c.Click += new EventHandler(chorusBox);
            t.Click += new EventHandler(talentBox);
            act.TextChanged += new EventHandler(talentAct);
            act.KeyDown += new KeyEventHandler(talentActEntry);
            this.Controls.Add(b);
            this.Controls.Add(c);
            this.Controls.Add(t);
            this.Controls.Add(band);
            this.Controls.Add(chorus);
            this.Controls.Add(talent);
            this.Controls.Add(activities);
            this.Controls.Add(act);
            t.Checked = false;
            b.Checked = false;
            c.Checked = false;
            act.Text = "";
        }

        //Event Handler for talent checkbox
        public void talentBox(object sender, EventArgs e)
        {
            if (Picture.Text != "")
            {
                int pinIndex = listBox1.SelectedIndex;
                int pin = pictureBarcodes[pinIndex];
                int staterIndex = staters.FindIndex(x => x.Barcode == pin);
                if (t.Checked == true)
                {
                    staters[staterIndex].Talent = true;
                    staters[staterIndex].Act = act.Text;
                }
                else
                {
                    staters[staterIndex].Talent = false;
                    staters[staterIndex].Act = "";
                    act.Text = "";
                }
                Picture.Focus();
                createBackup();
            }
            else
            {
                t.Checked = false;
                act.Text = "";
                Picture.Focus();
                createBackup();
            }
        }

        //Event Handler for talent act textbox
        private void talentAct(object sender, EventArgs e)
        {
            try
            {
                int pinIndex = listBox1.SelectedIndex;
                int pin = pictureBarcodes[pinIndex];
                int staterIndex = staters.FindIndex(x => x.Barcode == pin);
                if (act.Text != "")
                {
                    t.Checked = true;
                    staters[staterIndex].Talent = true;
                    staters[staterIndex].Act = act.Text;
                }
                else
                {
                    t.Checked = false;
                    staters[staterIndex].Talent = false;
                }
            }
            catch
            {
            }
            createBackup();
        }

        //Event Handler for talent act textbox
        private void talentActEntry(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Return)
            {
                Picture.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
                createBackup();
            }
        }

        //Event Handler for band checkbox
        public void bandBox(object sender, EventArgs e)
        {
            if (Picture.Text != "" && listBox1.SelectedIndex != -1)
            {
                int pinIndex = listBox1.SelectedIndex;
                int pin = pictureBarcodes[pinIndex];
                int staterIndex = staters.FindIndex(x => x.Barcode == pin);
                if (b.Checked)
                {
                    staters[staterIndex].Band = true;
                    c.Checked = false;
                    staters[staterIndex].Chorus = false;
                }
                else
                {
                    staters[staterIndex].Band = false;
                }
                Picture.Focus();
                createBackup();
            }
            else
            {
                b.Checked = false;
                Picture.Focus();
                createBackup();
            }
        }

        //Event Handler for chorus checkbox
        public void chorusBox(object sender, EventArgs e)
        {
            if (Picture.Text != "")
            {
                int pinIndex = listBox1.SelectedIndex;
                int pin = pictureBarcodes[pinIndex];
                int staterIndex = staters.FindIndex(x => x.Barcode == pin);
                if (c.Checked == true)
                {
                    staters[staterIndex].Chorus = true;
                    b.Checked = false;
                    staters[staterIndex].Band = false;
                }
                else
                {
                    staters[staterIndex].Chorus = false;
                }
                createBackup();
                Picture.Focus();
            }
            else
            {
                createBackup();
                c.Checked = false;
                Picture.Focus();
            }
        }

        //Accessing the integrated stater list.
        private string loadStaterList()
        {
            string path = Directory.GetCurrentDirectory();
            string pathString = System.IO.Path.Combine(path, "SNPCC.csv");
            return pathString;
        }

        //Checks the stater list for duplicates.
        private void checkStaterList()
        {
            List<int> Pins = new List<int>();
            List<string> Names = new List<string>();
            List<string> Errors = new List<string>();
            for (int i = 0; i < staters.Count(); i++)
            {
                Pins.Add(staters[i].Barcode);
                Names.Add(staters[i].FirstName + " " + staters[i].LastName);
            }
            var duplicatePins = Pins.GroupBy(x => x)
                             .Where(g => g.Count() > 1)
                             .Select(g => g.Key)
                             .ToList();
            var duplicateNames = Names.GroupBy(x => x)
                             .Where(g => g.Count() > 1)
                             .Select(g => g.Key)
                             .ToList();
            if (duplicatePins.Count() > 0)
            {
                Errors.Add("The stater list contains a duplicate pin.");
                foreach (int Pin in duplicatePins)
                {
                    Errors.Add("Duplicate pin: " + Pin);
                }
            }
            if (duplicateNames.Count() > 0)
            {
                Errors.Add("The stater list contains a duplicate stater.");
                foreach (String Name in duplicateNames)
                {
                    Errors.Add("Duplicate stater: " + Name);
                }
            }
            if (Errors.Count() > 0)
            {
                error = true;
                Form2 form2 = new Form2(Errors);
                form2.Show();
            }
        }

        //Upload the stater list into the program.
        private void createStaterObjects()
        {
            staters.Clear();
            string pathstring = loadStaterList();
            CsvFileReader csv = new CsvFileReader(pathstring);
            char[] _separators = new char[] { '\n', '"', ',' };
            CsvRow row = new CsvRow();
            bool entering = true;
            while (entering == true)
            {
                entering = csv.ReadRow(row);
                if (row.Count != 5)
                {
                    label1.Size = new Size(200, 30);
                    label1.Location = new Point(50, 20);
                    label1.Text = pathstring + " invalid format;\nCorrect Format: First Name, Last Name, Barcode";
                    error = true;

                }
                staters.Add(new Stater(row[1], row[2]));
                char[] bar = row[0].ToCharArray();
                string bs = Convert.ToString(bar[0]) + Convert.ToString(bar[1]) + Convert.ToString(bar[2]) + Convert.ToString(bar[3]);
                staters[staters.Count - 1].Barcode = Convert.ToInt32(bs);
                staters[staters.Count - 1].City = row[3];
                staters[staters.Count - 1].County = row[4];
                if (staters.Count() > 1 && staters[staters.Count - 1].Barcode == staters[staters.Count - 2].Barcode)
                {
                    staters.RemoveAt(staters.Count - 1);
                    break;
                }
            }
            csv.Close();
            SNPhasBeenLoaded = true;
        }

        //Check the users entries for duplicates.
        private bool checkForDuplicateUserEntry()
        {
            var duplicatePins = pictureBarcodes.GroupBy(x => x)
                             .Where(g => g.Count() > 1)
                             .Select(g => g.Key)
                             .ToList();

            if (duplicatePins.Count > 0)
            {
                foreach (int pin in duplicatePins)
                {
                    pictureBarcodes.Remove(pin);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        //Create backup
        private void createBackup()
        {
            string path = Directory.GetCurrentDirectory();
            string pathString = System.IO.Path.Combine(path, "backUp.txt");
            FileStream fs = File.Create(pathString);
            fs.Close();
            if (programRunning == "City Photos" || programRunning == "Individual Stater Photos")
            {
                using (StreamWriter writer = new StreamWriter(pathString, true))
                {
                    if (pictureBarcodes.Count() > 0)
                    {
                        writer.WriteLine(programRunning);
                        foreach (int Pin in pictureBarcodes)
                        {
                            writer.WriteLine(Pin);
                        }
                    }
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(pathString, true))
                {
                    writer.WriteLine(programRunning);
                    foreach (int Pin in pictureBarcodes)
                    {
                        writer.WriteLine(Pin);
                    }
                    writer.WriteLine("Band");
                    foreach (int Pin in pictureBarcodes)
                    {
                        int index = staters.FindIndex(x => x.Barcode == Pin);
                        if (index != -1 && staters[index].Band)
                        {
                            writer.WriteLine(Pin);
                        }
                    }
                    writer.WriteLine("Chorus");
                    foreach (int Pin in pictureBarcodes)
                    {
                        int index = staters.FindIndex(x => x.Barcode == Pin);
                        if (index != -1 && staters[index].Chorus)
                        {
                            writer.WriteLine(Pin);
                        }
                    }
                    writer.WriteLine("Talent");
                    foreach (int Pin in pictureBarcodes)
                    {
                        int index = staters.FindIndex(x => x.Barcode == Pin);
                        if (index != -1 && staters[index].Talent)
                        {
                            writer.WriteLine(Pin + "$" + staters[index].Act);
                        }
                    }
                }
            }
        }

        //Prevent user from accidently losing work.
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            exitToolStripMenuItem_Click(sender, e);
        }

        //Add a stater pin to the list up.
        private void addStaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (programRunning != "")
            {
                List<String> lines = new List<String>();
                int cursorPosition = Picture.SelectionStart;
                int lineIndex = Picture.GetLineFromCharIndex(cursorPosition);
                for (int i = 0; i < Picture.Lines.Count(); i++)
                {
                    if (i == lineIndex)
                    {
                        lines.Add("");
                    }
                    lines.Add(Picture.Lines[i]);
                }
                Picture.Clear();
                foreach (string pin in lines)
                {
                    Picture.Text += pin + "\n";
                }
                if (lineIndex == 0)
                {
                    Picture.SelectionStart = 0;
                }
                else
                {
                    Picture.SelectionStart = cursorPosition - 5;
                }
            }
        }

        //Add a stater pin to the list down.
        private void addPinToListDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (programRunning != "")
            {
                List<String> lines = new List<String>();
                int cursorPosition = Picture.SelectionStart;
                int lineIndex = Picture.GetLineFromCharIndex(cursorPosition);
                for (int i = 0; i < Picture.Lines.Count(); i++)
                {
                    if (i == lineIndex + 1)
                    {
                        lines.Add("");
                    }
                    lines.Add(Picture.Lines[i]);
                }
                Picture.Clear();
                foreach (string pin in lines)
                {
                    Picture.Text += pin + "\n";
                }
                Picture.SelectionStart = cursorPosition + 1;
            }
        }

        //Add a stater to the stater list.
        private void addStaterToStaterListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            createStaterObjects();
            Form3 form3 = new Form3();
            if (form3.ShowDialog() == DialogResult.OK)
            {
                staters.Add(new Stater(form3.firstName, form3.lastName));
                staters[staters.Count - 1].Barcode = form3.pin;
                staters[staters.Count - 1].City = form3.city;
                staters[staters.Count - 1].County = form3.county;
                string filePath = loadStaterList();
                using (CsvFileWriter writer = new CsvFileWriter(filePath))
                {
                    foreach (Stater s in staters)
                    {
                        CsvRow row = new CsvRow() { s.Barcode.ToString(), s.FirstName, s.LastName, s.City, s.County };
                        writer.WriteRow(row);
                    }
                }
                createStaterObjects();
                if (programRunning != "")
                {
                    Picture.Text += form3.pin + "\n";
                    Picture.Focus();
                    Picture.SelectionStart = Picture.Text.Length + 1;
                    SendKeys.Send("{ENTER}");
                }
            }
        }

        //Load backup.
        private void loadBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory();
            string pathString = System.IO.Path.Combine(path, "backUp.txt");
            if (File.Exists(pathString))
            {
                List<String> readText = File.ReadAllLines(pathString, Encoding.UTF8).ToList();
                List<String> pins = new List<string>();
                if (readText.Count() > 0)
                {
                    programRunning = readText[0];
                    Object senders = new Object();
                    EventArgs k = new EventArgs();
                    char[] separators = new char[] { ' ', '\n', '$' };
                    switch (programRunning)
                    {
                        case "City Photos":
                            createSNPFromcsvToolStripMenuItem_Click(senders, k);
                            readText.Remove(readText[0]);
                            foreach (String pin in readText)
                            {
                                Picture.Text += pin + "\n";
                            }
                            Picture.Focus();
                            Picture.SelectionStart = Picture.Text.Length + 1;
                            SendKeys.Send("{ENTER}");
                            break;
                        case "Individual Stater Photos":
                            loadSNPForStaterPhotosToolStripMenuItem_Click(senders, k);
                            readText.Remove(readText[0]);
                            foreach (String pin in readText)
                            {
                                Picture.Text += pin + "\n";
                            }
                            Picture.Focus();
                            Picture.SelectionStart = Picture.Text.Length + 1;
                            SendKeys.Send("{ENTER}");
                            break;
                        case "Stater Registration":
                            loadSNPForActivitiesRegistrationToolStripMenuItem_Click(senders, k);
                            readText.Remove(readText[0]);
                            int index = readText.FindIndex(x => x == "Band");
                            for (int i = 0; i < index; i++)
                            {
                                Picture.Text += readText[i] + "\n";
                                pins.Add(readText[i]);
                            }
                            foreach (String pin in pins)
                            {
                                readText.Remove(pin);
                            }
                            readText.Remove(readText[0]);
                            pins.Clear();
                            index = readText.FindIndex(x => x == "Chorus");
                            for (int i = 0; i < index; i++)
                            {
                                pins.Add(readText[i]);
                            }
                            foreach (String pin in pins)
                            {
                                int pinNum = Convert.ToInt32(pin);
                                int sindex = staters.FindIndex(x => x.Barcode == pinNum);
                                staters[sindex].Band = true;
                                readText.Remove(pin);
                            }
                            readText.Remove(readText[0]);
                            pins.Clear();
                            index = readText.FindIndex(x => x == "Talent");
                            for (int i = 0; i < index; i++)
                            {
                                pins.Add(readText[i]);
                            }
                            foreach (String pin in pins)
                            {
                                int pinNum = Convert.ToInt32(pin);
                                int sindex = staters.FindIndex(x => x.Barcode == pinNum);
                                staters[sindex].Chorus = true;
                                readText.Remove(pin);
                            }
                            readText.Remove(readText[0]);
                            foreach (String pin in readText)
                            {
                                List<String> pieces = pin.Split(separators).ToList();
                                int pinNum = Convert.ToInt32(pieces[0]);
                                int sindex = staters.FindIndex(x => x.Barcode == pinNum);
                                staters[sindex].Talent = true;
                                staters[sindex].Act = pieces[1];
                            }
                            Picture.Focus();
                            Picture.SelectionStart = Picture.Text.Length + 1;

                            SendKeys.Send("{ENTER}");
                            break;
                    }
                }
                else
                {
                    label1.Text = "No back up currently exists.";
                }
            }
        }

        //Check before exiting.
        private bool checkExitStatus()
        {
            bool run = true;
            if (!workSaved)
            {
                if (MessageBox.Show("Are you sure you wish to exit without saving?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    run = true;
                    workSaved = true;
                }
                else
                {
                    run = false;
                }
            }
            else
            {
                run = true;
            }
            return run;
        }

        //Check before clearing list.
        private bool checkClearStatus()
        {
            bool run = true;
            if (!workSaved)
            {
                if (MessageBox.Show("Do you want to clear the list without saving?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    run = true;
                    workSaved = true;
                }
                else
                {
                    run = false;
                }
            }
            else
            {
                run = true;
            }
            return run;
        }

        //Check before switching menus.
        private bool checkSwitchStatus()
        {
            bool run = true;
            if (!workSaved)
            {
                if (MessageBox.Show("Do you want to switch menus without saving?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    run = true;
                    workSaved = true;
                }
                else
                {
                    run = false;
                }
            }
            else
            {
                run = true;
            }
            return run;
        }

        //Manually updating the stater list.
        private void manuallyLoadNewSNPCCToSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter ="csv files (*.csv)|*.csv";
            string currentList = loadStaterList();
            string newList = "";
            string originalNewList = "";
            if (of.ShowDialog() == DialogResult.OK)
            {
                newList = of.FileName;
                originalNewList = newList;
                string directory = Path.GetDirectoryName(of.FileName);
                File.Move(newList, directory+"\\SNPCC.csv");
                newList = directory + "\\SNPCC.csv";
                File.Copy(newList, currentList, true);
                File.Move(newList, originalNewList);
            }

        }

    }
}