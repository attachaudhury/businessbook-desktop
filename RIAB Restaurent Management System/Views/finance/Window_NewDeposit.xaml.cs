using BLL.DBOperations;
using DAL;
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
    /// Interaction logic for Window_NewDeposit.xaml
    /// </summary>
    public partial class Window_NewDeposit : Window
    {
        public Window_NewDeposit()
        {
            InitializeComponent();
            
            foreach(tbl_DepositHead head in DepositHead.getAll())
            {
                cb_Head.ItemsSource = DepositHead.getAll();
                cb_Head.SelectedValuePath = "Id";
                cb_Head.DisplayMemberPath = "Name";
            }
        }

        private void btn_Save(object sender, RoutedEventArgs e)
        {
            if (tb_Ammount.Text != null && cb_Head.SelectedValue != null)
            {
                tbl_Deposit deposit = new tbl_Deposit();
                if (tb_Comment.Text != null)
                {
                    deposit.Comment = tb_Comment.Text;
                }
                deposit.Ammount = Convert.ToDouble(tb_Ammount.Text);
                deposit.DatenTime = DateTime.Now;
                deposit.DepositHead_Id = (int)cb_Head.SelectedValue;
                Deposit.insert(deposit);
                Close();
            }

        }
    }
}
