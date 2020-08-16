using DAL;
using RIAB_Restaurent_Management_System.bll;
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
    /// Interaction logic for saledetails.xaml
    /// </summary>
    public partial class saledetails : Window
    {
        public int saleid;
        public saledetails(int saleId)
        {
            InitializeComponent();
            saleid = saleId;
            var db = new RMSDBEntities();
            var productsinsale = db.salepurchaseproduct.Where(a => a.fk_financetransaction_salepurchaseproduct_financetransaction == saleid).ToList();
            foreach (var item in productsinsale)
            {
                dg.Items.Add(item);
            }
        }
        public void printPeceipt(object sender, RoutedEventArgs e)
        {
            saleutils.printDuplicateRecipt(saleid);
        }
    }
}
