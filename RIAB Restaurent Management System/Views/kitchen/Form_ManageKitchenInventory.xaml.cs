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
    public partial class Form_ManageKitchenInventory : Window
    {
        public Form_ManageKitchenInventory()
        {
            InitializeComponent();
            initFormOperations();
        }
        void initFormOperations() 
        {
            dg_AllKitchenInventory.ItemsSource = null;
            dg_AllKitchenInventory.ItemsSource = KitchenInventory.getAll();
            UpdateLayout();
        }
        private void btn_AddNewKitchenInventory(object sender, RoutedEventArgs e)
        {
            new Form_AddNewKitchenInventory().Show();
            Close();
        }

        private void btn_DeleteKitchenInventory(object sender, RoutedEventArgs e)
        {
            if (dg_AllKitchenInventory.SelectedItem != null) 
            {
                KitchenInventory.delete((tbl_KitchenInventory)dg_AllKitchenInventory.SelectedItem);
                AutoClosingMessageBox.Show("Item Deleted", "Success", 3000);
                initFormOperations();
            }
        }

        private void btn_UpdateKitchenInventory(object sender, RoutedEventArgs e)
        {
            if (dg_AllKitchenInventory.SelectedItem != null)
            {
                tbl_KitchenInventory ki = (tbl_KitchenInventory)dg_AllKitchenInventory.SelectedItem;
                new Form_EditKitchenInventory(ki.Id).Show();
                Close();
            }

        }
        private void btn_InstantKitchenInventoryManager(object sender, RoutedEventArgs e)
        {
            new Form_InstantKitchenInventoryManager().Show();
        }
    }
}
