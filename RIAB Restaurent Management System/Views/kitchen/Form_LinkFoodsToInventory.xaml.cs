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
using BLL;
using BLL.DBOperations;
using DAL;


namespace RIAB_Restaurent_Management_System.Views.kitchen
{
    public partial class Form_LinkFoodsToInventory : Window
    {
        List<DetuctInventoryModel> list = new List<DetuctInventoryModel>();

        int foodId;
        public Form_LinkFoodsToInventory()
        {
            InitializeComponent();
            initFormOperations();
        }
        void initFormOperations()
        {
            dg_AllInventoryItems.ItemsSource = KitchenInventory.getAll();
            var categoriesList = FoodItem.getAll();
            foreach (tbl_FoodItem item1 in categoriesList)
            {
                cb_AllFoodItems.ItemsSource = categoriesList;
                cb_AllFoodItems.DisplayMemberPath = "Name";
                cb_AllFoodItems.SelectedValuePath = "Id";
            }

            var categoriesList1 = Deal.getAll();
            foreach (tbl_Deal item1 in categoriesList1)
            {
                cb_AllDeals.ItemsSource = categoriesList1;
                cb_AllDeals.DisplayMemberPath = "Name";
                cb_AllDeals.SelectedValuePath = "Id";
            }

        }
        private void btn_Add(object sender, RoutedEventArgs e)
        {
            if (dg_AllInventoryItems.SelectedItem != null)
            {
                tbl_KitchenInventory ki = (tbl_KitchenInventory)dg_AllInventoryItems.SelectedItem;
                DetuctInventoryModel item = new DetuctInventoryModel();
                item.id = ki.Id;
                item.name = ki.Name;
                item.quantity = (double)ki.Quantity;
                list.Add(item);
                dg_SelectedInventoryItems.ItemsSource = null;
                dg_SelectedInventoryItems.ItemsSource = list;
            }
        }

        private void cb_AllFoodItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                foodId = (int)cb_AllFoodItems.SelectedValue;
            }
            catch { }
            
        }
        private void cb_AllDeals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                foodId = (int)cb_AllDeals.SelectedValue;
            }
            catch { }
        }
        private void btn_Save(object sender, RoutedEventArgs e)
        {
            foreach (DetuctInventoryModel item in list)
            {
                tbl_DetuctInventory di = new tbl_DetuctInventory();
                di.FoodItem_Id = foodId;
                di.KitchenInventory_id = item.id;
                di.DeductedQuantity = item.quantity;
                DetuctInventory.insert(di);
            }
            AutoClosingMessageBox.Show("Inventory Status Saved", "Success", 2000);
            Close();
            new Form_LinkKitchenInventory().Show();
        }

        private void btn_UpdateQuantity(object sender, RoutedEventArgs e)
        {
            if (dg_SelectedInventoryItems.SelectedItem != null)
            {
                DetuctInventoryModel dim = (DetuctInventoryModel)dg_SelectedInventoryItems.SelectedItem;
                double quantity = Convert.ToDouble(tb_Quantity.Text);
                DetuctInventoryModel dim1 = list.Find(a=>a.id == dim.id);
                dim1.quantity = quantity;
                dg_SelectedInventoryItems.ItemsSource = null;
                dg_SelectedInventoryItems.ItemsSource = list;
            }
        }
    }
    class DetuctInventoryModel
    {
       public int id { set; get; }
       public string name { set; get; }
       public double quantity { set; get; }
    }
}
