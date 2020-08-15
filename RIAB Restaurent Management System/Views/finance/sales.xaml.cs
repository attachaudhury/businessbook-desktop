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
    /// Interaction logic for sales.xaml
    /// </summary>
    public partial class sales : Window
    {
        public sales()
        {
            InitializeComponent();
            var db = new RMSDBEntities();
            var list = db.financetransaction.Where(a => a.financeaccount.name == "pos sale").ToList();
            foreach (var item in list)
            {
                dg.Items.Add(item);
            }
        }
        public void details(object sender, RoutedEventArgs e)
        {
            financetransaction obj = ((FrameworkElement)sender).DataContext as financetransaction;
            new saledetails(obj.id).Show();
        }
    }
}
