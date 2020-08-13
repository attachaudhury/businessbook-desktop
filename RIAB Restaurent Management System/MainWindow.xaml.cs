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
using RIAB_Restaurent_Management_System.Views;
using System.Globalization;
using DAL;
using BLL;
using BLL.DBOperations;
using RIAB_Restaurent_Management_System.bll;

namespace RIAB_Restaurent_Management_System
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tb_Name.Focus();
        }

        private void btn_Login(object sender, RoutedEventArgs e)
        {
            login();
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
        void login() 
        {
            //int nextMonthInt = Convert.ToInt32(DateTime.Now.ToString("MM")) + 1;
            //string nextMonthString = Convert.ToString(nextMonthInt);
            //char[] monthNameArray = DateTime.Now.ToString("MMM", CultureInfo.InvariantCulture).ToCharArray();
            //char secondCharacterofMonth = monthNameArray[1];
            //string password = nextMonthString + secondCharacterofMonth;



            RMSDBEntities db = new RMSDBEntities();
            user user = db.user.Where(a => (a.username == tb_Name.Text && a.password == tb_Pasword.Password)).FirstOrDefault();
            if (user!=null) {
            userutils.loggedinuser = user;
                new RMS().Show();
                Close();
            }
            else
            {
                    BLL.AutoClosingMessageBox.Show("Username or password not exists", "Failed", 3000);

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
    }
}
