using System.Windows;
using RIAB_Restaurent_Management_System.Views.finance;
using RIAB_Restaurent_Management_System.Views.others;
using DAL;
using RIAB_Restaurent_Management_System.Views.product;
using RIAB_Restaurent_Management_System.bll;

namespace RIAB_Restaurent_Management_System.Views
{
    public partial class RMS : Window
    {
        user loggininuser;

        public RMS()
        {
            
            InitializeComponent();
            loggininuser= userutils.loggedinuser;
            if (loggininuser.role != "admin")
            {
                hideAdminMenu();
            }
        }
        private void hideAdminMenu() {
            m_Staff.Visibility = Visibility.Collapsed;
        }

        
        
        #region customer
        private void mi_ViewAllCustomers(object sender, RoutedEventArgs e)
        {
            new person.List("customer").Show();
        }
        private void mi_AddNewCustomer(object sender, RoutedEventArgs e)
        {
            new person.Add("customer").Show();
        }
        #endregion customer

        
        #region staff
        private void mi_AddStaff(object sender, RoutedEventArgs e)
        {
            //new Window_AddNewStaff("staff").Show();
            new person.Add("staff").Show();
        }

        private void mi_AllStaff(object sender, RoutedEventArgs e)
        {
            //new Window_ViewAllStaff().Show();
            new person.List("staff").Show();
        }
        #endregion staff

        #region menuitem_products
        private void productadd(object sender, RoutedEventArgs e)
        {
            new ProductAdd().Show();
        }
        private void products(object sender, RoutedEventArgs e)
        {
            new ProductList().Show();
        }
        #endregion menuitem_products

        #region menuitem_finance
        private void pos(object sender, RoutedEventArgs e)
        {
            new pos().Show();
        }
        private void transactionsshow(object sender, RoutedEventArgs e)
        {
            new transactions().Show();
        }
        private void salesshow(object sender, RoutedEventArgs e)
        {
            new Views.finance.sales().Show();
        }
        private void purchasenewshow(object sender, RoutedEventArgs e)
        {
            new Views.finance.purchasenew().Show();
        }
        private void purchasesshow(object sender, RoutedEventArgs e)
        {
            new Views.finance.purchases().Show();
        }
        private void expencenewshow(object sender, RoutedEventArgs e)
        {
            new Views.finance.expencenew().Show();
        }
        private void expencesshow(object sender, RoutedEventArgs e)
        {
            new Views.finance.expences().Show();
        }
        private void mi_DoClosing(object sender, RoutedEventArgs e)
        {
            new Form_DoClosing().Show();
        }

        #endregion menuitem_finance

        #region others
        private void mi_Setting(object sender, RoutedEventArgs e)
        {
            new Window_Setting().Show();
        }
        #endregion others

        private void mi_LogOut(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

    }
}
