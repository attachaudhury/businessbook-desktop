
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

namespace RIAB_Restaurent_Management_System.Views.finance
{
    /// <summary>
    /// Interaction logic for purchases.xaml
    /// </summary>
    public partial class purchases : Window
    {
        List<data.dapper.financeaccount> financeaccounts = null;
        public purchases()
        {
            InitializeComponent();
            var financeaccountrepo = new data.dapper.financeaccountrepo();
            var financetransactionrepo = new data.dapper.financetransactionrepo();
            //var financetransactions = financetransactionrepo.get();
            financeaccounts = financeaccountrepo.get();
            //var db = new dbctx();
            //var list = db.financetransaction.Where(a => (a.financeaccount.name == "inventory") &&(a.name== "--inventory--on--purchase--")).ToList();
            var list = financetransactionrepo.getmanybyselfnameandfinanceaccountname("--inventory--on--purchase--", "inventory");
            foreach (var item in list)
            {
                dg.Items.Add(item);
            }
        }
        public void details(object sender, RoutedEventArgs e)
        {
            data.dapper.financetransaction obj = ((FrameworkElement)sender).DataContext as data.dapper.financetransaction;
            new purchasedetails(obj.id).Show();
        }
    }
}
