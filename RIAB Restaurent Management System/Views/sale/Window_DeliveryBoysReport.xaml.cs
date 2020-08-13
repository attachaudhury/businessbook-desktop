using BLL.DBOperations;
using DAL;
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

namespace RIAB_Restaurent_Management_System.Views.sale
{
    /// <summary>
    /// Interaction logic for Window_DeliveryBoysReport.xaml
    /// </summary>
    public partial class Window_DeliveryBoysReport : Window
    {
        public Window_DeliveryBoysReport()
        {
            InitializeComponent();
            foreach (tbl_Staff item in Staff.getAllDeliveryStaff())
            {
                cb_Name.ItemsSource = Staff.getAllDeliveryStaff();
                cb_Name.DisplayMemberPath = "Name";
                cb_Name.SelectedValuePath = "Id";
            }
        }

        private void btn_Search(object sender, RoutedEventArgs e)
        {
            if (cb_Name.SelectedValue != null)
            {
                if (dp_from.SelectedDate != null && dp_to.SelectedDate != null)
                {
                    DateTime tempFromDate = Convert.ToDateTime(dp_from.SelectedDate);
                    DateTime tempToDate = Convert.ToDateTime(dp_to.SelectedDate);

                    DateTime fromDate = Convert.ToDateTime(tempFromDate.ToShortDateString() + " 12:00:00 AM");
                    DateTime toDate = Convert.ToDateTime(tempToDate.ToShortDateString() + " 11:59:59 PM");
                    int deliveryBoyId = (int)cb_Name.SelectedValue;
                    int ammount = DeliveryQueue.getAmmountOfDeliveryOfaBoyWithDate(deliveryBoyId, fromDate, toDate);
                    lbl_Total.Content = ammount;
                }
                

            }
           
        }
    }
}
