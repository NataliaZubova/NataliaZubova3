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

namespace TDC_Filial
{
    public partial class Form4 : Form
    {
        public Collection<Record> records = new Collection<Record>();
        StreamReader reader;
        StreamReader reader2;
        StreamReader reader3;
        StreamWriter writer;
        public Form4()
        {
            
            InitializeComponent();
            Process.Start("zagr.bat");
            LoadUsers();
            
        }
        private void LoadUsers()
        {   
            Record record;
            reader = new StreamReader("Users1.txt");
            while (!reader.EndOfStream)
            {
                record = new Record();
                record.Name = reader.ReadLine();
                record.Pas = reader.ReadLine();
                reader.ReadLine();
                records.Add(record);
            }
            reader.Close();

            for (int i = 0; i < records.Count; i++)
            {
                comboBox1.Items.Add(records[i].Name);
            }
        }
        public class Record
        {
            public String Pas,Name;
        };
        

        private void button1_Click(object sender, EventArgs e)
        {
            Int32 userid = 0;
            for (int i = 0; i < records.Count; i++)
                if (records[i].Name == records[comboBox1.SelectedIndex].Name && records[i].Pas == textBox2.Text)
                {
                    Form1 frmAbout = new Form1();
                    userid = i + 1;
                    writer = new StreamWriter("User1.txt");
                    writer.WriteLine(records[i].Name);
                    writer.Close();
                    this.Hide();
                    frmAbout.ShowDialog();
                    Close();
                }
            if (userid == 0)
            {
                
                MessageBox.Show("Не найдено соответствие пары логин/пароль. В доступе отказано.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
           
            textBox2.UseSystemPasswordChar = true;
            textBox1.Text = DateTime.Now.ToString("dd MMMM yyyy  HH:mm:ss");
            reader2 = new StreamReader("Izmen1.txt");
            while (!reader2.EndOfStream)
            {
                textBox3.Text = reader2.ReadLine();
            }
            reader2.Close();
            reader3 = new StreamReader("Set.txt", Encoding.Default);
            while (!reader3.EndOfStream)
            {
                reader3.ReadLine();
                reader3.ReadLine();
                reader3.ReadLine();
                Int32 s13 = Convert.ToInt32(reader3.ReadLine());
                reader3.ReadLine();
                reader3.ReadLine();
                reader3.ReadLine();
                reader3.ReadLine();
                reader3.ReadLine();
                reader3.ReadLine();
                reader3.ReadLine();
                reader3.ReadLine();
                if (s13==1)
                textBox4.Text = "каждый день";
                if (s13 == 2 | s13 == 3 | s13 == 4)
                textBox4.Text = "каждые " + s13 + " дня";
                if (s13 != 1 && s13 != 2 && s13 != 3 && s13 != 4)
                    textBox4.Text = "каждые " + s13 + " дней";
            }
            reader3.Close();
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Int32 userid = 0;
                for (int i = 0; i < records.Count; i++)
                    if (records[i].Name == records[comboBox1.SelectedIndex].Name && records[i].Pas == textBox2.Text)
                    {
                        Form1 frmAbout = new Form1();
                        userid = i + 1;
                        writer = new StreamWriter("User1.txt");
                        writer.WriteLine(records[i].Name);
                        writer.Close();
                        this.Hide();
                        frmAbout.ShowDialog();
                        Close();
                    }
                if (userid == 0)
                    MessageBox.Show("Не найдено соответствие пары логин/пароль. В доступе отказано.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Int32 kolstart = records.Count;
            LoadUsers();
            comboBox1.Items.Clear();
             for (int i = kolstart; i < records.Count; i++)
            {
                comboBox1.Items.Add(records[i].Name);
            }
            
        }

       

      
    }
}
