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

            if (userutils.ravicosoftusername != null)
            {
                userid_tb.Text = userutils.ravicosoftuserid.stringvalue;
            }
            if (userutils.ravicosoftusername != null)
            {
                username_tb.Text = userutils.ravicosoftusername.stringvalue;
            }
            if (userutils.ravicosoftuserpassword != null)
            {
                password_tb.Text = userutils.ravicosoftuserpassword.stringvalue;
            }
            if (userutils.membershiptype != null)
            {
                membershiptype_tb.Text = userutils.membershiptype.stringvalue;
            }
            if (userutils.membershipexpirydate != null)
            {
                membershipexpiry_tb.Text = userutils.membershipexpirydate.datevalue.ToString();
            }
            if (userutils.cansendsms != null)
            {
                cansendsms_tb.Text = userutils.cansendsms.boolvalue.ToString();
            }
        }
        private void btn_Save(object sender, RoutedEventArgs e)
        {

            if (username_tb.Text == "")
            {
                MessageBox.Show("Please fill form", "Information");
                return;
            }
        }

    }
}
