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
    public partial class Form2 : Form
    {
        public Form2(List<String> errors)
        {
            InitializeComponent();
            for (int i = 0; i < errors.Count(); i++)
            {
                listBox1.Items.Add(errors[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
