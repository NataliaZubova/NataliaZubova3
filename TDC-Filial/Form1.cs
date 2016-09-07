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
    public partial class Form1 : Form
    {
        public Collection<Record> records = new Collection<Record>();
        StreamReader reader;
        StreamReader reader2;
        StreamReader reader3;
        StreamWriter writer;
        StreamWriter writer3;
       
        public Form1()
        {
            Process.Start("zagr.bat");
            InitializeComponent();
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadFromFile();
            FillListBox();
            ReadStandart();
            listBox1.SelectedIndex = 0; 
        }
        private void ReadStandart()
        { 
            reader3 = new StreamReader("Standart.txt");
            while (!reader3.EndOfStream)
            {
                string par1 = reader3.ReadLine();
                string par2 = reader3.ReadLine();
                string par3 = reader3.ReadLine();
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
            reader3.Close();
        }
        private void SaveToFile()
        {
            writer = new StreamWriter("Data1.txt");
            for (int i = 0; i < records.Count; i++)
            {
                writer.WriteLine(records[i].Name);
                writer.WriteLine(records[i].Sht);
                writer.WriteLine(records[i].Col);
                writer.WriteLine(records[i].Min);
                writer.WriteLine(records[i].Norm);
                writer.WriteLine("$");
            }
            writer.Close();
        }

        private void LoadFromFile()
        {
            Record record;
            reader = new StreamReader("Nomen.txt");
            reader2 = new StreamReader("Data1.txt");
            while (!reader.EndOfStream)
            {
                record = new Record();
                record.Name = reader.ReadLine();
                record.Sht = reader.ReadLine();
                reader.ReadLine();
                record.Min = reader.ReadLine();
                record.Norm = reader.ReadLine();
                reader.ReadLine();
                reader2.ReadLine();
                reader2.ReadLine();
                record.Col = reader2.ReadLine();
                reader2.ReadLine();
                reader2.ReadLine();
                reader2.ReadLine();
                records.Add(record);
            }
            reader.Close();
            reader2.Close();
        }
        public class Record
        {
            public String Name, Sht, Col, Min, Norm;
        };
        private void Form1_Shown(object sender, EventArgs e)
        {
            FillListBox();
        }
        private void FillListBox()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < records.Count; i++)
            {
                listBox1.Items.Add(records[i].Name + "  [" + records[i].Sht + "]");
            }
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) return;
            if (listBox1.SelectedIndex < records.Count)
            {
              textBox1.Focus();
              textBox1.SelectionStart = 0;
              textBox1.SelectionLength = textBox1.Text.Length;
                  if (records[listBox1.SelectedIndex].Col != "0")
              textBox1.Text = records[listBox1.SelectedIndex].Col;
                  else
                  {
                      textBox1.Text = "";
                  }
            }
            else
            {
                textBox1.Text = "";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
                Record record = new Record();
                records[listBox1.SelectedIndex].Col = textBox1.Text;
                for (int i = 0; i < records.Count; i++)
                    if (records[i].Col == "")
                        records[i].Col = "0";


                if (listBox1.SelectedIndex < listBox1.Items.Count - 1)
                    listBox1.SelectedIndex++;
                if (checkBox1.Checked == true)
                    textBox2.Focus();
                textBox1.SelectionStart = 0;
                textBox1.SelectionLength = textBox1.Text.Length;
                if (checkBox2.Checked == true)
                    SaveToFile();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                writer3 = new StreamWriter("Sohran.txt");
                writer3.WriteLine("1");
                writer3.Close();
            }
            if (checkBox3.Checked == false)
            {
                writer3 = new StreamWriter("Sohran.txt");
                writer3.WriteLine("0");
                writer3.Close();
            }
            SaveToFile();
            Form2 frmAbout = new Form2();
            frmAbout.ShowDialog();
        }

        private void выходБезСохраненияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void выгрузитьДанныеНаСерверToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToFile();
            MessageBox.Show("Данные приняты. Выгрузка на сервер завершена.", "Выгрузка на сервер", MessageBoxButtons.OK, MessageBoxIcon.Information);
            writer = new StreamWriter("Izmen1.txt");
            string s = DateTime.Now.ToString("dd MMMM yyyy  HH:mm:ss");
            writer.WriteLine(s);
            writer.Close();
            if (checkBox3.Checked == true)
            {
                Stream fs = new FileStream(@"Data1.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
               Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                excel.SheetsInNewWorkbook = 2;
                excel.Workbooks.Add(Type.Missing);
                Excel.Workbook workbook = excel.Workbooks[1]; 
                Excel.Worksheet sheet = workbook.Worksheets.get_Item(1);
                string da = DateTime.Now.ToString("dd.MM.yyyy  HH:mm:ss");
                sheet.get_Range("A1", "D1").MergeCells = true;
                sheet.Cells[1, 1].Value = "Выгрузка данных от " + da;
                sheet.Cells[3, 1].Value = "№";
                sheet.Cells[3, 2].Value = "Название";
                sheet.Cells[3, 3].Value = "Штрих-Код";
                sheet.Cells[3, 4].Value = "Остаток";//
                int k = 1;
                while (sr.Peek() != -1)
                {
                    string s1 = sr.ReadLine();
                    string s2 = sr.ReadLine();
                    string s3 = sr.ReadLine();
                    string s4 = sr.ReadLine();
                    string s5 = sr.ReadLine();
                    string s6 = sr.ReadLine();
                    sheet.get_Range("C1", "D1000").NumberFormat = "0";
                    sheet.Columns.AutoFit();
                    var cells = sheet.get_Range("B2", "F5");
                    sheet.Cells[k + 3, 1].Value = k;
                    sheet.Cells[k + 3, 2].Value = "    " + s1 + "    ";
                    sheet.Cells[k + 3, 3].Value = s2;
                    sheet.Cells[k + 3, 4].Value = s3;
                    k++;
                }
                sr.Close();
                sheet.Cells.get_Range("A3", "D" + (k + 2)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                string dat = DateTime.Now.ToString("dd.MM.yyyy  HH.mm");
                workbook.SaveAs(@"C:\TDC\Выгрузки\Выгрузка от " + dat + ".xlsx");
                
            }Process.Start("vigr.bat");
        }

        private void выгрузитьДанныеИВыйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToFile();
            MessageBox.Show("Данные приняты. Выгрузка на сервер завершена.", "Выгрузка на сервер", MessageBoxButtons.OK, MessageBoxIcon.Information);
            writer = new StreamWriter("Izmen1.txt");
            string s = DateTime.Now.ToString("dd MMMM yyyy  HH:mm:ss");
            writer.WriteLine(s);
            writer.Close();
            Process.Start("vigr.bat");
         Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {String Isk;
        Int32 Find=0;
        Isk = textBox2.Text;
        for (int i = 0; i < records.Count; i++)
            if (records[i].Sht == Isk)
            {
                listBox1.SelectedIndex = i;
                textBox1.Text = records[i].Col;
                Find = 1;
            }
            if (Find==0)
            MessageBox.Show("Товар с таким штрих-кодом не найден", "Ошибка поиска", MessageBoxButtons.OK, MessageBoxIcon.Information);
        textBox2.Clear();
        if (checkBox1.Checked == true)
            textBox1.Focus();
        textBox1.SelectionStart = 0;
        textBox1.SelectionLength = textBox1.Text.Length;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Int32 Find = 0;
                String Isk;

                Isk = textBox2.Text;
                for (int i = 0; i < records.Count; i++)
                    if (records[i].Sht == Isk)
                    {
                        listBox1.SelectedIndex = i;
                        textBox1.Text = records[i].Col;
                        Find = 1;
                    }
                if (Find == 0)
                    MessageBox.Show("Товар с таким штрих-кодом не найден", "Ошибка поиска", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox2.Clear();
                if (checkBox1.Checked == true)
                    textBox1.Focus();
                textBox1.SelectionStart = 0;
                textBox1.SelectionLength = textBox1.Text.Length;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Record record = new Record();
                records[listBox1.SelectedIndex].Col = textBox1.Text;
                for (int i = 0; i < records.Count; i++)
                    if (records[i].Col == "")
                        records[i].Col = "0";


                if (listBox1.SelectedIndex < listBox1.Items.Count - 1)
                    listBox1.SelectedIndex++;
                if (checkBox1.Checked == true)
                    textBox2.Focus();
                textBox1.SelectionStart = 0;
                textBox1.SelectionLength = textBox1.Text.Length;
                if (checkBox2.Checked == true)
                    SaveToFile();
            }
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frmAbout = new Form3();
            frmAbout.ShowDialog();
        }
        private void сохранитьИВыйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveToFile();
        }
        private void очиститьВсёToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 nul = 0;
            writer = new StreamWriter("Data1.txt");
            for (int i = 0; i < records.Count; i++)
            {
                writer.WriteLine(records[i].Name);
                writer.WriteLine(records[i].Sht);
                writer.WriteLine(nul);
                writer.WriteLine(records[i].Min);
                writer.WriteLine(records[i].Norm);
                writer.WriteLine("$");
            }
            writer.Close();
            for (int i = 0; i < records.Count; i++)
            records[i].Col="";
            textBox1.Clear();
                }

        private void открытьПапкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "C:\\TDC\\Выгрузки");

        }

        private void удалитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryInfo dirInfo = new DirectoryInfo("C:\\TDC\\Выгрузки");

            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 frmAbout = new Form6();
            frmAbout.ShowDialog();
        }

        private void справкаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start("info.docx");
            
        }
        }
    }

