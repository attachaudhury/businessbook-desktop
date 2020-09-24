using BusinessBook.bll;
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
using Windows.ApplicationModel.Search;

namespace BusinessBook.Views.others
{
    /// <summary>
    /// Interaction logic for ravicosoftaccount.xaml
    /// </summary>
    public partial class ravicosoftaccount : Window
    {
        softwaresettingrepo ssr = new softwaresettingrepo();
        public ravicosoftaccount()
        {
            InitializeComponent();
            if (userutils.loggedinuserd.role == "superadmin") 
            {
                apiendpoint_tb.IsEnabled = true;
            }

            if (userutils.ravicosoftusername != null)
            {
                userid_tb.Text = userutils.ravicosoftuserid.stringvalue;
            }
            if (userutils.ravicosoftusername != null)
            {
                username_tb.Text = userutils.ravicosoftusername.stringvalue;
            }
            if (userutils.ravicosoftpassword != null)
            {
                password_tb.Text = userutils.ravicosoftpassword.stringvalue;
            }
            if (userutils.ravicosoftbusinessbookmembershipplan != null)
            {
                membershiptype_tb.Text = userutils.ravicosoftbusinessbookmembershipplan.stringvalue;
            }
            if (userutils.ravicosoftbusinessbookmembershipexpirydate != null)
            {
                membershipexpiry_tb.Text = userutils.ravicosoftbusinessbookmembershipexpirydate.datevalue.ToString();
            }
            if (userutils.ravicosoftsmsplan != null)
            {
                cansendsms_tb.Text = userutils.ravicosoftsmsplan.stringvalue;
            }
            if (userutils.apiendpoint != null)
            {
                apiendpoint_tb.Text = userutils.apiendpoint.stringvalue;
            }
        }
        private void savesetting(object sender, RoutedEventArgs e)
        {

            if (username_tb.Text == "")
            {
                MessageBox.Show("Please enter username", "Information");
                return;
            }
            if (apiendpoint_tb.IsEnabled && apiendpoint_tb.Text == "")
            {
                MessageBox.Show("Please enter apiendpoint", "Information");
                return;
            }

            dynamic obj = new { username = username_tb.Text };
            networkutils.updateonlinesetting(obj);
            
            if (apiendpoint_tb.IsEnabled && apiendpoint_tb.Text != "") 
            {
                userutils.updateapiendpoint(apiendpoint_tb.Text);
            }
        }

    }
}
