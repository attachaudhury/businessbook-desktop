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
    public partial class Form_EditKitchenInventory : Window
    {
        int catId;
        tbl_KitchenInventory ki;
        public Form_EditKitchenInventory(int id)
        {
            InitializeComponent();
            initFormOperations(id);
        }
        void initFormOperations(int id)
        {
            ki = KitchenInventory.getById(id);
            tb_Name.Text = ki.Name;
            tb_Quantity.Text = Convert.ToString(ki.Quantity);
            tb_MinimumQuantity.Text = Convert.ToString(ki.MinimumQuantity);
            tb_PurchasePrice.Text = Convert.ToString(ki.PurchasePrice);
            dp_ExpiryDate.SelectedDate = ki.ExpiryDate;
            catId = (int)ki.KitchenInventoryCategory_Id;
            cb_Category.SelectedValue = catId;
            UpdateLayout();
 
            var allCategories = KitchenInventoryCategory.getAll();
            foreach (tbl_KitchenInventoryCategory item in allCategories)
            {
                cb_Category.ItemsSource = allCategories;
                cb_Category.DisplayMemberPath = "Name";
                cb_Category.SelectedValuePath = "Id";
            }

        }

        private void btn_Update(object sender, RoutedEventArgs e)
        {
            if (tb_Name.Text == "" || tb_Quantity.Text == "" || tb_MinimumQuantity.Text == "" || tb_PurchasePrice.Text == "")
            {
                AutoClosingMessageBox.Show("Please Enter Correct Values", "Alert", 3000);
                return;
            }
            try
            {
                ki.Name = tb_Name.Text;
                ki.Quantity = Convert.ToDouble(tb_Quantity.Text);
                ki.MinimumQuantity = Convert.ToDouble(tb_MinimumQuantity.Text);
                ki.PurchasePrice = Convert.ToDouble(tb_PurchasePrice.Text);
                ki.ExpiryDate = dp_ExpiryDate.SelectedDate;
                ki.KitchenInventoryCategory_Id = catId;
                KitchenInventory.update(ki);
                AutoClosingMessageBox.Show("Item Updated", "Success", 3000);
                Close();
                new Form_ManageKitchenInventory().Show();

            }
            catch
            {
                AutoClosingMessageBox.Show("Some Error Occured", "Alert", 3000);
                return;
            }

        }
        private void cb_Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            catId = (int)cb_Category.SelectedValue;
        }
    }
}
