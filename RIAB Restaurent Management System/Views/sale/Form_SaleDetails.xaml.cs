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
using DAL;
using BLL;
using BLL.DBOperations;

namespace RIAB_Restaurent_Management_System.Views.sale
{
    /// <summary>
    /// Interaction logic for Form_SaleDetails.xaml
    /// </summary>
    public partial class Form_SaleDetails : Window
    {
        int saleID;
        public Form_SaleDetails(int saleId)
        {
            saleID = saleId;
            InitializeComponent();
            initFormOperations(saleId);
        }
        void initFormOperations(int saleId)
        {
            tbl_Sale sale = Sale.getById(saleId);
            dg_ItemPurchased.ItemsSource = SaleItem.getAllBySaleId(saleId);
            tbl_Customer customer = Customer.getById((int)sale.Customer_Id);
            if (customer != null) 
            {
                lbl_CustomerName.Content = customer.Name;
            }
            lbl_SaleId.Content = saleId;
            lbl_Date.Content = ((DateTime)sale.Date_Time).ToShortDateString();
            lbl_Total.Content = sale.Amount;

        }

        private void printDuplicateRecipt(object sender, RoutedEventArgs e)
        {
            Sale.printDuplicateRecipt(saleID);
        }
    }
}
