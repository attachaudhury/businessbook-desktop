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

namespace RIAB_Restaurent_Management_System.Views.kitchen
{
    public partial class Form_AddNewKitchenInventory : Window
    {
        int catId = 0;
        public Form_AddNewKitchenInventory()
        {
            InitializeComponent();
            initFormOperations();
        }
        void initFormOperations() 
        {
            var allCategories = KitchenInventoryCategory.getAll();
            foreach(tbl_KitchenInventoryCategory item in allCategories)
            {
                cb_Category.ItemsSource = allCategories;
                cb_Category.DisplayMemberPath = "Name";
                cb_Category.SelectedValuePath = "Id";
            }

        }

        private void btn_Save(object sender, RoutedEventArgs e)
        {
            if (tb_Name.Text == "" || tb_Quantity.Text == "" || tb_MinimumQuantity.Text == "" || tb_PurchasePrice.Text == "") 
            {
                AutoClosingMessageBox.Show("Please Enter Correct Values", "Alert", 3000);
                return;
            }

            tbl_KitchenInventory ki = new tbl_KitchenInventory();
            try
            {
                ki.Name = tb_Name.Text;
                ki.Quantity = Convert.ToDouble(tb_Quantity.Text);
                ki.MinimumQuantity = Convert.ToDouble(tb_MinimumQuantity.Text);
                ki.PurchasePrice = Convert.ToDouble(tb_PurchasePrice.Text);
                ki.ExpiryDate = dp_ExpiryDate.SelectedDate;
                ki.KitchenInventoryCategory_Id = catId;
                KitchenInventory.insert(ki);
                AutoClosingMessageBox.Show("Item Added", "Success", 3000);
                Close();
                new Form_AddNewKitchenInventory().Show();

            }
            catch 
            {
                AutoClosingMessageBox.Show("Some Error Occured", "Alert", 3000);
                return;
            }

        }

        private void dp_ExpiryDate_Loaded(object sender, RoutedEventArgs e)
        {
            dp_ExpiryDate.SelectedDate = DateTime.Now.AddDays(15);
        }

        private void cb_Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            catId = (int)cb_Category.SelectedValue;
        }
    }
}
