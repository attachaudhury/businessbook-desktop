﻿
using BusinessBook.data;
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

namespace BusinessBook.Views.finance
{
    /// <summary>
    /// Interaction logic for sales.xaml
    /// </summary>
    public partial class salespurchases : Window
    {
        List<data.dapper.financeaccount> financeaccounts = null;

        string listtype;
        public salespurchases(string type)
        {
            InitializeComponent();
            listtype = type;
            var financeaccountrepo = new data.dapper.financeaccountrepo();
            var financetransactionrepo = new data.dapper.financetransactionrepo();
                financeaccounts = financeaccountrepo.get();
            
            List<financetransactionextended> list = new List<financetransactionextended>();
            if (type == "sale") 
            {
                list = financetransactionrepo.getmanybymanyfinanceaccountnames(new string[]{ "pos sale","sale","service sale"});
            }
            else if(type == "purchase")
            {
                list = financetransactionrepo.getmanybyselfnameandfinanceaccountname("--inventory--on--purchase--", "inventory");
            }
            foreach (var item in list)
            {
                dg.Items.Add(item);
            }
        }
        public void details(object sender, RoutedEventArgs e)
        {
            data.dapper.financetransaction obj = ((FrameworkElement)sender).DataContext as data.dapper.financetransaction;
            new salepurchasedetails(obj.id, listtype).Show();
        }
    }
}
