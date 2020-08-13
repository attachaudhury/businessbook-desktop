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
    public partial class Form_EditAFoodItem : Window
    {
        int cateroryid = 0;
        tbl_FoodItem foodItem;
        public Form_EditAFoodItem(int id)
        {
            InitializeComponent();
            initFormOperations(id);
        }
        private void btn_UpdateFoodItem(object sender, RoutedEventArgs e)
        {
            foodItem.Name = tb_FoodItemName.Text;
            foodItem.SalePrice = Convert.ToInt32(tb_FoodSalePrice.Text);
            foodItem.Category_Id = cateroryid;
            foodItem.ManageInventory = cbx_ManageInventory.IsChecked.Value;
            foodItem.Recipe = tb_FoodItemRecipe.Text;
            AutoClosingMessageBox.Show("Item Update", "Success", 2000);
            Close();
            new Form_ManageFoodItems().Show();
        }

        private void cb_FoodItemCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try {
                cateroryid = (int)cb_FoodItemCategory.SelectedValue;
            }
            catch 
            {
            }
            
        }
        void initFormOperations(int id)
        {

            foodItem = FoodItem.getById(id);
            tb_FoodItemName.Text = foodItem.Name;
            tb_FoodSalePrice.Text = Convert.ToString(foodItem.SalePrice);
            cateroryid = (int)foodItem.Category_Id;
            if (foodItem.ManageInventory == true)
            {
                cbx_ManageInventory.IsChecked = true;
            }
            tb_FoodItemRecipe.Text = foodItem.Recipe;
            var categoriesList = FoodItemCategory.getAll();
            foreach (tbl_FoodItemCategory item1 in categoriesList)
            {
                cb_FoodItemCategory.ItemsSource = categoriesList;
                cb_FoodItemCategory.DisplayMemberPath = "Name";
                cb_FoodItemCategory.SelectedValuePath = "Id";
            }
            cb_FoodItemCategory.SelectedValue = (int)foodItem.Category_Id;
            UpdateLayout();
        }
    }
}
