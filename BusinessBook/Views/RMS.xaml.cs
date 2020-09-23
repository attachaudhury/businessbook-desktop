using System.Windows;
using BusinessBook.Views.finance;
using BusinessBook.Views.others;

using BusinessBook.Views.product;
using BusinessBook.bll;
using BusinessBook.data;
using System.Runtime.InteropServices;
using System.Linq;
using System.Collections.Generic;
using System;
using Telerik.Charting;
using Telerik.Windows.Documents.Lists;
using System.Collections.ObjectModel;
using BusinessBook.data.dapper;

namespace BusinessBook.Views
{

    [ComVisible(true)]
    public partial class RMS : Window
    {
        // data.user loggininuser;
        data.dapper.user loggininuserd;
        List<CategoricalDataPoint> collection = new List<CategoricalDataPoint>();

        public RMS()
        {
            
            InitializeComponent();
            loggininuserd = userutils.loggedinuserd;
            if (loggininuserd.role != "admin")
            {
                hideAdminMenu();
            }

            initpage();

        }
        void initpage()
        {

            initchart();
            //var db = new dbctx();
            //var sales = db.financetransaction.Where(a => a.financeaccount.name == "pos sale").Sum(a => a.amount);
            //if (sales == null)
            //{
            //    sales = 0;
            //}
            //var customers = db.user.Where(a => (a.role == "customer")).Count();
            //var vendors = db.user.Where(a => (a.role == "vendor")).Count();
            //var users = db.user.Where(a => (a.role != "customer" && a.role != "vendor")).Count();

            var userrepo = new userrepo();
            var financetransactionrepo = new financetransactionrepo();
            var sales = financetransactionrepo.gettransactionsumbyaccountname("pos sale");
            var customers = userrepo.getbywherein("role", new object[] { "customer" }).Count();
            var vendors = userrepo.getbywherein("role", new object[] { "vendor" }).Count();
            var users = userrepo.getbywherein("role", new object[] { "admin", "user" }).Count();
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
      padding: 10px 20px 10px 20px;
      border-radius: 4px; 
    }
    .blocks .title{
      margin: 0;
      font-weight: 300;
      color: #888;
    }
.blocks p{
      text-align: center;
      font-size: 55px;
    }
    p.a{
      color:rgb(98, 147, 211);
    }
    p.b{
      color:#f5584c;
    }
    p.c{
      color:#aa6edb;
    }
    p.d{
      color:#7fb856;
    }
.blocks .footer{
      margin: 0;
      font-weight: 100;
      color: #888;
      font-size:10px;
      text-align: center;
    }
    
  </style>
</head>
<body style='background-color:#f0f0f0' scroll='no'>
  <div class='main'>
    <div class='blocks'>
      <span class='title'>Sales</span>
      <p class='a'>" + sales + @"</p>
      <span class='footer'>Total Sales</span>
    </div>
    <div class='blocks'>
      <span class='title'>Customers</span>
       <p class='b'>" + customers + @"</p>
        <span class='footer'>Included All Types</span>
    </div>
    <div class='blocks'>
      <span class='title'>Vendors</span>
       <p class='c'>" + vendors + @"</p>
        <span class='footer'>Included All Types</span>
    </div>
    <div class='blocks'>
      <span class='title'>Users</span>
       <p class='d'>" + users + @"</p>
        <span class='footer'>All software users</span>
    </div>
  <div>
</body>
</html>";
            webview.NavigateToString(html);
        }

        void initchart()
        {

            chartseries.ItemsSource = new ObservableCollection<PlotInfo>
                {
                    new PlotInfo() { Day = "Monday", Total = 1002},
                    new PlotInfo() { Day = "Tuesday", Total = 3000},
                    new PlotInfo() { Day = "Wednesday", Total = 12000},
                    new PlotInfo() { Day = "Thursday", Total = 8000},
                    new PlotInfo() { Day = "Friday", Total = 9000},
                };
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
        private void accountsbalanceshow(object sender, RoutedEventArgs e)
        {
            new accountsbalance().Show();
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
        #endregion menuitem_finance

        #region others
        private void mi_Setting(object sender, RoutedEventArgs e)
        {
            new Window_Setting().Show();
        }
        private void mi_ravicosoftaccount(object sender, RoutedEventArgs e)
        {
            var w = new ravicosoftaccount();
            userutils.showauthorize(w, new string[] { "admin" });
        }
        #endregion others

        private void mi_LogOut(object sender, RoutedEventArgs e)
        {
            Close();
            new MainWindow().Show();
        }

    }
    public class PlotInfo
    {
        public string Day { get; set; }
        public double Total { get; set; }
    }

}
