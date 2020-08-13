using BLL;
using BLL.DBOperations;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RIAB_Restaurent_Management_System.Views.finance
{
    /// <summary>
    /// Interaction logic for Window_ViewDeposits.xaml
    /// </summary>
    public partial class Window_ViewDeposits : System.Windows.Window
    {
        public Window_ViewDeposits()
        {
            InitializeComponent();
            dg_Deposits.ItemsSource = Deposit.getAll();
        }

        private void btn_ExportExcel(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application ex = new Microsoft.Office.Interop.Excel.Application();
                ex.Visible = true;
                Workbook workbook = ex.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

                for (int j = 0; j < dg_Deposits.Columns.Count; j++) //Başlıklar için
                {
                    Range myRange = (Range)sheet1.Cells[1, j + 1];
                    sheet1.Cells[1, j + 1].Font.Bold = true; //Başlığın Kalın olması için
                    sheet1.Columns[j + 1].ColumnWidth = 15; //Sütun genişliği ayarı
                    myRange.Value2 = dg_Deposits.Columns[j].Header;
                }
                for (int i = 0; i < dg_Deposits.Columns.Count; i++)
                {
                    for (int j = 0; j < dg_Deposits.Items.Count; j++)
                    {
                        TextBlock b = dg_Deposits.Columns[i].GetCellContent(dg_Deposits.Items[j]) as TextBlock;
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                        myRange.Value2 = b.Text;
                    }
                }
            }
            catch
            {
                AutoClosingMessageBox.Show("some error occured", "Failed", 0);
            }
        }
    }
}
