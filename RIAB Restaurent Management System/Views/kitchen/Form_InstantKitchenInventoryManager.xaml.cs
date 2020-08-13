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
    public partial class Form_InstantKitchenInventoryManager : Window
    {
        public Form_InstantKitchenInventoryManager()
        {
            InitializeComponent();
            initFormOperations();
        }
        void initFormOperations() 
        {
            dg_AllKitchenItems.Items.Clear();
            dg_AllLowInventoryItems.Items.Clear();
            foreach(tbl_KitchenInventory item in KitchenInventory.getAll())
            {
                dg_AllKitchenItems.Items.Add(item);
            }
            foreach (tbl_KitchenInventory item in KitchenInventory.getLowInventoryItem())
            {
                dg_AllLowInventoryItems.Items.Add(item);
            }
        }

        private void tb_Quantity_KeyDown_PressEnter(object sender, KeyEventArgs e)
        {
            if (dg_AllKitchenItems.SelectedItem != null)
            {
                if (e.Key == Key.Enter) 
                {
                    tbl_KitchenInventory ki = (tbl_KitchenInventory)dg_AllKitchenItems.SelectedItem;
                    ki.Quantity = Convert.ToDouble(tb_Quantity.Text);
                    KitchenInventory.update(ki);
                    initFormOperations();
                }

            }
        }

        private void tb_Price_KeyDown_PressEnter(object sender, KeyEventArgs e)
        {
            if (dg_AllKitchenItems.SelectedItem != null)
            {
                if (e.Key == Key.Enter)
                {
                    tbl_KitchenInventory ki = (tbl_KitchenInventory)dg_AllKitchenItems.SelectedItem;
                    ki.PurchasePrice = Convert.ToDouble(tb_Price.Text);
                    KitchenInventory.update(ki);
                    initFormOperations();
                }

            }
        }
    }
}
