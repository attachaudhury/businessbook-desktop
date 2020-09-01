using System.Windows;
using RIAB_Restaurent_Management_System.Views.finance;
using RIAB_Restaurent_Management_System.Views.others;

using RIAB_Restaurent_Management_System.Views.product;
using RIAB_Restaurent_Management_System.bll;
using RIAB_Restaurent_Management_System.data;
using System.Runtime.InteropServices;
using System.Linq;

namespace RIAB_Restaurent_Management_System.Views
{

    [ComVisible(true)]
    public partial class RMS : Window
    {
        data.user loggininuser;

        public RMS()
        {

            InitializeComponent();
            loggininuser = userutils.loggedinuser;
            if (loggininuser.role != "admin")
            {
                hideAdminMenu();
            }

            initpage();

        }
        void initpage()
        {

            var db = new dbctx();
            var sales = db.financetransaction.Where(a => a.financeaccount.name == "pos sale").Sum(a => a.amount);
            if (sales == null)
            {
                sales = 0;
            }
            var customers = db.user.Where(a => (a.role == "customer")).Count();
            var vendors = db.user.Where(a => (a.role == "vendor")).Count();
            var users = db.user.Where(a => (a.role != "customer" && a.role != "vendor")).Count();

            string html = @"<html>
<head>
  <style>
html{overflow:hidden;height:200px;}
    .main{
      font-family: arial;
    }
    .blocks{
      float: left;
      width: 20%;
      margin: 1%;
      border: 1px solid #ddd;
      padding: 10px 20px;
      border-radius: 4px; 
    }
    .blocks h4{
      margin: 0;
      font-weight: 600;
      color: #888;
    }
    .blocks p{
      text-align: center;
      font-size: 45px;
    }
    p.a{
      color:rgb(98, 147, 211);
    }
    p.b{
      color:red;
    }
    p.c{
      color:#8418e6;
    }
    p.d{
      color:green;
    }
  </style>
</head>
<body style='background-color:#f0f0f0' scroll='no'>
  <div class='main'>
    <div class='blocks'>
      <h4>Sales</h4>
      <p class='a'>" + sales + @"</p>
    </div>
    <div class='blocks'>
      <h4>Customers</h4>
       <p class='b'>" + customers + @"</p>
    </div>
    <div class='blocks'>
      <h4>Vendors</h4>
       <p class='c'>" + vendors + @"</p>
    </div>
    <div class='blocks'>
      <h4>Users</h4>
       <p class='d'>" + users + @"</p>
    </div>
  <div>
</body>
</html>";
            webview.NavigateToString(html);
        }
        private void hideAdminMenu()
        {
            m_Staff.Visibility = Visibility.Collapsed;
        }



        #region customer
        private void mi_ViewAllCustomers(object sender, RoutedEventArgs e)
        {
            new user.List("customer").Show();
        }
        private void mi_AddNewCustomer(object sender, RoutedEventArgs e)
        {
            new user.Add("customer").Show();
        }
        #endregion customer

        #region customer
        private void mi_ViewAllVendors(object sender, RoutedEventArgs e)
        {
            new user.List("vendor").Show();
        }
        private void mi_AddNewVendor(object sender, RoutedEventArgs e)
        {
            new user.Add("vendor").Show();
        }
        #endregion customer


        #region staff
        private void mi_AddStaff(object sender, RoutedEventArgs e)
        {
            //new Window_AddNewStaff("staff").Show();
            new user.Add("staff").Show();
        }

        private void mi_AllStaff(object sender, RoutedEventArgs e)
        {
            //new Window_ViewAllStaff().Show();
            new user.List("staff").Show();
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
        private void accountsshow(object sender, RoutedEventArgs e)
        {
            new accounts().Show();
        }
        private void pos(object sender, RoutedEventArgs e)
        {
            new pos().Show();
        }
        private void salenewshow(object sender, RoutedEventArgs e)
        {
            new salenew().Show();
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
