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
            //int nextMonthInt = Convert.ToInt32(DateTime.Now.ToString("MM")) + 1;
            //string nextMonthString = Convert.ToString(nextMonthInt);
            //char[] monthNameArray = DateTime.Now.ToString("MMM", CultureInfo.InvariantCulture).ToCharArray();
            //char secondCharacterofMonth = monthNameArray[1];
            //string password = nextMonthString + secondCharacterofMonth;



            //var db = new dbctx();
            //data.user user = db.user.Where(a => (a.username == tb_Name.Text && a.password == tb_Pasword.Password)).FirstOrDefault();
            data.dapper.user userd = new data.dapper.userrepo().get(tb_Name.Text, tb_Pasword.Password);
            int i = 0;
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

            //if (tb_Name.Text == "admin")
            //{
            //    DateTime licenceDate = new DateTime(2022, 3, 1);
            //    if (tb_Pasword.Password == MyPrinterSetting.Pass)
            //    {
            //        //new RMS().Show();
            //        //Close();
            //        if (DateTime.Now < licenceDate)
            //        {
            //            new RMS().Show();
            //            Close();
            //        }
            //        else
            //        {
            //            BLL.AutoClosingMessageBox.Show("Please renew Licence", "Failed", 3000);
            //        }

            //    } else if (tb_Pasword.Password == "adminmasterpassword")
            //    {
            //        //new RMS().Show();
            //        //Close();
            //        if (DateTime.Now < licenceDate)
            //        {
            //            new RMS().Show();
            //            Close();
            //        }
            //        else
            //        {
            //            BLL.AutoClosingMessageBox.Show("Please renew Licence", "Failed", 3000);
            //        }
            //    }
            //}
            //else if (tb_Name.Text == "user")
            //{
            //    if (tb_Pasword.Password == "12345")
            //    {
            //        new User().Show();
            //        Close();
            //    }
            //}
            //else
            //{
            //    BLL.AutoClosingMessageBox.Show("Wrong User Name and Password", "Failed", 3000);
            //}
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
                softwaresetting softwareshouldrun = userutils.canrunsoftware;
                if (softwareshouldrun != null)
                {
                    if ((Boolean)softwareshouldrun.boolvalue)
                    {
                        return true;
                    }
                    else
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
            softwaresetting membershiptype = userutils.membershiptype;
            if (membershiptype == null || membershiptype.stringvalue == "free")
            {
                userutils.membership = "free";
            }
            else if (membershiptype.stringvalue == "paid")
            {
                var validationresult = validatepaidmembership();
                if (validationresult)
                {
                    userutils.membership = "paid";
                }
                return;
            }
        }
        private Boolean validatepaidmembership()
        {
            try
            {
                softwaresetting membershipexpirydate = userutils.membershipexpirydate;
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
