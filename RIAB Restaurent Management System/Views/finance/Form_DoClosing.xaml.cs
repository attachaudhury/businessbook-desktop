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
using BLL.DBOperations;
using DAL;

namespace RIAB_Restaurent_Management_System.Views.finance
{
    /// <summary>
    /// Interaction logic for Form_DoClosing.xaml
    /// </summary>
    public partial class Form_DoClosing : Window
    {
        public Form_DoClosing()
        {
            InitializeComponent();
            initFormOperations();
        }

        private void btn_Save(object sender, RoutedEventArgs e)
        {
            if (tb_OpeningBalance.Text == "" || tb_Sale.Text == "" || tb_Delivery.Text == "" || tb_TotalSale.Text == "" || tb_TotalCash.Text == "" || tb_Deposit.Text == "" || tb_Expence.Text == "" || tb_ClosingBalance.Text == "")
            {
                AutoClosingMessageBox.Show("Please Fill all fields", "Alert", 2000);
            }
            else
            {
                try
                {
                    tbl_FinanceChart fc = new tbl_FinanceChart();
                    fc.Date = DateTime.Now;
                    fc.OpeningBalance = Convert.ToInt32(tb_OpeningBalance.Text);
                    fc.Sale = Convert.ToInt32(tb_Sale.Text);
                    fc.Delivery = Convert.ToInt32(tb_Delivery.Text);
                    fc.TotalSale = Convert.ToInt32(tb_TotalSale.Text);
                    fc.TotalCash = Convert.ToInt32(tb_TotalCash.Text);
                    fc.Deposit = Convert.ToInt32(tb_Deposit.Text);
                    fc.Expence = Convert.ToInt32(tb_Expence.Text);
                    fc.ClosingBalance = Convert.ToInt32(tb_ClosingBalance.Text);
                    FinanceChart.insert(fc);
                    Close();
                    AutoClosingMessageBox.Show("Closings saved", "Success", 2000);
                }
                catch
                {
                    AutoClosingMessageBox.Show("Please try again", "Fail", 2000);
                }
            }
        }
        void initFormOperations()
        {
            try
            {
                tb_OpeningBalance.Text = Convert.ToString(FinanceChart.getLastDayClosingBalance());
                dp_Date.Content = DateTime.Now.ToShortDateString();
                tb_Sale.Text = Convert.ToString(Sale.getAllTodayOnRestaurentAmmount());
                tb_Delivery.Text = Convert.ToString(Sale.getAllTodayOnDeliveryAmmount());
                tb_TotalSale.Text = Convert.ToString(Sale.getAllTodayOnRestaurentAmmount() + Sale.getAllTodayOnDeliveryAmmount());
                tb_TotalCash.Text = Convert.ToString(FinanceChart.getLastDayClosingBalance() + Sale.getAllTodayOnRestaurentAmmount() + Sale.getAllTodayOnDeliveryAmmount());
                tb_Expence.Text = Convert.ToString(Expence.getTodayTotalAmmount());
                tb_Deposit.Text = Convert.ToString(Deposit.getAllTodayAmmount());
                int deposit = Convert.ToInt32(tb_Deposit.Text);
                int ClosingBalance = FinanceChart.getLastDayClosingBalance() + Sale.getAllTodayOnRestaurentAmmount() + Sale.getAllTodayOnDeliveryAmmount() - Expence.getTodayTotalAmmount() - deposit;
                tb_ClosingBalance.Text = Convert.ToString(ClosingBalance);

                
            } catch{}
            
        }
    }
}
