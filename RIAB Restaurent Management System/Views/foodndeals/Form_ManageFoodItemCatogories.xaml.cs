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
    public partial class Form_ManageFoodItemCatogories : Window
    {
        public Form_ManageFoodItemCatogories()
        {
            InitializeComponent();
            initFormOperations();
        }

        private void initFormOperations()
        {
            dg_AllFoodItemCategory.Items.Clear();
            dg_AllFoodItemCategory.Items.Refresh();
            foreach(tbl_FoodItemCategory item in FoodItemCategory.getAll()) 
            {
                dg_AllFoodItemCategory.Items.Add(item);
            }
        }

        private void btn_AddNewFoodItemCategory(object sender, RoutedEventArgs e)
        {
            if (tb_FoodItemCategory.Text != "")
            {
                tbl_FoodItemCategory category = new tbl_FoodItemCategory();
                category.Name = tb_FoodItemCategory.Text;
                FoodItemCategory.insert(category);
                AutoClosingMessageBox.Show("New Category Added", "Success", 3000);
                tb_FoodItemCategory.Text = "";
                initFormOperations();
            }
            else
            {
                AutoClosingMessageBox.Show("Please Enter A Name", "Alert", 3000);
            }
        }

        private void btn_RemoveFoodItemCategory(object sender, RoutedEventArgs e)
        {
            if (dg_AllFoodItemCategory.SelectedItem != null)
            {
                tbl_FoodItemCategory f = (tbl_FoodItemCategory)dg_AllFoodItemCategory.SelectedItem;
                FoodItemCategory.delete(f);
                initFormOperations();
            }
            else 
            {
                AutoClosingMessageBox.Show("Please Select a Item", "Alert", 3000);
            }
        }

        private void btn_UpdateFoodItemCategory(object sender, RoutedEventArgs e)
        {
            if (dg_AllFoodItemCategory.SelectedItem != null)
            {
                tbl_FoodItemCategory category = (tbl_FoodItemCategory)dg_AllFoodItemCategory.SelectedItem;
                var dialog = new Form_InputDialog(category.Name);
                if (dialog.ShowDialog() == true)
                {
                    category.Name = dialog.ResponseText;
                    FoodItemCategory.update(category);
                    AutoClosingMessageBox.Show("Category Updated", "Success", 3000);
                    initFormOperations();
                }
            }
            else
            {
                AutoClosingMessageBox.Show("Please Select a Item", "Alert", 3000);
            }
        }

        private void dg_AllFoodItemCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_AllFoodItemCategory.SelectedItem != null)
            {
                tbl_FoodItemCategory r = (tbl_FoodItemCategory)dg_AllFoodItemCategory.SelectedItem;
            }            
        }
    }
}
