
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
    public partial class inventoryreport : Window
    {
        int productid;
        public inventoryreport(int productid)
        {
            this.productid = productid;
            InitializeComponent();
            initFormOperations();
        }
        private void initFormOperations()
        {
            var report = new inventoryreportrepo().get(this.productid);
            dg.ItemsSource = null;
            dg.ItemsSource = report;
            UpdateLayout();
        }
        private void search(object sender, RoutedEventArgs e)
        {
            var report = new inventoryreportrepo().get(this.productid);
            dg.ItemsSource = null;
            dg.ItemsSource = report;
        }

    }
}
