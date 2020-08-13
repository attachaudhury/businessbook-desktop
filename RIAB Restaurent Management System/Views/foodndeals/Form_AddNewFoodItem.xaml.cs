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
    
    public partial class Form_AddNewFoodItem : Window
    {
        int cateroryid = 0;
        
        public Form_AddNewFoodItem()
        {
            InitializeComponent();
            initFormOperations();
        }

        private void btn_SaveFoodItem(object sender, RoutedEventArgs e)
        {
            tbl_FoodItem r = new tbl_FoodItem();
            r.Name = tb_FoodItemName.Text;
            r.SalePrice = Convert.ToInt32(tb_FoodSalePrice.Text);
            r.Category_Id = cateroryid;
            r.ManageInventory = cbx_ManageInventory.IsChecked.Value;
            r.Recipe = tb_FoodItemRecipe.Text;
            FoodItem.insert(r);
            Close();
            new Form_AddNewFoodItem().Show();
        }
        
        private void cb_FoodItemCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cateroryid = (int)cb_FoodItemCategory.SelectedValue;
        }
        void initFormOperations()
        {
            var categoriesList = FoodItemCategory.getAll();
            foreach (tbl_FoodItemCategory item1 in categoriesList)
            {
                cb_FoodItemCategory.ItemsSource = categoriesList;
                cb_FoodItemCategory.DisplayMemberPath = "Name";
                cb_FoodItemCategory.SelectedValuePath = "Id";
            }
        }

    }
}
