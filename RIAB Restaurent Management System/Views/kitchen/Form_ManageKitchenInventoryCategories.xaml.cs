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
    /// <summary>
    /// Interaction logic for Form_ManageKitchenInventoryCategories.xaml
    /// </summary>
    public partial class Form_ManageKitchenInventoryCategories : Window
    {
        public Form_ManageKitchenInventoryCategories()
        {
            InitializeComponent();
            initFormOperations();
        }
        void initFormOperations() 
        {
            dg_AllCategories.Items.Clear();
            foreach(tbl_KitchenInventoryCategory item in KitchenInventoryCategory.getAll())
            {
                dg_AllCategories.Items.Add(item);
            }
            UpdateLayout();
        }

        private void btn_AddNewCaterory(object sender, RoutedEventArgs e)
        {
            if (tb_Name.Text != "")
            {
                tbl_KitchenInventoryCategory kic = new tbl_KitchenInventoryCategory();
                kic.Name = tb_Name.Text;
                KitchenInventoryCategory.insert(kic);
                AutoClosingMessageBox.Show("Category Added", "Success", 3000);
                tb_Name.Text = "";
                initFormOperations();
            }
            else
            {
                AutoClosingMessageBox.Show("Please Enter Category Name","Alert",3000);
            }
        }

        private void btn_DeleteCaterory(object sender, RoutedEventArgs e)
        {
            if (dg_AllCategories.SelectedItem != null)
            {
                KitchenInventoryCategory.delete((tbl_KitchenInventoryCategory)dg_AllCategories.SelectedItem);
                AutoClosingMessageBox.Show("Category Deleted", "Success", 3000);
                initFormOperations();
            }
            else 
            {
                AutoClosingMessageBox.Show("Please Select a Item", "Alert", 3000);
            }
        }

        private void btn_UpdateCaterory(object sender, RoutedEventArgs e)
        {
            if (dg_AllCategories.SelectedItem != null)
            {
                tbl_KitchenInventoryCategory category = (tbl_KitchenInventoryCategory)dg_AllCategories.SelectedItem;
                var dialog = new Form_InputDialog(category.Name);
                if (dialog.ShowDialog() == true)
                {
                    category.Name = dialog.ResponseText;
                    KitchenInventoryCategory.update(category);
                    AutoClosingMessageBox.Show("Category Updated", "Success", 3000);
                    initFormOperations();
                }
            }
            else
            {
                AutoClosingMessageBox.Show("Please Select a Item", "Alert", 3000);
            }
        }
    }
}
