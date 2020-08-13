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
using BLL.DBOperations.TmpModels;

namespace RIAB_Restaurent_Management_System.Views.foodndeals
{
    public partial class Form_EditADeal : Window
    {
        tbl_Deal deal;
        public Form_EditADeal(int dealId)
        {
            InitializeComponent();
            initFormOperations(dealId);
        }
        private void initFormOperations(int dealId)
        {
            deal = Deal.getById(dealId);
            tb_DealName.Text = deal.Name;
            tb_DealSalePrice.Text = Convert.ToString(deal.SalePrice);
            cbx_ManageInventory.IsChecked = deal.ManageInventory;
            foreach (FoodItemSmallModel item in Deal.getMAppedListOfDealDetailsToFoodItemSmallModelByDealId(dealId))
            {
                dg_DealFoodItems.Items.Add(item);
            }
            foreach (FoodItemSmallModel item in FoodItem.getMappedListOfAllFoodItemToFoodItemSmallModel())
            {
                dg_AllFoodItems.Items.Add(item);
            }
            foreach (tbl_FoodItemCategory item in FoodItemCategory.getAll())
            {
                cb_Cat.ItemsSource = FoodItemCategory.getAll();
                cb_Cat.DisplayMemberPath = "Name";
                cb_Cat.SelectedValuePath = "Id";
            }
            cb_Cat.SelectedValue = deal.Category_Id;
            update_lbl_Total();
        }
        private void btn_AddToDeal(object sender, RoutedEventArgs e)
        {
            if (dg_AllFoodItems.SelectedItem != null)
            {
                dg_DealFoodItems.Items.Add(dg_AllFoodItems.SelectedItem);
                update_lbl_Total();
            }
        }
        private void btn_RemoveFromDeal(object sender, RoutedEventArgs e)
        {
            if (dg_DealFoodItems.SelectedItem != null)
            {
                dg_DealFoodItems.Items.Remove(dg_DealFoodItems.SelectedItem);
                update_lbl_Total();
            }
        }
        private void btn_UpdateDeal(object sender, RoutedEventArgs e)
        {
            if (tb_DealName.Text == "")
            {
                AutoClosingMessageBox.Show("Please enter name", "Alert", 3000);
                return;
            }
            if (tb_DealSalePrice.Text == "")
            {
                AutoClosingMessageBox.Show("Please enter Privce", "Alert", 3000);
                return;
            }
            if (dg_DealFoodItems.Items.Count == 0)
            {
                AutoClosingMessageBox.Show("Please enter Items", "Alert", 3000);
                return;
            }

            int salePrice = 0;
            try
            {
                salePrice = Convert.ToInt16(tb_DealSalePrice.Text);
            }
            catch
            {
                AutoClosingMessageBox.Show("Please enter Valid Price", "Alert", 3000);
                return;
            }
            deal.Name = tb_DealName.Text;
            deal.SalePrice = salePrice;
            deal.ManageInventory = cbx_ManageInventory.IsChecked.Value;
            deal.Category_Id = (int)cb_Cat.SelectedValue;
            List<FoodItemSmallModel> list = new List<FoodItemSmallModel>();
            foreach (FoodItemSmallModel item in dg_DealFoodItems.Items)
            {
                list.Add(item);
            }
            Deal.update(deal,list);
            AutoClosingMessageBox.Show("Deal Updated", "Success", 3000);
            Close();
            new Form_ManageDeals().Show();
        }
        void update_lbl_Total()
        {
            int i = 0;
            foreach (FoodItemSmallModel item in dg_DealFoodItems.Items)
            {
                i += item.Price;
            }
            lbl_Total.Content = i;
        }
        

    }
}
