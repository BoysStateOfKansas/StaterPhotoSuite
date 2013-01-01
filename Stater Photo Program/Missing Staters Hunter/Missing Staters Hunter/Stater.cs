using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class Stater
    {
        private String firstname;
        private String lastname;
        private int barcode;
        private String city;
        private String county;
        private bool band;
        private bool chorus;
        private bool talent;
        private string act;
        private int occurances;

        public Stater(String f, String l)
        {
            firstname = f;
            lastname = l;
            barcode = 0;
            city = "";
            county = "";
            band = false;
            chorus = false;
            talent = false;
            act = "";
            occurances = 0;
        }

        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }

        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }

        public int Barcode
        {
            get { return barcode; }
            set { barcode = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string County
        {
            get { return county; }
            set { county = value; }
        }

        public bool Band
        {
            get { return band; }
            set { band = value; }
        }

        public bool Chorus
        {
            get { return chorus; }
            set { chorus = value; }
        }

        public bool Talent
        {
            get { return talent; }
            set { talent = value; }
        }

        public String Act
        {
            get { return act; }
            set { act = value; }
        }

        public int Occurances
        {
            get { return occurances; }
            set { occurances = value; }
        }
    }
}
