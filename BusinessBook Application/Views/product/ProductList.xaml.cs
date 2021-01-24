
using BusinessBook.data;
using BusinessBook.data.dapper;
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

namespace BusinessBook.Views.product
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
            // var db = dbctxsinglton.getInstance();
            var productrepo = new productrepo().get();
            dg_ProductList.ItemsSource = null;
            dg_ProductList.ItemsSource = productrepo;
            UpdateLayout();
        }
        public void edit(object sender, RoutedEventArgs e)
        {
            data.dapper.product obj = ((FrameworkElement)sender).DataContext as data.dapper.product;
            new ProductAdd(obj.id).Show();
        }
    }
}
