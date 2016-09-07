using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;

namespace TDC_Filial
{
    public partial class Form6 : Form
    {
        StreamReader reader;
        StreamWriter writer;
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WriteStandart();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            ReadStandart();
        }
        private void ReadStandart()
        {
            reader = new StreamReader("Standart.txt");
            while (!reader.EndOfStream)
            {
                string par1 = reader.ReadLine();
                string par2 = reader.ReadLine();
                string par3 = reader.ReadLine();
                if (par1 == "1")
                    checkBox1.Checked = true;
                if (par1 == "0")
                    checkBox1.Checked = false;
                if (par2 == "1")
                    checkBox2.Checked = true;
                if (par2 == "0")
                    checkBox2.Checked = false;
                if (par3 == "1")
                    checkBox3.Checked = true;
                if (par3 == "0")
                    checkBox3.Checked = false;
            }
            reader.Close();
        }
        private void WriteStandart()
        {
        writer = new StreamWriter("Standart.txt");
            if (checkBox1.Checked == true) 
                writer.WriteLine("1");
            if (checkBox1.Checked == false) 
                writer.WriteLine("0");
            if (checkBox2.Checked == true) 
                writer.WriteLine("1");
            if (checkBox2.Checked == false) 
                writer.WriteLine("0");
            if (checkBox3.Checked == true) 
                writer.WriteLine("1");
            if (checkBox3.Checked == false) 
                writer.WriteLine("0");
            writer.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    
    }
}
