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
using BLL;
using DAL;
using BLL.DBOperations;
using Microsoft.Office.Interop.Excel;

namespace RIAB_Restaurent_Management_System.Views.sale
{
    public partial class Form_AllSales : System.Windows.Window
    {
        public Form_AllSales()
        {
            InitializeComponent();
            initFormOperations();
        }
        void initFormOperations()
        {
            dg_AllSales.ItemsSource = Sale.getAll();
            lbl_Ammount.Content = Sale.getAllTotalAmmount();
        }

        private void btn_SaleDetails(object sender, RoutedEventArgs e)
        {
            if(dg_AllSales.SelectedItem != null)
            {
               tbl_Sale sale = (tbl_Sale)dg_AllSales.SelectedItem;
               new Form_SaleDetails(sale.Id).Show();
            }
        }

        private void btn_CustomDateSearch(object sender, RoutedEventArgs e)
        {

            DateTime tempFromDate = (DateTime)dp_from.SelectedDate;
            DateTime tempToDate = (DateTime)dp_to.SelectedDate;

            DateTime fromDate = Convert.ToDateTime(tempFromDate.ToShortDateString() + " 12:00:00 AM");
            DateTime toDate = Convert.ToDateTime(tempToDate.ToShortDateString() + " 11:59:59 PM");
            dg_AllSales.ItemsSource = null;
            dg_AllSales.ItemsSource = Sale.getAllCustomDate(fromDate,toDate);
            lbl_Ammount.Content = Sale.getAllCustomDateAmmount(fromDate, toDate);
        }

        private void btn_ExportExcel(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ex = new Microsoft.Office.Interop.Excel.Application();
            ex.Visible = true;
            Workbook workbook = ex.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < dg_AllSales.Columns.Count; j++) //Başlıklar için
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true; //Başlığın Kalın olması için
                sheet1.Columns[j + 1].ColumnWidth = 15; //Sütun genişliği ayarı
                myRange.Value2 = dg_AllSales.Columns[j].Header;
            }
            for (int i = 0; i < dg_AllSales.Columns.Count; i++)
            {
                for (int j = 0; j < dg_AllSales.Items.Count; j++)
                {
                    TextBlock b = dg_AllSales.Columns[i].GetCellContent(dg_AllSales.Items[j]) as TextBlock;
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;
                }
            }
        }
    }
}
