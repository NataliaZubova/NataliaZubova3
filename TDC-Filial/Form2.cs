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
    public partial class Form2 : Form
    {
        StreamReader reader;
        StreamWriter writer;
        public Form2()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form2_Load);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Stream fs = new FileStream(@"Data1.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            DataTable dt = new DataTable();
            dt.Columns.Add("№");
            dt.Columns.Add("Название");
            dt.Columns.Add("Штрих-Код");
            dt.Columns.Add("Остаток");  
            dataGridView1.AutoResizeColumns();
            int count = 1;
            while (sr.Peek() != -1)
            {
                string s1 = sr.ReadLine();
                string s2 = sr.ReadLine();
                string s3 = sr.ReadLine();
                string s4 = sr.ReadLine();
                string s5 = sr.ReadLine();
                string s6 = sr.ReadLine();
                {
                    dt.Rows.Add(count, s1, s2, s3);
                    count++;
                }
            }
            sr.Close();
            dataGridView1.DataSource = dt;
            }
   
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данные приняты. Выгрузка на сервер завершена.", "Выгрузка на сервер", MessageBoxButtons.OK, MessageBoxIcon.Information);
            writer = new StreamWriter("Izmen1.txt");
            string s = DateTime.Now.ToString("dd MMMM yyyy  HH:mm:ss");
            writer.WriteLine(s);
            writer.Close();
            Process.Start("vigr.bat");
            reader = new StreamReader("Sohran.txt");
            string sohr = reader.ReadLine();
            reader.Close();
            if (sohr == "1")
            {
                Stream fs = new FileStream(@"Data1.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                excel.SheetsInNewWorkbook = 2;
                excel.Workbooks.Add(Type.Missing);
                Excel.Workbook workbook = excel.Workbooks[1];
                Excel.Worksheet sheet = workbook.Worksheets.get_Item(1);
                string s62 = DateTime.Now.ToString("dd.MM.yyyy  HH:mm:ss");
                sheet.get_Range("A1", "D1").MergeCells = true;
                sheet.Cells[1, 1].Value = "Выгрузка данных от " + s62;
                sheet.Cells[3, 1].Value = "№";
                sheet.Cells[3, 2].Value = "Название";
                sheet.Cells[3, 3].Value = "Штрих-Код";
                sheet.Cells[3, 4].Value = "Остаток";
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
            }
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void принятьДанныеИВыгрузитьНаСерверToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данные приняты. Выгрузка на сервер завершена.", "Выгрузка на сервер", MessageBoxButtons.OK, MessageBoxIcon.Information);
            writer = new StreamWriter("Izmen1.txt");
            string s = DateTime.Now.ToString("dd MMMM yyyy  HH:mm:ss");
            writer.WriteLine(s);
            writer.Close();
            Process.Start("vigr.bat");
            reader = new StreamReader("Sohran.txt");
            string sohr=reader.ReadLine();
            reader.Close();
            if (sohr=="1")
            {
            Stream fs = new FileStream(@"Data1.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
             Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            excel.SheetsInNewWorkbook = 2;
            excel.Workbooks.Add(Type.Missing);
            Excel.Workbook workbook = excel.Workbooks[1]; 
            Excel.Worksheet sheet = workbook.Worksheets.get_Item(1);
            string s61 = DateTime.Now.ToString("dd.MM.yyyy  HH:mm:ss");
            sheet.get_Range("A1", "D1").MergeCells = true;
            sheet.Cells[1, 1].Value="Выгрузка данных от " + s61;
            sheet.Cells[3, 1].Value = "№";
            sheet.Cells[3, 2].Value = "Название";
            sheet.Cells[3, 3].Value = "Штрих-Код";
            sheet.Cells[3, 4].Value = "Остаток";
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
                sheet.Cells[k+3, 1].Value =k;
                sheet.Cells[k+3, 2].Value = "    " + s1 + "    ";
                sheet.Cells[k+3, 3].Value = s2;
                sheet.Cells[k+3, 4].Value = s3;
                k++;
            }
            sr.Close();
            sheet.Cells.get_Range("A3", "D"+(k+2)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            string dat = DateTime.Now.ToString("dd.MM.yyyy  HH.mm");
            workbook.SaveAs(@"C:\TDC\Выгрузки\Выгрузка от " + dat + ".xlsx");
            }
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void выходБезСохраненияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            Close();
        }

        private void выгрузкаВЭксельToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream fs = new FileStream(@"Data1.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            excel.SheetsInNewWorkbook = 2;
            excel.Workbooks.Add(Type.Missing);
            Excel.Workbook workbook = excel.Workbooks[1]; 
            Excel.Worksheet sheet = workbook.Worksheets.get_Item(1);
            string s = DateTime.Now.ToString("dd.MM.yyyy  HH:mm:ss");
            sheet.get_Range("A1", "D1").MergeCells = true;
            sheet.Cells[1, 1].Value="Выгрузка данных от " + s;
            sheet.Cells[3, 1].Value = "№";
            sheet.Cells[3, 2].Value = "Название";
            sheet.Cells[3, 3].Value = "Штрих-Код";
            sheet.Cells[3, 4].Value = "Остаток";
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
                sheet.Cells[k+3, 1].Value =k;
                sheet.Cells[k+3, 2].Value = "    " + s1 + "    ";
                sheet.Cells[k+3, 3].Value = s2;
                sheet.Cells[k+3, 4].Value = s3;
                k++;
            }
            sr.Close();
            sheet.Cells.get_Range("A3", "D"+(k+2)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            string dat = DateTime.Now.ToString("dd.MM.yyyy  HH.mm");
            workbook.SaveAs(@"C:\TDC\Выгрузки\Выгрузка от " + dat + ".xlsx");
        }
           
            }
        }
    

