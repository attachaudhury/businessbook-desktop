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

namespace RIAB_Restaurent_Management_System.Views.foodndeals
{
    public partial class Form_ManageDeals : Window
    {
        public Form_ManageDeals()
        {
            InitializeComponent();
            initFormOperation();
        }
        private void initFormOperation()
        {
            dg_AllDeals.ItemsSource = null;
            dg_AllDeals.ItemsSource = Deal.getAll();
            UpdateLayout();
        }
        

        private void btn_DeleteADeal(object sender, RoutedEventArgs e)
        {
            if (dg_AllDeals.SelectedItem != null) 
            {
                tbl_Deal deal = (tbl_Deal)dg_AllDeals.SelectedItem;
                Deal.delete(deal.Id);
                AutoClosingMessageBox.Show("Deal Deleted","Success",3000);
                initFormOperation();
            }
        }
        private void btn_EditADeal(object sender, RoutedEventArgs e)
        {
            if (dg_AllDeals.SelectedItem != null)
            {
                tbl_Deal deal = (tbl_Deal)dg_AllDeals.SelectedItem;
                new Form_EditADeal(deal.Id).Show();
                Close();
            }

        }
    }
}
