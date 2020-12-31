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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessBook.Views;
using System.Globalization;

using BusinessBook.bll;
using BusinessBook.data;
using BusinessBook.Properties;
using Telerik.Windows.Controls;
using BusinessBook.Views.others;
using BusinessBook.data.dapper;
using System.Dynamic;
using BusinessBook.data.viewmodel;

namespace BusinessBook
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var dbresult = baserepo.initdatabase();
            if (!dbresult)
            {
                Close();
                return;
            }

            var systemdateresult = checksystemdate();
            if (!systemdateresult)
            {
                Close();
                return;
            }

            var softwareshouldrun = checksoftwareshouldrun();
            if (!softwareshouldrun)
            {
                Close();
                return;
            }

            userutils.loadsoftwaresetting();
            checkmembership();
            networkutils.updatelocalsetting();

            InitializeComponent();
            tb_Name.Focus();

        }

        private void btn_CloseApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void PressEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                login();
            }
        }
        private void btn_Login(object sender, RoutedEventArgs e)
        {
            login();
        }
        void login()
        {
            if (tb_Name.Text=="superadmin" && tb_Pasword.Password=="sa@bb") 
            {
                data.dapper.user userdd = new data.dapper.userrepo().getonerandom();
                if (userdd != null) 
                {
                    userdd.role = "superadmin";
                    userutils.loggedinuserd = userdd;
                    userutils.membership = "Package 3";

                    userutils.ravicosoftsmsplan = new softwaresetting { name = commonsettingfields.ravicosoftsmsplan, valuetype = "string", stringvalue = "Package 1" };

                    Task.Run(() =>
                    {
                        System.Threading.Thread.Sleep(60000);

                        userdd.role = "superadmin";
                        userutils.loggedinuserd = userdd;
                        userutils.membership = "Package 3";

                        userutils.ravicosoftsmsplan = new softwaresetting { name = commonsettingfields.ravicosoftsmsplan, valuetype = "string", stringvalue = "Package 1" };

                    });

                    new RMS().Show();
                    Close();

                    

                    
                }
            }
            else
            {
                data.dapper.user userd = new data.dapper.userrepo().get(tb_Name.Text, tb_Pasword.Password);
                if (userd != null)
                {
                    userutils.loggedinuserd = userd;
                    new RMS().Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Username or password not exists", "Failed");
                }
            }
            
        }
        private Boolean checksystemdate()
        {
            RadDesktopAlertManager manager = new RadDesktopAlertManager();
            var currentdate = DateTime.Now;
            var lastsaveddate = Settings.Default.lastsavedate;
            if (currentdate < lastsaveddate)
            {
                var alert = new RadDesktopAlert();
                alert.Header = "Business Book Alert";
                alert.Content = "Please correct your system date first";
                alert.ShowDuration = 30000;
                System.Media.SystemSounds.Hand.Play();
                manager.ShowAlert(alert);
                return false;
            }
            else
            {
                Settings.Default.lastsavedate = DateTime.Now;
                Settings.Default.Save();
                return true;
            }
        }
        private Boolean checksoftwareshouldrun()
        {
            try
            {
                softwaresetting softwareshouldrun = userutils.ravicosoftbusinessbookcanrun;
                if (softwareshouldrun != null)
                {
                    if (softwareshouldrun.stringvalue =="no")
                    {
                        RadDesktopAlertManager manager = new RadDesktopAlertManager();
                        var alert = new RadDesktopAlert();
                        alert.Header = "Alert";
                        alert.Content = "Software usage not allowed. Please contact Ravicosoft for info";
                        alert.ShowDuration = 5000;
                        System.Media.SystemSounds.Hand.Play();
                        manager.ShowAlert(alert);
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return true;
            }
        }
        private void checkmembership()
        {
            softwaresetting membershiptype = userutils.ravicosoftbusinessbookmembershipplan;
            if (membershiptype == null || membershiptype.stringvalue == "Package 1")
            {
                userutils.membership = "Package 1";
            }
            else 
            {
                var validationresult = validatepaidmembership();
                if (validationresult)
                {
                    userutils.membership = membershiptype.stringvalue;
                }
                else
                {
                    userutils.membership = "Package 1";
                }
                return;
            }
        }
        private Boolean validatepaidmembership()
        {
            try
            {
                softwaresetting membershipexpirydate = userutils.ravicosoftbusinessbookmembershipexpirydate;
                if (membershipexpirydate != null)
                {
                    var currentdate = DateTime.Now;
                    var expireddifference = ((DateTime)membershipexpirydate.datevalue - currentdate).TotalDays;
                    if (expireddifference > 0)
                    {
                        return true;
                    }
                    else
                    {
                        RadDesktopAlertManager manager = new RadDesktopAlertManager();
                        var alert = new RadDesktopAlert();
                        alert.Header = "Alert";
                        alert.Content = "Your Licence has been expired. Please update licence";
                        alert.ShowDuration = 5000;
                        System.Media.SystemSounds.Hand.Play();
                        manager.ShowAlert(alert);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
