
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
    /// Interaction logic for purchasedetails.xaml
    /// </summary>
    public partial class purchasedetails : Window
    {
        List<data.dapper.financeaccount> financeaccounts = null;


        public purchasedetails(int purchaseid)
        {
            InitializeComponent();
            var financeaccountrepo = new data.dapper.financeaccountrepo();
            var financetransactionrepo = new data.dapper.financetransactionrepo();
            var salepurchaseproductrepo = new data.dapper.salepurchaseproductrepo();
            //var financetransactions = financetransactionrepo.get();
            //var db = new dbctx();
            //var productsinsale = db.salepurchaseproduct.Where(a => a.fk_financetransaction_salepurchaseproduct_financetransaction == purchaseid).ToList();
            var productsinsale = salepurchaseproductrepo.getmultiplebytransactionid(purchaseid);

            foreach (var item in productsinsale)
            {
                dg.Items.Add(item);
            }
        }
    }
}
