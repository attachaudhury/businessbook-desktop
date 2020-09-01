using RIAB_Restaurent_Management_System.bll;
using RIAB_Restaurent_Management_System.data;
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

namespace RIAB_Restaurent_Management_System.Views.finance
{
    /// <summary>
    /// Interaction logic for ledger.xaml
    /// </summary>
    public partial class ledger : Window
    {
        List<financeaccount> financeaccounts = null;
        data.user user;
        public ledger(int userid)
        {
            InitializeComponent();
            
            var db = new dbctx();
            financeaccounts = db.financeaccount.ToList();
            user = db.user.Find(userid);
            if (user.role == "customer") 
            {
                dg_Title.Content = "Sales";
                var list = db.financetransaction.Where(a => (a.fk_targettouser_user_financetransaction==userid)&&((a.financeaccount.name == "pos sale" || a.financeaccount.name == "sale"))).ToList();
                foreach (var item in list)
                {
                    dg.Items.Add(item);
                }
                var totalpending = db.financetransaction.Where(a => (a.fk_targettouser_user_financetransaction == userid)&&(a.financeaccount.name == "account receivable")).Sum(a => a.amount);
                remaining_label.Content = totalpending;
            }
            else if (user.role == "vendor") 
            {
                dg_Title.Content = "Purchases";
                var list = db.financetransaction.Where(a => (a.fk_targettouser_user_financetransaction == userid)  && (a.financeaccount.name == "inventory") && (a.name == "--inventory--on--purchase--")).ToList();
                foreach (var item in list)
                {
                    dg.Items.Add(item);
                }
                var totalpending = db.financetransaction.Where(a => (a.fk_targettouser_user_financetransaction == userid) && (a.financeaccount.name == "account payable")).Sum(a => a.amount);
                remaining_label.Content = totalpending;
            }
            var assetaccounts = db.financeaccount.Where(a => a.type == "asset").ToList();
            account_combobox.ItemsSource = assetaccounts;
            account_combobox.DisplayMemberPath = "name";
            account_combobox.SelectedValuePath = "id";
        }

        private void save(object sender, RoutedEventArgs e) 
        {
            if (account_combobox.SelectedItem == null) 
            {
                MessageBox.Show("Please select account");
            }
            if (tb_amount.Text == "")
            {
                MessageBox.Show("Please enter amount");
            }

            var amount = Convert.ToDouble(tb_amount.Text);
            var account = (int)account_combobox.SelectedValue;
            if (user.role == "customer")
            {
                financeutils.insertCustomerPayment(account,amount,user.id);
            }
            else if (user.role == "vendor") 
            {
                financeutils.insertVendorPayment(account, amount, user.id);
            }
            MessageBox.Show("Operation Successfull");
            Close();
            new ledger(user.id).Show();
            
        }
    }

}
