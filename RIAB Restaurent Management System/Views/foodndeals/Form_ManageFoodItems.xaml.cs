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
    public partial class Form_ManageFoodItems : Window
    {
        public Form_ManageFoodItems()
        {
            InitializeComponent();
            initFormOperations();
        }

        private void initFormOperations()
        {
            dg_AllFoodItems.ItemsSource = null;
            dg_AllFoodItems.ItemsSource = FoodItem.getAll();
            UpdateLayout();
        }
        private void btn_EditAFoodItem(object sender, RoutedEventArgs e)
        {
            if(dg_AllFoodItems.SelectedItem != null)
            {
                tbl_FoodItem fi = (tbl_FoodItem)dg_AllFoodItems.SelectedItem;
                new Form_EditAFoodItem(fi.Id).Show();
                Close();
            }
        }

        private void btn_DeleteAFoodItem(object sender, RoutedEventArgs e)
        {
            if (dg_AllFoodItems.SelectedItem != null)
            {
                tbl_FoodItem fi = (tbl_FoodItem)dg_AllFoodItems.SelectedItem;
                FoodItem.delete(fi);
                AutoClosingMessageBox.Show("Food Item Deleted", "Success", 3000);
                initFormOperations();

            }
            else
            {
                AutoClosingMessageBox.Show("Please Select A Item", "Alert", 3000);
            }
        }
    }
}
