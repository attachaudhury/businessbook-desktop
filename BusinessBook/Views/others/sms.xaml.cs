using BusinessBook.bll;
using BusinessBook.data.dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace BusinessBook.Views.others
{
    /// <summary>
    /// Interaction logic for sms.xaml
    /// </summary>
    public partial class sms : Window
    {
        userrepo ur = new userrepo();
        List<data.dapper.user> users;
        public sms()
        {
            InitializeComponent();
            this.users = ur.get();
            dg.ItemsSource = users;
        }

        private void sendsms(object sender, RoutedEventArgs e)
        {
            networkutils.sendsms(text_tb.Text, dg.SelectedItems);
        }
    }
}
