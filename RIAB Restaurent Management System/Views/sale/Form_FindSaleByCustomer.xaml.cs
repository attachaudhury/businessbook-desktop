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
    public partial class Form_FindSaleByCustomer : Window
    {
        public Form_FindSaleByCustomer()
        {
            InitializeComponent();
        }

        private void btn_FindSales(object sender, RoutedEventArgs e)
        {
            if (tb_Phone.Text != null)
            {
                tbl_Customer customer = Customer.getByPhoneNumber(tb_Phone.Text);
                if (customer != null)
                {
                    dg_AllCustomerSales.IsEnabled = true;
                    dg_AllCustomerSales.ItemsSource = Sale.getAllByCustomerID(customer.Id);
                }
                else 
                {
                    AutoClosingMessageBox.Show("No Customer Found", "Alert", 3000);
                }
            }
            else
            {
                AutoClosingMessageBox.Show("Please Enter A value", "Alert", 3000);
            }
        }
    }
}
