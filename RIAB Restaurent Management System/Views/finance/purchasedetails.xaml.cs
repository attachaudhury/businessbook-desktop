using DAL;
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
    /// Interaction logic for purchasedetails.xaml
    /// </summary>
    public partial class purchasedetails : Window
    {
        public purchasedetails(int purchaseid)
        {
            InitializeComponent();
            var db = new RMSDBEntities();
            var productsinsale = db.salepurchaseproduct.Where(a => a.fk_financetransaction_salepurchaseproduct_financetransaction == purchaseid).ToList();
            foreach (var item in productsinsale)
            {
                dg.Items.Add(item);
            }
        }
    }
}
