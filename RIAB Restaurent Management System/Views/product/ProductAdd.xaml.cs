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

namespace RIAB_Restaurent_Management_System.Views.product
{
    /// <summary>
    /// Interaction logic for ProductAdd.xaml
    /// </summary>
    public partial class ProductAdd : Window
    {
        public ProductAdd()
        {
            InitializeComponent();
            initFormOperations();
        }
        private void btn_Save(object sender, RoutedEventArgs e)
        {
            DAL.product r = new DAL.product();
            r.name = tb_name.Text;
            r.saleprice = Convert.ToInt32(tb_saleprice.Text);
            r.purchaseprice = Convert.ToInt32(tb_purchaseprice.Text);
            r.discount = Convert.ToInt32(tb_discount.Text);
            r.carrycost = Convert.ToInt32(tb_carrycost.Text);
            r.barcode = tb_barcode.Text;
            r.quantity = Convert.ToInt32(tb_quantity.Text);
            r.type = (string)cb_Type.SelectedValue;
            r.saleactive = cbx_SaleActive.IsChecked.Value;
            r.purchaseactive = cbx_PurchaseActive.IsChecked.Value;
            RMSDBEntities db = DBContext.getInstance();
            db.product.Add(r);
            db.SaveChanges();
            Close();
            new ProductAdd().Show();
        }

        
        void initFormOperations()
        {
            var types = new string[] {"product", "complex"};
            cb_Type.ItemsSource = types;
        }
    }
}
