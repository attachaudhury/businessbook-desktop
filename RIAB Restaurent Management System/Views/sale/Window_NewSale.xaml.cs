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
using BLL.DBOperations.TmpModels;
using DAL;
using System.Reflection;
using System.Media;

namespace RIAB_Restaurent_Management_System.Views.sale
{
    /// <summary>
    /// Interaction logic for Window_NewSale.xaml
    /// </summary>
    public partial class Window_NewSale : Window
    {
        List<ItemOrDealSaleModel> mappedListOfAllItems;
        List<ItemOrDealSaleModel> mappedListOfDeals;
        List<ItemOrDealSaleModel> salelist = new List<ItemOrDealSaleModel>();
        List<Brush> myBrushes = new List<Brush>();
        int customerId = 0;
        bool isDelivery = false;
        int deliveryBoyId;
        string customerAddress = "";


        public Window_NewSale()
        {
            InitializeComponent();
            initFormOperations();

        }
        void initFormOperations()
        {
            mappedListOfAllItems = Sale.getMappedListofDealsAndItemsToItemOrDealSaleModel();
            mappedListOfDeals = Sale.getMappedListofDealsToItemOrDealSaleModel();

            myBrushes.Add(Brushes.LimeGreen);
            myBrushes.Add(Brushes.Olive);
            myBrushes.Add(Brushes.DarkViolet);
            myBrushes.Add(Brushes.BlueViolet);
            myBrushes.Add(Brushes.Brown);
            myBrushes.Add(Brushes.Chocolate);
            myBrushes.Add(Brushes.Magenta);
            myBrushes.Add(Brushes.Purple);
            myBrushes.Add(Brushes.Teal);
            myBrushes.Add(Brushes.DarkGray);
            myBrushes.Add(Brushes.DarkGreen);
            myBrushes.Add(Brushes.DarkKhaki);
            myBrushes.Add(Brushes.DarkOrange);
            myBrushes.Add(Brushes.DarkRed);
            myBrushes.Add(Brushes.DarkSeaGreen);
            myBrushes.Add(Brushes.DarkSlateGray);
            myBrushes.Add(Brushes.DarkViolet);
            myBrushes.Add(Brushes.DeepPink);
            myBrushes.Add(Brushes.DodgerBlue);
            myBrushes.Add(Brushes.Firebrick);
            myBrushes.Add(Brushes.ForestGreen);
            myBrushes.Add(Brushes.Gainsboro);
            myBrushes.Add(Brushes.Goldenrod);
            myBrushes.Add(Brushes.GreenYellow);
            myBrushes.Add(Brushes.RoyalBlue);

            myBrushes.Add(Brushes.LimeGreen);
            myBrushes.Add(Brushes.Olive);
            myBrushes.Add(Brushes.DarkViolet);
            myBrushes.Add(Brushes.BlueViolet);
            myBrushes.Add(Brushes.Brown);
            myBrushes.Add(Brushes.Chocolate);
            myBrushes.Add(Brushes.Magenta);
            myBrushes.Add(Brushes.Purple);
            myBrushes.Add(Brushes.Teal);
            myBrushes.Add(Brushes.DarkGray);
            myBrushes.Add(Brushes.DarkGreen);
            myBrushes.Add(Brushes.DarkKhaki);
            myBrushes.Add(Brushes.DarkOrange);
            myBrushes.Add(Brushes.DarkRed);
            myBrushes.Add(Brushes.DarkSeaGreen);
            myBrushes.Add(Brushes.DarkSlateGray);
            myBrushes.Add(Brushes.DarkViolet);
            myBrushes.Add(Brushes.DeepPink);
            myBrushes.Add(Brushes.DodgerBlue);
            myBrushes.Add(Brushes.Firebrick);
            myBrushes.Add(Brushes.ForestGreen);
            myBrushes.Add(Brushes.Gainsboro);
            myBrushes.Add(Brushes.Goldenrod);
            myBrushes.Add(Brushes.GreenYellow);
            myBrushes.Add(Brushes.RoyalBlue);


            myBrushes.Add(Brushes.LimeGreen);
            myBrushes.Add(Brushes.Olive);
            myBrushes.Add(Brushes.DarkViolet);
            myBrushes.Add(Brushes.BlueViolet);
            myBrushes.Add(Brushes.Brown);
            myBrushes.Add(Brushes.Chocolate);
            myBrushes.Add(Brushes.Magenta);
            myBrushes.Add(Brushes.Purple);
            myBrushes.Add(Brushes.Teal);
            myBrushes.Add(Brushes.DarkGray);
            myBrushes.Add(Brushes.DarkGreen);
            myBrushes.Add(Brushes.DarkKhaki);
            myBrushes.Add(Brushes.DarkOrange);
            myBrushes.Add(Brushes.DarkRed);
            myBrushes.Add(Brushes.DarkSeaGreen);
            myBrushes.Add(Brushes.DarkSlateGray);
            myBrushes.Add(Brushes.DarkViolet);
            myBrushes.Add(Brushes.DeepPink);
            myBrushes.Add(Brushes.DodgerBlue);
            myBrushes.Add(Brushes.Firebrick);
            myBrushes.Add(Brushes.ForestGreen);
            myBrushes.Add(Brushes.Gainsboro);
            myBrushes.Add(Brushes.Goldenrod);
            myBrushes.Add(Brushes.GreenYellow);
            myBrushes.Add(Brushes.RoyalBlue);



            myBrushes.Add(Brushes.LimeGreen);
            myBrushes.Add(Brushes.Olive);
            myBrushes.Add(Brushes.DarkViolet);
            myBrushes.Add(Brushes.BlueViolet);
            myBrushes.Add(Brushes.Brown);
            myBrushes.Add(Brushes.Chocolate);
            myBrushes.Add(Brushes.Magenta);
            myBrushes.Add(Brushes.Purple);
            myBrushes.Add(Brushes.Teal);
            myBrushes.Add(Brushes.DarkGray);
            myBrushes.Add(Brushes.DarkGreen);
            myBrushes.Add(Brushes.DarkKhaki);
            myBrushes.Add(Brushes.DarkOrange);
            myBrushes.Add(Brushes.DarkRed);
            myBrushes.Add(Brushes.DarkSeaGreen);
            myBrushes.Add(Brushes.DarkSlateGray);
            myBrushes.Add(Brushes.DarkViolet);
            myBrushes.Add(Brushes.DeepPink);
            myBrushes.Add(Brushes.DodgerBlue);
            myBrushes.Add(Brushes.Firebrick);
            myBrushes.Add(Brushes.ForestGreen);
            myBrushes.Add(Brushes.Gainsboro);
            myBrushes.Add(Brushes.Goldenrod);
            myBrushes.Add(Brushes.GreenYellow);
            myBrushes.Add(Brushes.RoyalBlue);



            myBrushes.Add(Brushes.LimeGreen);
            myBrushes.Add(Brushes.Olive);
            myBrushes.Add(Brushes.DarkViolet);
            myBrushes.Add(Brushes.BlueViolet);
            myBrushes.Add(Brushes.Brown);
            myBrushes.Add(Brushes.Chocolate);
            myBrushes.Add(Brushes.Magenta);
            myBrushes.Add(Brushes.Purple);
            myBrushes.Add(Brushes.Teal);
            myBrushes.Add(Brushes.DarkGray);
            myBrushes.Add(Brushes.DarkGreen);
            myBrushes.Add(Brushes.DarkKhaki);
            myBrushes.Add(Brushes.DarkOrange);
            myBrushes.Add(Brushes.DarkRed);
            myBrushes.Add(Brushes.DarkSeaGreen);
            myBrushes.Add(Brushes.DarkSlateGray);
            myBrushes.Add(Brushes.DarkViolet);
            myBrushes.Add(Brushes.DeepPink);
            myBrushes.Add(Brushes.DodgerBlue);
            myBrushes.Add(Brushes.Firebrick);
            myBrushes.Add(Brushes.ForestGreen);
            myBrushes.Add(Brushes.Gainsboro);
            myBrushes.Add(Brushes.Goldenrod);
            myBrushes.Add(Brushes.GreenYellow);
            myBrushes.Add(Brushes.RoyalBlue);


            myBrushes.Add(Brushes.LimeGreen);
            myBrushes.Add(Brushes.Olive);
            myBrushes.Add(Brushes.DarkViolet);
            myBrushes.Add(Brushes.BlueViolet);
            myBrushes.Add(Brushes.Brown);
            myBrushes.Add(Brushes.Chocolate);
            myBrushes.Add(Brushes.Magenta);
            myBrushes.Add(Brushes.Purple);
            myBrushes.Add(Brushes.Teal);
            myBrushes.Add(Brushes.DarkGray);
            myBrushes.Add(Brushes.DarkGreen);
            myBrushes.Add(Brushes.DarkKhaki);
            myBrushes.Add(Brushes.DarkOrange);
            myBrushes.Add(Brushes.DarkRed);
            myBrushes.Add(Brushes.DarkSeaGreen);
            myBrushes.Add(Brushes.DarkSlateGray);
            myBrushes.Add(Brushes.DarkViolet);
            myBrushes.Add(Brushes.DeepPink);
            myBrushes.Add(Brushes.DodgerBlue);
            myBrushes.Add(Brushes.Firebrick);
            myBrushes.Add(Brushes.ForestGreen);
            myBrushes.Add(Brushes.Gainsboro);
            myBrushes.Add(Brushes.Goldenrod);
            myBrushes.Add(Brushes.GreenYellow);
            myBrushes.Add(Brushes.RoyalBlue);

            int i= 1;
            foreach (ItemOrDealSaleModel item in  mappedListOfDeals)
            {
                Button b = new Button();
                b.Content = item.Name;
                b.Name = "Deal"+item.Id.ToString();
                b.FontWeight = FontWeights.Light;
                b.FontSize = 12;
                Brush selectedBrush = myBrushes.ElementAt(i);
                b.Background = selectedBrush;
                b.Height = 45;
                b.Width = 80;
                b.Margin = new Thickness(2, 2, 2, 2);
                
                wp_Deals.Children.Add(b);
                b.Click += new RoutedEventHandler(btn_DealClick);
                i++;
            }

            tb_Search.Focus();

        }
        void addItem_To_SaleList(ItemOrDealSaleModel item)
        {
            foreach (ItemOrDealSaleModel oldItem in salelist)
            {
                if (item.Id == oldItem.Id)
                {
                    oldItem.Quantity += 1;
                    oldItem.Total = oldItem.Quantity * oldItem.SalePrice;
                    dg_SellingList.Items.Clear();
                    int totalBill1 = 0;
                    foreach (ItemOrDealSaleModel item1 in salelist)
                    {
                        totalBill1 += item1.Total;
                        dg_SellingList.Items.Add(item1);
                    }
                    lbl_Total.Content = totalBill1;
                    return;
                }
            }
            salelist.Add(item);
            dg_SellingList.Items.Clear();
            int totalBill = 0;
            foreach (ItemOrDealSaleModel item1 in salelist)
            {
                totalBill += item1.Total;
                dg_SellingList.Items.Add(item1);
            }
            lbl_Total.Content = totalBill;
        }
        private void btn_DealClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string a = b.Name.Substring(4);
            int id = Convert.ToInt32(a);
            ItemOrDealSaleModel item =  mappedListOfAllItems.Where(c => c.Id == id).FirstOrDefault();
            addItem_To_SaleList(item);
        }
        private void btn_AddQuantity(object sender, RoutedEventArgs e)
        {
            ItemOrDealSaleModel obj = ((FrameworkElement)sender).DataContext as ItemOrDealSaleModel;
            addItem_To_SaleList(obj);
        }
        private void btn_RemoveQuantity(object sender, RoutedEventArgs e)
        {
            ItemOrDealSaleModel obj = ((FrameworkElement)sender).DataContext as ItemOrDealSaleModel;

            foreach (ItemOrDealSaleModel oldItem in salelist)
            {
                if (obj.Id == oldItem.Id)
                {
                    if (oldItem.Quantity > 1)
                    {
                        oldItem.Quantity -= 1;
                        oldItem.Total = oldItem.Quantity * oldItem.SalePrice;
                        dg_SellingList.Items.Clear();
                        int totalBill1 = 0;
                        foreach (ItemOrDealSaleModel item1 in salelist)
                        {
                            totalBill1 += item1.Total;
                            dg_SellingList.Items.Add(item1);
                        }
                        lbl_Total.Content = totalBill1;
                        return;
                    }
                    else
                    {
                        salelist.Remove(obj);
                        dg_SellingList.Items.Clear();
                        int totalBill1 = 0;
                        foreach (ItemOrDealSaleModel item1 in salelist)
                        {
                            totalBill1 += item1.Total;
                            dg_SellingList.Items.Add(item1);
                        }
                        lbl_Total.Content = totalBill1;
                        return;
                    }
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
                lv_SearchFoodItem.Visibility = Visibility.Visible;
            }
            else { lv_SearchFoodItem.Visibility = Visibility.Hidden; }
        }
        private void tb_Search_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                var dialog = new Form_InputDialogForAddCustomerInNewSale();
                if (dialog.ShowDialog() == true)
                {
                    isDelivery = true;
                    customerId = dialog.Customer_Id;
                    customerAddress = dialog.ResponseAddress +" "+ dialog.ResponsePhone;
                    if (dialog.DeliveryBoy_Id != null)
                    {
                        deliveryBoyId = (int)dialog.DeliveryBoy_Id;
                    } 
                    return;
                }
            }
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
                    addItem_To_SaleList(item);
                    tb_Search.Text = "";
                    lv_SearchFoodItem.Visibility = Visibility.Hidden;
                }
            }

        }

        private void tb_Paying_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int total = Convert.ToInt32(tb_Paying.Text)- Convert.ToInt32(lbl_Total.Content);
                lbl_Remaining.Content = total;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tb_Paying_KeyDownPressEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                doneSale();
            }
        }

        private void tb_Discount_KeyDown_PressEnter(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    int discount = Convert.ToInt32(tb_Discount.Text);
                    int total = Convert.ToInt32(lbl_Total.Content);
                    double discounedBill = total - ((total * discount) / 100);
                    lbl_Total.Content = Convert.ToInt32(discounedBill);
                }
            } catch(Exception ex) { MessageBox.Show(ex.Message); }
            
        }
        void doneSale()
        {
            int saleType = 1;
            if (isDelivery)
            {
                saleType = 3;
            }
            int totalBill = Convert.ToInt32(lbl_Total.Content);
            int Remaining = Convert.ToInt32(lbl_Remaining.Content);
            bool reciept1 = cbx_Receipt1.IsChecked.Value;
            bool reciept2 = cbx_Receipt2.IsChecked.Value;
            bool reciept3 = cbx_Receipt3.IsChecked.Value;
            double discount = 0;
            Sale.performANewSale(salelist, discount, totalBill, Remaining, customerId, reciept1, reciept2, reciept3, saleType,customerAddress,deliveryBoyId);
            try
            {
                SoundPlayer player = new SoundPlayer("../../media/2.wav");
                player.Load();
                player.Play();
            }
            catch { }

            AutoClosingMessageBox.Show("Ammount " + totalBill, "Success", 2000);
            Close();
            new Window_NewSale().Show();
        }

        private void btn_doSale(object sender, RoutedEventArgs e)
        {
            doneSale();
        }
    }

}
