
using BusinessBook.data;
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

namespace BusinessBook.Views.finance
{
    /// <summary>
    /// Interaction logic for sales.xaml
    /// </summary>
    public partial class sales : Window
    {
        List<data.dapper.financeaccount> financeaccounts = null;

        public sales()
        {
            InitializeComponent();
            //var db = new dbctx();
            var financeaccountrepo = new data.dapper.financeaccountrepo();
            var financetransactionrepo = new data.dapper.financetransactionrepo();
            //var financetransactions = financetransactionrepo.get();
            financeaccounts = financeaccountrepo.get();
            //var list = db.financetransaction.Where(a => a.financeaccount.name == "pos sale").ToList();
            var list = financetransactionrepo.getmanybyfinanceaccountname("pos sale");
            foreach (var item in list)
            {
                dg.Items.Add(item);
            }
        }
        public void details(object sender, RoutedEventArgs e)
        {
            data.dapper.financetransaction obj = ((FrameworkElement)sender).DataContext as data.dapper.financetransaction;
            new saledetails(obj.id).Show();
        }
    }
}
