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
using BLL.DBOperations;
using DAL;
using BLL;
using Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Interop.Excel;

namespace RIAB_Restaurent_Management_System.Views.finance
{
    /// <summary>
    /// Interaction logic for Form_CashInHand.xaml
    /// </summary>
    public partial class Form_CashInHand : System.Windows.Window
    {
        public Form_CashInHand()
        {
            InitializeComponent();
            initFormOperations();
            
        }
        void initFormOperations()
        {
            foreach (tbl_FinanceChart item in FinanceChart.getAll())
            {
                dg_FinanceChart.Items.Add(item);
            }
            lbl_TotalSale.Content =Sale.getAllTotalAmmount();
            lbl_TotalDeposit.Content = FinanceChart.getTotalDeposit();
            lbl_TotalExpence.Content = Expence.getAllTotalAmmount();
        }
        private void btn_ExportExcel(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application ex = new Microsoft.Office.Interop.Excel.Application();
                ex.Visible = true;
                Workbook workbook = ex.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

                for (int j = 0; j < dg_FinanceChart.Columns.Count; j++) //Başlıklar için
                {
                    Range myRange = (Range)sheet1.Cells[1, j + 1];
                    sheet1.Cells[1, j + 1].Font.Bold = true; //Başlığın Kalın olması için
                    sheet1.Columns[j + 1].ColumnWidth = 15; //Sütun genişliği ayarı
                    myRange.Value2 = dg_FinanceChart.Columns[j].Header;
                }
                for (int i = 0; i < dg_FinanceChart.Columns.Count; i++)
                {
                    for (int j = 0; j < dg_FinanceChart.Items.Count; j++)
                    {
                        TextBlock b = dg_FinanceChart.Columns[i].GetCellContent(dg_FinanceChart.Items[j]) as TextBlock;
                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                        myRange.Value2 = b.Text;
                    }
                }
            }
            catch
            {
                AutoClosingMessageBox.Show("some error occured","Failed",0);
            }
            
        }

        private void btn_CustomDateSearch(object sender, RoutedEventArgs e)
        {
            if (dp_from.SelectedDate != null && dp_to.SelectedDate != null)
            {
                DateTime tempFromDate = Convert.ToDateTime(dp_from.SelectedDate);
                DateTime tempToDate = Convert.ToDateTime(dp_to.SelectedDate);

                DateTime fromDate = Convert.ToDateTime(tempFromDate.ToShortDateString() + " 12:00:00 AM");
                DateTime toDate = Convert.ToDateTime(tempToDate.ToShortDateString() + " 11:59:59 PM");
                dg_FinanceChart.Items.Clear();
                foreach (tbl_FinanceChart item in FinanceChart.getAllCustomDate(fromDate,toDate))
                {
                    dg_FinanceChart.Items.Add(item);
                }
            }
        }
    }
}
