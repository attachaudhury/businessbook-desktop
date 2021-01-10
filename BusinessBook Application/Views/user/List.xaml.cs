
using BusinessBook.data;
using BusinessBook.data.dapper;
using BusinessBook.Views.finance;
using System;
using System.Collections.Generic;
using System.Dynamic;
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

namespace BusinessBook.Views.user
{
    /// <summary>
    /// Interaction logic for List.xaml
    /// </summary>
    public partial class List : Window
    {
        public List(string roletype)
        {
            InitializeComponent();
            
            var userrepo = new userrepo();
            ////var db = new dbctx();
            if (roletype == "staff")
            {
                //dg_AllStaff.ItemsSource = db.user.Where(a => (a.role == "admin" || a.role == "user")).ToList();
                var roles = new object[] { "admin","user" };
                dg_AllStaff.ItemsSource = userrepo.getbywherein("role",roles);
            }
            else if (roletype == "customer")
            {
                //dg_AllStaff.ItemsSource = db.user.Where(a => a.role == "customer").ToList();
                var roles = new object[] { "customer" };
                dg_AllStaff.ItemsSource = userrepo.getbywherein("role", roles);
            }
            else
            {
                //dg_AllStaff.ItemsSource = db.user.Where(a => a.role == "vendor").ToList();
                var roles = new object[] { "vendor" };
                dg_AllStaff.ItemsSource = userrepo.getbywherein("role", roles);
            }
        }
        public void details(object sender, RoutedEventArgs e)
        {
            data.dapper.user obj = ((FrameworkElement)sender).DataContext as data.dapper.user;
            new ledger(obj.id).Show();
        }
    }
}
