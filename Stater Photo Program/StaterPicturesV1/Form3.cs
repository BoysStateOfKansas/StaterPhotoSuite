using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StaterOrganizer
{
    public partial class Form3 : Form
    {
        public int pin { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String county { get; set; }
        public String city { get; set; }
        public Form3()
        {
            InitializeComponent();
            fillCountySelection();
            comboBox2.Enabled = false;
            error.Text = "";
        }
        private void fillCountySelection()
        {
            List<String> Counties = new List<string> { "Bradley", "Eisenhower", "Kennedy", "King", "MacArthur", "Marshall", "Patton", "Pershing", "Powell", "Seitz" };
            foreach (String county in Counties)
            {
                comboBox1.Items.Add(county);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = true;
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Bradley":
                    comboBox2.Items.Add("Aylward");
                    comboBox2.Items.Add("Bramlage");
                    break;
                case "Eisenhower":
                    comboBox2.Items.Add("Chastain");
                    comboBox2.Items.Add("Crum");
                    break;
                case "Kennedy":
                    comboBox2.Items.Add("Finley");
                    comboBox2.Items.Add("Haney");
                    break;
                case "King":
                    comboBox2.Items.Add("Harris");
                    comboBox2.Items.Add("Hiatt");
                    break;
                case "MacArthur":
                    comboBox2.Items.Add("Icenogle");
                    comboBox2.Items.Add("Johnson");
                    break;
                case "Marshall":
                    comboBox2.Items.Add("Klassen");
                    comboBox2.Items.Add("Lane");
                    break;
                case "Patton":
                    comboBox2.Items.Add("Mantey");
                    comboBox2.Items.Add("Newman");
                    break;
                case "Pershing":
                    comboBox2.Items.Add("Perrill");
                    comboBox2.Items.Add("Schmitt");
                    break;
                case "Powell":
                    comboBox2.Items.Add("Schulz");
                    comboBox2.Items.Add("Spigarelli");
                    break;
                case "Seitz":
                    comboBox2.Items.Add("Sullivan");
                    comboBox2.Items.Add("Wiles");
                    break;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool addStater = verifyPin();
            if(addStater) addStater = verifyFirstName();
            if (addStater) addStater = verifyLastName();
            if (addStater) addStater = verifyCounty();
            if (addStater) addStater = verifyCity();
            if (addStater)
            {
                pin = Convert.ToInt32(Pin.Text);
                firstName = fName.Text;
                lastName = lName.Text;
                county = comboBox1.SelectedItem.ToString();
                city = comboBox2.SelectedItem.ToString();
                this.DialogResult = DialogResult.OK;
            }

        }
        private bool verifyPin()
        {
            if (Pin.Text == "")
            {
                error.Text = "Pin cannot be empty.";
                return false;
            }
            else
            {
                return true;
            }

        }
        private bool verifyFirstName()
        {
            if (fName.Text == "")
            {
                error.Text = "First name cannot be empty.";
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool verifyLastName()
        {
            if (fName.Text == "")
            {
                error.Text = "Last name cannot be empty.";
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool verifyCounty()
        {
            if (comboBox1.SelectedItem == null)
            {
                error.Text = "Please select a county.";
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool verifyCity()
        {
            if (comboBox2.SelectedItem == null)
            {
                error.Text = "Please select a city.";
                return false;
            }
            else
            {
                return true;
            }
        }
        private void Cancel(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Dispose();
        }

        

        
    }
}
