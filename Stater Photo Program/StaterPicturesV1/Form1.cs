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
        private List<Stater> activestaters = new List<Stater>();
        private List<Stater> bandMembers = new List<Stater>();
        private List<Stater> chorusMembers = new List<Stater>();
        private List<Stater> talentShowParticipants = new List<Stater>();
        private List<int> barcodes = new List<int>();
        private List<int> pictureBarcodes = new List<int>();
        //booleans for error and user stability
        private bool SNPhasBeenLoaded = false;
        private bool error = false;
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
            this.Dispose();
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

                                        for (int j = 0; j < bandMembers.Count; j++)
                                        {

                                            myWritter.WriteLine(bandMembers[j].Barcode + "," + bandMembers[j].FirstName + "," + bandMembers[j].LastName + "," + bandMembers[j].City + "," + bandMembers[j].County);
                                        }

                                    }
                                }
                            }
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

                                        for (int j = 0; j < chorusMembers.Count; j++)
                                        {

                                            myWritter.WriteLine(chorusMembers[j].Barcode + "," + chorusMembers[j].FirstName + "," + chorusMembers[j].LastName + "," + chorusMembers[j].City + "," + chorusMembers[j].County);
                                        }

                                    }
                                }
                            }
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

                                        for (int j = 0; j < talentShowParticipants.Count; j++)
                                        {

                                            myWritter.WriteLine(talentShowParticipants[j].Barcode + "," + talentShowParticipants[j].FirstName + "," + talentShowParticipants[j].LastName + "," + talentShowParticipants[j].City + "," + talentShowParticipants[j].County + "," + talentShowParticipants[j].Act);
                                        }

                                    }
                                }
                            }
                        }
                        #endregion
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
                b.Checked = false;
                c.Checked = false;
                t.Checked = false;
                act.Text = "";
                string name = listBox1.SelectedItem.ToString();
                string[] n = name.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < bandMembers.Count; i++)
                {
                    if (n[0] == bandMembers[i].FirstName && n[1] == bandMembers[i].LastName)
                    {
                        b.Checked = true;
                        act.Text = "";
                        Picture.Focus();
                        break;
                    }
                }
                for (int i = 0; i < chorusMembers.Count; i++)
                {
                    if (n[0] == chorusMembers[i].FirstName && n[1] == chorusMembers[i].LastName)
                    {
                        c.Checked = true;
                        act.Text = "";
                        Picture.Focus();
                        break;
                    }
                }
                for (int i = 0; i < talentShowParticipants.Count; i++)
                {
                    if (n[0] == talentShowParticipants[i].FirstName && n[1] == talentShowParticipants[i].LastName)
                    {
                        t.Checked = true;
                        act.Text = talentShowParticipants[i].Act;
                        Picture.Focus();
                        break;

                    }
                }
            }
        }

        //Method to dynamically update user interface.
        private void listBox1_KeyPress(object sender, KeyEventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add(" ");
            activestaters.Clear();

            // The keypressed method uses the KeyChar property to check 
            // whether the ENTER key is pressed. 

            // If the ENTER key is pressed, the Handled property is set to true, 
            // to indicate the event is handled.
            loadPictureList();
            if (programRunning == "City Photos")
            {
                if (SNPhasBeenLoaded == true)
                {
                    if (e.KeyValue == (char)Keys.Return)
                    {
                        TextWriter tw = new StreamWriter("FailSafeCityPhoto.txt");
                        bool staterFound = false;
                        String[] fs = Picture.Text.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < fs.Length; i++)
                        {
                            tw.WriteLine(fs[i]);
                        }
                        tw.Close();
                        for (int j = 0; j < pictureBarcodes.Count; j++)
                        {
                            for (int i = 0; i < staters.Count; i++)
                            {
                                if (staters[i].Barcode == pictureBarcodes[j])
                                {
                                    listBox1.Items.RemoveAt(listBox1.Items.Count - 1);
                                    listBox1.Items.Add(staters[i].FirstName + " " + staters[i].LastName);
                                    listBox1.Items.Add(" ");
                                    staterFound = true;
                                    label1.Text = "Stater Found";
                                    if (checkForDuplicateUserEntry())
                                    {
                                        Picture.Clear();
                                        pictureBarcodes.RemoveAt(pictureBarcodes.Count() - 1);
                                        foreach (int Pic in pictureBarcodes)
                                        {
                                            Picture.Text += Pic.ToString() + "\n";
                                        }
                                        label1.Text = "Barcode has already been entered.";
                                    }
                                    break;
                                }
                                else
                                {
                                    staterFound = false;
                                }
                            }
                        }
                        e.Handled = true;
                        if (!staterFound)
                        {
                            Picture.Clear();
                            pictureBarcodes.RemoveAt(pictureBarcodes.Count() - 1);
                            foreach (int Pic in pictureBarcodes)
                            {
                                Picture.Text += Pic.ToString() + "\n";
                            }
                            label1.Text = "Barcode Does Not Exist.";
                        }
                        Picture.Focus();
                        Picture.SelectionStart = Picture.Text.Length + 1;
                    }
                }
            }
            if (programRunning == "Individual Stater Photos")
            {
                if (SNPhasBeenLoaded == true)
                {
                    if (e.KeyValue == (char)Keys.Return)
                    {
                        bool staterFound = false;
                        for (int j = 0; j < pictureBarcodes.Count; j++)
                        {
                            for (int i = 0; i < staters.Count; i++)
                            {
                                if (staters[i].Barcode == pictureBarcodes[j])
                                {
                                    listBox1.Items.RemoveAt(listBox1.Items.Count - 1);
                                    listBox1.Items.Add(staters[i].LastName + " " + staters[i].FirstName);
                                    listBox1.Items.Add(" ");
                                    staterFound = true;
                                    label1.Text = "Stater Found";
                                    if (checkForDuplicateUserEntry())
                                    {
                                        Picture.Clear();
                                        pictureBarcodes.RemoveAt(pictureBarcodes.Count() - 1);
                                        foreach (int Pic in pictureBarcodes)
                                        {
                                            Picture.Text += Pic.ToString() + "\n";
                                        }
                                        label1.Text = "Barcode has already been entered.";
                                    }
                                    break;
                                }
                                else
                                {
                                    staterFound = false;
                                }
                            }
                        }
                        e.Handled = true;
                        if (!staterFound)
                        {
                            Picture.Clear();
                            pictureBarcodes.RemoveAt(pictureBarcodes.Count() - 1);
                            foreach (int Pic in pictureBarcodes)
                            {
                                Picture.Text += Pic.ToString() + "\n";
                            }
                            label1.Text = "Barcode Does Not Exist.";
                        }
                        Picture.Focus();
                        Picture.SelectionStart = Picture.Text.Length + 1;
                    }
                }
            }

            if (programRunning == "Stater Registration")
            {
                if (SNPhasBeenLoaded == true)
                {
                    if (e.KeyValue == (char)Keys.Return)
                    {
                        bool staterFound = false;
                        for (int j = 0; j < pictureBarcodes.Count; j++)
                        {
                            for (int i = 0; i < staters.Count; i++)
                            {
                                if (staters[i].Barcode == pictureBarcodes[j])
                                {
                                    b.Checked = false;
                                    c.Checked = false;
                                    t.Checked = false;
                                    act.Text = "";
                                    listBox1.Items.RemoveAt(listBox1.Items.Count - 1);
                                    listBox1.Items.Add(staters[i].FirstName + " " + staters[i].LastName);
                                    activestaters.Add(new Stater(staters[i].FirstName, staters[i].LastName));
                                    activestaters[activestaters.Count - 1].Barcode = staters[i].Barcode;
                                    activestaters[activestaters.Count - 1].City = staters[i].City;
                                    activestaters[activestaters.Count - 1].County = staters[i].County;
                                    activestaters[activestaters.Count - 1].Act = staters[i].Act;
                                    listBox1.Items.Add(" ");
                                    staterFound = true;
                                    label1.Text = "Stater Found";
                                    if (checkForDuplicateUserEntry())
                                    {
                                        Picture.Clear();
                                        pictureBarcodes.RemoveAt(pictureBarcodes.Count() - 1);
                                        foreach (int Pic in pictureBarcodes)
                                        {
                                            Picture.Text += Pic.ToString() + "\n";
                                        }
                                        label1.Text = "Barcode has already been entered.";
                                    }
                                    break;

                                }
                                else
                                {
                                    staterFound = false;
                                }
                            }
                        }
                        e.Handled = true;
                        if (!staterFound)
                        {
                            Picture.Clear();
                            pictureBarcodes.RemoveAt(pictureBarcodes.Count() - 1);
                            foreach (int Pic in pictureBarcodes)
                            {
                                Picture.Text += Pic.ToString() + "\n";
                            }
                            label1.Text = "Barcode Does Not Exist.";
                        }
                        if (listBox1.Items.Count > 1)
                        {
                            listBox1.SelectedIndex = listBox1.Items.Count - 2;
                        }
                        else
                        {
                            listBox1.SelectedIndex = listBox1.Items.Count - 1;
                        }
                        Picture.Focus();
                        Picture.SelectionStart = Picture.Text.Length + 1;
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


        }

        //Clears the current lists
        private void clearCurrentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Picture.Text = "";
            listBox1.Items.Clear();
            Picture.Focus();
        }

        //Method to Load SNP for City Photos
        private void createSNPFromcsvToolStripMenuItem_Click(object sender, EventArgs e)
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
                    label1.Location = new Point(50, 20);
                    label1.Text = pathstring + " invalid format;\nCorrect Format: First Name, Last Name, Barcode";
                    error = true;

                }
                staters.Add(new Stater(row[1], row[2]));
                char[] bar = row[0].ToCharArray();
                string bs = Convert.ToString(bar[0]) + Convert.ToString(bar[1]) + Convert.ToString(bar[2]) + Convert.ToString(bar[3]);
                staters[staters.Count - 1].Barcode = Convert.ToInt32(bs);
                staters[staters.Count - 1].City = row[3];
                if (staters.Count()>1 && staters[staters.Count - 1].Barcode == staters[staters.Count - 2].Barcode)
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
                label1.Location = new Point(76, 17);
                label1.Text = "Stater list successfully loaded.";

                Picture.Enabled = true;
                button1.Enabled = true;
                Picture.Focus();
            }
            else
            {
                label1.Size = new Size(130, 30);
                label1.Location = new Point(76, 17);
                label1.Text = "SNP Contains an Error";
            }


        }

        //Method to load SNP for Stater Photos
        private void loadSNPForStaterPhotosToolStripMenuItem_Click(object sender, EventArgs e)
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
                label1.Location = new Point(76, 17);
                label1.Text = "Stater list successfully loaded.";
                Picture.Enabled = true;
                button1.Enabled = true;
                Picture.Focus();
            }
        }

        //Method to load SNP for Activities Registration
        private void loadSNPForActivitiesRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
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
            }

            csv.Close();

            SNPhasBeenLoaded = true;

            if (error == false)
            {
                label1.Size = new Size(130, 30);
                label1.Location = new Point(76, 17);
                label1.Text = "Stater list successfully loaded.";

                Picture.Enabled = true;
                button1.Enabled = true;
                Picture.Focus();
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
            this.Controls.Add(b);
            this.Controls.Add(c);
            this.Controls.Add(t);
            this.Controls.Add(band);
            this.Controls.Add(chorus);
            this.Controls.Add(talent);
            this.Controls.Add(activities);
            this.Controls.Add(act);
        }

        //Event Handler for talent checkbox
        public void talentBox(object sender, EventArgs e)
        {
            if (Picture.Text != "")
            {

                if (t.Checked == true)
                {
                    activestaters[listBox1.SelectedIndex].Talent = true;
                    try
                    {
                        talentShowParticipants.Remove(activestaters[talentShowParticipants.FindIndex(x => x.FirstName == activestaters[listBox1.SelectedIndex].FirstName && x.LastName == activestaters[listBox1.SelectedIndex].LastName)]);
                    }
                    catch
                    {
                    }
                    activestaters[listBox1.SelectedIndex].Act = act.Text;
                    talentShowParticipants.Add(activestaters[listBox1.SelectedIndex]);
                    talentShowParticipants[talentShowParticipants.FindIndex(x => x.FirstName == activestaters[listBox1.SelectedIndex].FirstName && x.LastName == activestaters[listBox1.SelectedIndex].LastName)].Act = act.Text;
                    TextWriter tw = new StreamWriter("FailSafeRegistrationTalent.txt");

                    for (int i = 0; i < talentShowParticipants.Count; i++)
                    {
                        tw.WriteLine("Stater Pin" + "," + "First Name" + "," + "Last Name" + "," + "City" + "," + "County" + "," + "Act");
                        tw.WriteLine(talentShowParticipants[i].Barcode + "," + talentShowParticipants[i].FirstName + "," + talentShowParticipants[i].LastName + "," + talentShowParticipants[i].City + "," + talentShowParticipants[i].County + "," + talentShowParticipants[i].Act);
                    }
                    tw.Close();

                }



                if (t.Checked == false)
                {
                    activestaters[listBox1.Items.Count - 2].Talent = false;
                    if (talentShowParticipants.Count > 1)
                    {
                        talentShowParticipants.RemoveAt(talentShowParticipants.Count - 2);
                    }
                    else
                    {
                        talentShowParticipants.RemoveAt(talentShowParticipants.Count - 1);
                    }

                }
                Picture.Focus();
            }
            else
            {
                t.Checked = false;
                Picture.Focus();
            }
        }

        //Event Handler for band checkbox
        public void bandBox(object sender, EventArgs e)
        {
            if (Picture.Text != "")
            {
                if (b.Checked == true)
                {
                    c.Checked = false;
                    chorusMembers.Remove(activestaters[listBox1.Items.Count - 2]);
                    activestaters[listBox1.Items.Count - 2].Chorus = false;
                    activestaters[listBox1.Items.Count - 2].Band = true;
                    bandMembers.Add(activestaters[listBox1.Items.Count - 2]);
                    TextWriter tw = new StreamWriter("FailSafeRegistrationTalent.txt");

                    for (int i = 0; i < bandMembers.Count; i++)
                    {
                        tw.WriteLine("Stater Pin" + "," + "First Name" + "," + "Last Name" + "," + "City" + "," + "County");
                        tw.WriteLine(bandMembers[i].Barcode + "," + bandMembers[i].FirstName + "," + bandMembers[i].LastName + "," + bandMembers[i].City + "," + bandMembers[i].County);
                    }
                    tw.Close();
                }

                if (b.Checked == false)
                {
                    activestaters[listBox1.Items.Count - 2].Band = false;
                    if (bandMembers.Count > 1)
                    {
                        bandMembers.RemoveAt(bandMembers.Count - 2);
                    }
                    else
                    {
                        bandMembers.RemoveAt(bandMembers.Count - 1);
                    }
                }
                Picture.Focus();
            }
            else
            {
                b.Checked = false;
                Picture.Focus();
            }
        }

        //Event Handler for chorus checkbox
        public void chorusBox(object sender, EventArgs e)
        {
            if (Picture.Text != "")
            {

                if (c.Checked == true)
                {
                    b.Checked = false;
                    bandMembers.Remove(activestaters[listBox1.Items.Count - 2]);
                    activestaters[listBox1.Items.Count - 2].Band = false;
                    activestaters[listBox1.Items.Count - 2].Chorus = true;
                    chorusMembers.Add(activestaters[listBox1.Items.Count - 2]);
                    TextWriter tw = new StreamWriter("FailSafeRegistrationTalent.txt");

                    for (int i = 0; i < chorusMembers.Count; i++)
                    {
                        tw.WriteLine("Stater Pin" + "," + "First Name" + "," + "Last Name" + "," + "City" + "," + "County");
                        tw.WriteLine(chorusMembers[i].Barcode + "," + chorusMembers[i].FirstName + "," + chorusMembers[i].LastName + "," + chorusMembers[i].City + "," + chorusMembers[i].County);
                    }
                    tw.Close();
                }

                if (c.Checked == false)
                {
                    activestaters[listBox1.Items.Count - 2].Chorus = false;
                    if (chorusMembers.Count > 1)
                    {
                        chorusMembers.RemoveAt(chorusMembers.Count - 2);
                    }
                    else
                    {
                        chorusMembers.RemoveAt(chorusMembers.Count - 1);
                    }

                }
                Picture.Focus();
            }
            else
            {
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
            List<int> Pins= new List<int>();
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
                    Errors.Add("Duplicate pin: "+Pin);
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
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}