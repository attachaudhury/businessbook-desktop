using RIAB_Restaurent_Management_System.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            //foreach (tbl_Staff item in Staff.getAllDeliveryStaff())
            //{
            //    cb_DeliveryBoy.ItemsSource = Staff.getAllDeliveryStaff();
            //    cb_DeliveryBoy.DisplayMemberPath = "Name";
            //    cb_DeliveryBoy.SelectedValuePath = "Id";
            //}
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
            var db = new dbctx();
            user c = new user();
            c.phone = tb_Phone.Text;
            c.role = "customer";
            if (tb_Name.Text != "")
            {
                c.name = tb_Name.Text;
            }
            if (tb_Address.Text == "")
            {
                MessageBox.Show("Please Enter Address", "Error");
            }
            else
            {
                c.address = tb_Address.Text;
            }
            db.user.Add(c);
            db.SaveChanges();
            Customer_Id = c.id;
            DialogResult = true;
        }

        private void tb_Phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            var db = new dbctx();
            if (tb_Phone.Text != null)
            {
                try
                {
                    user customer = db.user.Where(a=>a.phone==tb_Phone.Text).FirstOrDefault();
                    if (customer == null)
                    {
                        return;
                    }
                    else
                    {
                        tb_Name.Text = customer.name;
                        tb_Address.Text = customer.address;
                        Customer_Id = customer.id;
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
