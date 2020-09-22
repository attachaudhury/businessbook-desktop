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
using BusinessBook.data.viewmodel;
using BusinessBook.bll;
using BusinessBook.data;
using BusinessBook.data.dapper;

namespace BusinessBook.Views.finance
{
    /// <summary>
    /// Interaction logic for Window_NewSale.xaml
    /// </summary>
    public partial class pos : Window
    {
        List<productsaleorpurchaseviewmodel> mappedproducts;
        List<productsaleorpurchaseviewmodel> salelist = new List<productsaleorpurchaseviewmodel>();
        data.dapper.user customer = null;
        productrepo productrepo = new productrepo();

        public pos()
        {
            InitializeComponent();
            initFormOperations();
        }

        void initFormOperations()
        {
            //var db = new dbctx();
            var products = this.productrepo.get();
            mappedproducts = productutils.mapproducttoproductsalemodel(products);
            tb_Search.Focus();


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

        private void paying_textbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                doneSale();
            }
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

       private void customer_button_click(object sender, RoutedEventArgs e) {
            var dialog = new DialogSelectUser("customer");
            if (dialog.ShowDialog() == true)
            {
                customer = dialog.seleteduser;
                return;
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
                if (salelist.Count == 0)
                {
                    MessageBox.Show("Add products to cart", "Information");
                    return;
                }
                var totalbill = salelist.Sum(a => a.total);
                double totalpayment = 0;
                if (paying_textbox.Text != "") {
                    totalpayment = Convert.ToDouble(paying_textbox.Text);
                }
                var printCustomerInfoOnReceipt = true;
                var totalnumberofReceipts = 0;
                if ((bool)cbx_Receipt1.IsChecked) 
                {
                    totalnumberofReceipts++;
                }
                if ((bool)cbx_Receipt2.IsChecked)
                {
                    totalnumberofReceipts++;
                }
                if ((bool)cbx_Receipt3.IsChecked)
                {
                    totalnumberofReceipts++;
                }
                saleutils.possale(salelist, totalpayment-totalbill, customer, totalnumberofReceipts, printCustomerInfoOnReceipt);

                MessageBox.Show("Sale done Ammount :: " + totalbill, "Success");
                Close();
                new pos().Show();
            } 
            catch (Exception ex) 
            {
                MessageBox.Show("Sale not saved \n" + ex.Message, "Info");
            }

        }
    }

}
