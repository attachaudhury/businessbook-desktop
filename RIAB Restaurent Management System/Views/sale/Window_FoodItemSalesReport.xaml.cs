using BLL.DBOperations;
using BLL.DBOperations.TmpModels;
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

namespace RIAB_Restaurent_Management_System.Views.sale
{
    public partial class Window_FoodItemSalesReport : Window
    {
        List<ItemOrDealSaleModel> mappedListOfAllItems;
        int selectedItemId;
        public Window_FoodItemSalesReport()
        {
            InitializeComponent();
            initFormOperations();
        }
        void initFormOperations()
        {
            mappedListOfAllItems = Sale.getMappedListofDealsAndItemsToItemOrDealSaleModel();
        }

        private void tb_Search_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                int index = lv_SearchFoodItem.SelectedIndex + 1;
                lv_SearchFoodItem.SelectedIndex = index;
                return;
            }
            if (e.Key == Key.Up)
            {
                int index = lv_SearchFoodItem.SelectedIndex - 1;
                lv_SearchFoodItem.SelectedIndex = index;
                return;
            }
            if (e.Key == Key.Enter)
            {
                if (lv_SearchFoodItem.SelectedItem != null)
                {
                    ItemOrDealSaleModel item = (ItemOrDealSaleModel)lv_SearchFoodItem.SelectedItem;
                    tb_Search.Text = item.Name;
                    selectedItemId = item.Id;
                    lv_SearchFoodItem.Visibility = Visibility.Hidden;
                }
            }
        }

        private void tb_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Search.Text != "")
            {
                string s = tb_Search.Text;
                List<ItemOrDealSaleModel> productList = mappedListOfAllItems.Where(a => a.Name.ToLower().Contains(s.ToLower())).ToList();
                lv_SearchFoodItem.ItemsSource = null;
                lv_SearchFoodItem.ItemsSource = productList;
                lv_SearchFoodItem.Visibility = Visibility.Visible;
                lv_SearchFoodItem.SelectedIndex = 0;
            }
            else { lv_SearchFoodItem.Visibility = Visibility.Hidden; }
        }

        private void btn_CustomDateSearch(object sender, RoutedEventArgs e)
        {
            if (dp_from.SelectedDate != null && dp_to.SelectedDate != null)
            {
                DateTime tempFromDate = (DateTime)dp_from.SelectedDate;
                DateTime tempToDate = (DateTime)dp_to.SelectedDate;

                DateTime fromDate = Convert.ToDateTime(tempFromDate.ToShortDateString() + " 12:00:00 AM");
                DateTime toDate = Convert.ToDateTime(tempToDate.ToShortDateString() + " 11:59:59 PM");
                dg_ItemSaleSearch.Items.Clear();
                List<ItemSaleSearchModel> list = Sale.itemSalesSearchByDate(selectedItemId, fromDate,toDate);
                int total = 0;
                int quantity = 0;
                foreach (ItemSaleSearchModel item in list)
                {
                    dg_ItemSaleSearch.Items.Add(item);
                    total += item.Total;
                    quantity += item.Quantity;
                }
                lbl_total.Content = total;
                lbl_totalQuantity.Content = quantity;


            }
        }
    }
}
