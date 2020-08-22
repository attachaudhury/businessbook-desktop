using DAL;
using RIAB_Restaurent_Management_System.data;
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

namespace RIAB_Restaurent_Management_System.Views.product
{
    /// <summary>
    /// Interaction logic for ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        public ProductList()
        {
            InitializeComponent();
            initFormOperations();
        }
        private void initFormOperations()
        {
            RMSDBEntities db = DBContext.getInstance();
            dg_ProductList.ItemsSource = null;
            dg_ProductList.ItemsSource = db.product.ToList();
            UpdateLayout();
        }
        public void details(object sender, RoutedEventArgs e)
        {
            DAL.product obj = ((FrameworkElement)sender).DataContext as DAL.product;
            new ProductAdd(obj.id).Show();
        }
    }
}
