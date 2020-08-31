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
    public partial class DialogSelectUser : Window
    {
        string roletype = null;
        public data.user seleteduser { get; set; }
        List<data.user> allusers = null;
        
        public DialogSelectUser(string roletype)
        {
            InitializeComponent();
            tb_Phone.Focus();
            this.roletype = roletype;
            var db = new dbctx();
            if (roletype == "staff")
            {
                allusers = db.user.Where(a => (a.role == "admin" || a.role == "user")).ToList();
            }
            else if (roletype == "customer")
            {
                allusers = db.user.Where(a => a.role == "customer").ToList();
            }
            else
            {
                allusers = db.user.Where(a => a.role == "vendor").ToList();
            }
            dg.ItemsSource = allusers;
        }

        private void tb_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var db = new dbctx();
            if (tb_Search.Text != null)
            {
                try
                {
                    dg.ItemsSource = db.user.Where(a => (a.role == roletype) && (a.phone.Contains(tb_Search.Text))).ToList();
                }
                catch { }
            }
        }

        public void select(object sender, RoutedEventArgs e)
        {
            data.user obj = ((FrameworkElement)sender).DataContext as data.user;
            this.seleteduser = obj;
            DialogResult = true;
        }
        public void cancel(object sender, RoutedEventArgs e)
        {
            this.seleteduser = null;
            DialogResult = true;
        }


        private void SaveAndSelect(object sender, System.Windows.RoutedEventArgs e)
        {
            var db = new dbctx();
            data.user c = new data.user();
            c.phone = tb_Phone.Text;
            c.role = roletype;
            c.name = tb_Name.Text;
            c.address = tb_Address.Text;
            db.user.Add(c);
            db.SaveChanges();
            this.seleteduser = c;
            DialogResult = true;
        }

        

    }
}
