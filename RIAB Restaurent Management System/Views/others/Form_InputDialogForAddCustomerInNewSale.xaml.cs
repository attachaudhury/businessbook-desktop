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
using DAL;
using BLL;
using BLL.DBOperations;

namespace RIAB_Restaurent_Management_System.Views
{
    /// <summary>
    /// Interaction logic for Form_InputDialogForAddCustomerInNewSale.xaml
    /// </summary>
    public partial class Form_InputDialogForAddCustomerInNewSale : Window
    {
        public Form_InputDialogForAddCustomerInNewSale()
        {
            InitializeComponent();
            foreach (tbl_Staff item in Staff.getAllDeliveryStaff())
            {
                cb_DeliveryBoy.ItemsSource = Staff.getAllDeliveryStaff();
                cb_DeliveryBoy.DisplayMemberPath = "Name";
                cb_DeliveryBoy.SelectedValuePath = "Id";
            }
            tb_Phone.Focus();
        }
        public string ResponseName
        {
            get { return tb_Name.Text; }
            set { tb_Name.Text = value; }
        }
        public string ResponsePhone
        {
            get { return tb_Phone.Text; }
            set { tb_Phone.Text = value; }
        }
        public string ResponseAddress
        {
            get { return tb_Address.Text; }
            set { tb_Address.Text = value; }
        }
        public int? DeliveryBoy_Id
        {
            get { return (int?)cb_DeliveryBoy.SelectedValue; }
        }
        public int Customer_Id = 0;

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            tbl_Customer c = new tbl_Customer();
            c.PhoneNo = tb_Phone.Text;
            if (tb_Name.Text != "")
            {
                c.Name = tb_Name.Text;
            }
            if (tb_Address.Text == "")
            {
                AutoClosingMessageBox.Show("Please Enter Address", "Error", 2000);
            }
            else
            {
                c.Address = tb_Address.Text;
            }
            Customer.insert(c);
            Customer_Id = c.Id;
            DialogResult = true;
        }

        private void tb_Phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Phone.Text != null)
            {
                try
                {
                    tbl_Customer customer = Customer.getByPhoneNumber(tb_Phone.Text);
                    if (customer == null)
                    {
                        return;
                    }
                    else
                    {
                        tb_Name.Text = customer.Name;                        
                        tb_Address.Text = customer.Address;
                        Customer_Id = customer.Id;
                    }
                } catch { }
            }
        }

        private void tb_Phone_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DialogResult = true;
            }

           
        }

        private void tb_Address_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DialogResult = true;
            }
        }
    }
}
