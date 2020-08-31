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

using System.Reflection;
using System.Media;
using RIAB_Restaurent_Management_System.data.viewmodel;
using RIAB_Restaurent_Management_System.bll;
using RIAB_Restaurent_Management_System.data;

namespace RIAB_Restaurent_Management_System.Views.finance
{
    /// <summary>
    /// Interaction logic for Window_NewSale.xaml
    /// </summary>
    public partial class salenew : Window
    {
        List<productsaleorpurchaseviewmodel> mappedproducts;
        List<productsaleorpurchaseviewmodel> salelist = new List<productsaleorpurchaseviewmodel>();
        int customerId = 0;
        bool isDelivery = false;
        int deliveryBoyId;
        string customerAddress = "";

        public salenew()
        {
            InitializeComponent();
            initFormOperations();
        }

        void initFormOperations()
        {
            var db = new dbctx();
            mappedproducts = productutils.mapproducttoproductsalemodel(db.product.ToList());
            tb_Search.Focus();

            var customers = db.user.Where(a => a.role == "customer").ToList();
            customer_combobox.ItemsSource = customers;
            customer_combobox.DisplayMemberPath = "name";
            customer_combobox.SelectedValuePath = "id";

        }

        private void paying_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int total = Convert.ToInt32(paying_textbox.Text) - Convert.ToInt32(total_label.Content);
                change_label.Content = total;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void paying_textbox_KeyDownPressEnter(object sender, KeyEventArgs e)
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
                    int total = Convert.ToInt32(total_label.Content);
                    total_label.Content = Convert.ToInt32(total);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        void addItem_To_SaleList(productsaleorpurchaseviewmodel item)
        {
            foreach (productsaleorpurchaseviewmodel oldItem in salelist)
            {
                if (item.id == oldItem.id)
                {
                    oldItem.quantity += 1;
                    oldItem.total = oldItem.quantity * oldItem.price;
                    dg_SellingList.Items.Clear();
                    double totalBill1 = 0;
                    foreach (productsaleorpurchaseviewmodel item1 in salelist)
                    {
                        totalBill1 += item1.total;
                        dg_SellingList.Items.Add(item1);
                    }
                    total_label.Content = totalBill1;
                    return;
                }
            }
            salelist.Add(item);
            dg_SellingList.Items.Clear();
            double totalBill = 0;
            foreach (productsaleorpurchaseviewmodel item1 in salelist)
            {
                totalBill += item1.total;
                dg_SellingList.Items.Add(item1);
            }
            total_label.Content = totalBill;
        }
        
        private void btn_AddQuantity(object sender, RoutedEventArgs e)
        {
            productsaleorpurchaseviewmodel obj = ((FrameworkElement)sender).DataContext as productsaleorpurchaseviewmodel;
            addItem_To_SaleList(obj);
        }

        private void btn_RemoveQuantity(object sender, RoutedEventArgs e)
        {
            productsaleorpurchaseviewmodel obj = ((FrameworkElement)sender).DataContext as productsaleorpurchaseviewmodel;

            foreach (productsaleorpurchaseviewmodel oldItem in salelist)
            {
                if (obj.id == oldItem.id)
                {
                    if (oldItem.quantity > 1)
                    {
                        oldItem.quantity -= 1;
                        oldItem.total = oldItem.quantity * oldItem.price;
                        dg_SellingList.Items.Clear();
                        double totalBill1 = 0;
                        foreach (productsaleorpurchaseviewmodel item1 in salelist)
                        {
                            totalBill1 += item1.total;
                            dg_SellingList.Items.Add(item1);
                        }
                        total_label.Content = totalBill1;
                        return;
                    }
                    else
                    {
                        salelist.Remove(obj);
                        dg_SellingList.Items.Clear();
                        double totalBill1 = 0;
                        foreach (productsaleorpurchaseviewmodel item1 in salelist)
                        {
                            totalBill1 += item1.total;
                            dg_SellingList.Items.Add(item1);
                        }
                        total_label.Content = totalBill1;
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
                List<productsaleorpurchaseviewmodel> productList = mappedproducts.Where(a => a.name.ToLower().Contains(s.ToLower())).ToList();
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
                    customerAddress = dialog.ResponseAddress + " " + dialog.ResponsePhone;
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
                    productsaleorpurchaseviewmodel item = (productsaleorpurchaseviewmodel)lv_SearchFoodItem.SelectedItem;
                    addItem_To_SaleList(item);
                    tb_Search.Text = "";
                    lv_SearchFoodItem.Visibility = Visibility.Hidden;
                }
            }

        }

        private void btn_doSale(object sender, RoutedEventArgs e)
        {
            doneSale();
        }

        void doneSale()
        {
            try 
            {


                if ((bool)ledger_checkbox.IsChecked) 
                {

                }
                int saleType = 1;
                if (isDelivery)
                {
                    saleType = 3;
                }
                int totalBill = Convert.ToInt32(total_label.Content);
                int Remaining = Convert.ToInt32(change_label.Content);
                bool reciept1 = cbx_Receipt1.IsChecked.Value;
                bool reciept2 = cbx_Receipt2.IsChecked.Value;
                bool reciept3 = cbx_Receipt3.IsChecked.Value;
                double discount = 0;
                saleutils.newsale(salelist, totalBill, Remaining, customerId, reciept1, reciept2, reciept3, saleType, customerAddress, deliveryBoyId);

                MessageBox.Show("Ammount " + totalBill, "Success");
                Close();
                new pos().Show();
            } 
            catch (Exception ex) 
            {
            }
            
        }
    }

}
