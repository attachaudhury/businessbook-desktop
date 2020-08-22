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

using System.Data.Entity.Migrations;
using RIAB_Restaurent_Management_System.data;

namespace RIAB_Restaurent_Management_System.Views.product
{
    /// <summary>
    /// Interaction logic for ProductAdd.xaml
    /// </summary>
    public partial class ProductAdd : Window
    {
        bool createmode = true;
        data.product selectedproduct = null;
        public ProductAdd(int? productId = null)
        {
            InitializeComponent();
            if (productId == null)
            {
                this.createmode = true;
            }
            else
            {
                this.createmode = false;
                tb_quantity.IsEnabled = false;
                this.getone((int)productId);
            }

            initFormOperations();
        }
        private void btn_Save(object sender, RoutedEventArgs e)
        {
            var db = DBContext.getInstance();
            if (tb_name.Text == "" || tb_saleprice.Text == "" || tb_purchaseprice.Text == "")
            {
                MessageBox.Show("Please fill form", "Information");
                return;
            }
            if (this.createmode)
            {
                data.product r = new data.product();
                r.name = tb_name.Text;
                r.saleprice = Convert.ToInt32(tb_saleprice.Text);
                r.purchaseprice = Convert.ToInt32(tb_purchaseprice.Text);
                if (tb_discount.Text != "")
                {
                    r.discount = Convert.ToInt32(tb_discount.Text);
                }
                if (tb_carrycost.Text != "")
                {
                    r.carrycost = Convert.ToInt32(tb_carrycost.Text);
                }
                if (tb_discount.Text != "")
                {
                    r.discount = Convert.ToInt32(tb_discount.Text);
                }
                r.barcode = tb_barcode.Text;
                if (tb_quantity.Text != "")
                {
                    r.quantity = Convert.ToInt32(tb_quantity.Text);
                }
                //r.type = (string)cb_Type.SelectedValue;
                r.saleactive = cbx_SaleActive.IsChecked.Value;
                r.purchaseactive = cbx_PurchaseActive.IsChecked.Value;
                db.product.Add(r);
                db.SaveChanges();
                selectedproduct = r;
                this.createmode = false;
                MessageBox.Show("Product saved", "Information");
            }
            else
            {
                selectedproduct.name = tb_name.Text;
                selectedproduct.saleprice = Convert.ToInt32(tb_saleprice.Text);
                selectedproduct.purchaseprice = Convert.ToInt32(tb_purchaseprice.Text);
                if (tb_discount.Text != "")
                {
                    selectedproduct.discount = Convert.ToInt32(tb_discount.Text);
                }
                if (tb_carrycost.Text != "")
                {
                    selectedproduct.carrycost = Convert.ToInt32(tb_carrycost.Text);
                }
                if (tb_discount.Text != "")
                {
                    selectedproduct.discount = Convert.ToInt32(tb_discount.Text);
                }
                selectedproduct.barcode = tb_barcode.Text;

                //r.type = (string)cb_Type.SelectedValue;
                selectedproduct.saleactive = cbx_SaleActive.IsChecked.Value;
                selectedproduct.purchaseactive = cbx_PurchaseActive.IsChecked.Value;
                db.product.AddOrUpdate(selectedproduct);
                db.SaveChanges();
                MessageBox.Show("product saved", "Information");
            }
        }
        private void btn_AddSubProduct(object sender, RoutedEventArgs e)
        {
            if (this.createmode)
            {
                MessageBox.Show("Please save product first", "Information");
            }
            else
            {
                if (products_cb.SelectedItem == null)
                {
                    MessageBox.Show("Please select product", "Information");
                    return;
                }
                if (tb_subproductquantity.Text == "" || tb_subproductquantity.Text == "0")
                {
                    MessageBox.Show("Please add quantity", "Information");
                    return;
                }
                var products_cb_selectedobject = products_cb.SelectedItem as data.product;
                subproduct subproduct = new subproduct();
                subproduct.fk_product_product_subproduct = selectedproduct.id;
                subproduct.fk_subproduct_product_subproduct = products_cb_selectedobject.id;
                subproduct.quantity = Convert.ToInt32(tb_subproductquantity.Text);
                var db = DBContext.getInstance();
                db.subproduct.Add(subproduct);
                db.SaveChanges();
                dg.Items.Clear();
                var subproducts = db.subproduct.Where(a => a.fk_product_product_subproduct == selectedproduct.id).ToList();
                foreach (var item in subproducts)
                {
                    dg.Items.Add(item);
                }
                products_cb.SelectedItem = null;
                tb_subproductquantity.Text = "";
            }
        }
        public void btn_removeSubProduct(object sender, RoutedEventArgs e)
        {
            subproduct obj = ((FrameworkElement)sender).DataContext as subproduct;
            var db = new dbctx();
            var dbsubproduct = db.subproduct.Find(obj.id);
            db.subproduct.Remove(dbsubproduct);
            db.SaveChanges();
            dg.Items.Clear();
            var subproducts = db.subproduct.Where(a => a.fk_product_product_subproduct == selectedproduct.id).ToList();
            foreach (var item in subproducts)
            {
                dg.Items.Add(item);
            }

        }



        void initFormOperations()
        {
            //var types = new string[] {"product", "deal", "raw"};
            //cb_Type.ItemsSource = types;
            var db = new dbctx();

            var products = db.product.ToList();

            foreach (data.product item1 in products)
            {
                products_cb.ItemsSource = products;
                products_cb.DisplayMemberPath = "name";
                products_cb.SelectedValuePath = "id";
            }
        }
        void getone(int productid)
        {
            var db = new dbctx();
            selectedproduct = db.product.Find(productid);
            tb_name.Text = selectedproduct.name;
            if (selectedproduct.saleprice != null)
            {
                tb_saleprice.Text = selectedproduct.saleprice.ToString();
            }
            if (selectedproduct.purchaseprice != null)
            {
                tb_purchaseprice.Text = selectedproduct.purchaseprice.ToString();
            }
            if (selectedproduct.discount != null)
            {
                tb_discount.Text = selectedproduct.discount.ToString();
            }
            if (selectedproduct.carrycost != null)
            {
                tb_carrycost.Text = selectedproduct.carrycost.ToString();
            }
            if (selectedproduct.barcode != null)
            {
                tb_barcode.Text = selectedproduct.barcode.ToString();
            }
            if (selectedproduct.quantity != null)
            {
                tb_quantity.Text = selectedproduct.quantity.ToString();
            }

            cbx_SaleActive.IsChecked = selectedproduct.saleactive;
            cbx_PurchaseActive.IsChecked = selectedproduct.purchaseactive;

            var subproducts = db.subproduct.Where(a => a.fk_product_product_subproduct == productid).ToList();

            foreach (var item in subproducts)
            {
                dg.Items.Add(item);
            }
        }
    }
}
