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
using BLL.DBOperations.TmpModels;


namespace RIAB_Restaurent_Management_System.Views.finance
{
    public partial class Form_ExpenceHeads : Window
    {
        public Form_ExpenceHeads()
        {
            InitializeComponent();
            initFormOperations();
        }
        void initFormOperations()
        {
            tb_ExpenceHead.Text = "";
            tb_ExpenceSubHead.Text = "";
            dg_AllExpenceHeads.Items.Clear();
            dg_AllExpenceSubHeads.Items.Clear();
            foreach(tbl_ExpenceHead eh in ExpenceHead.getAll())
            {
                dg_AllExpenceHeads.Items.Add(eh);
            }
          
            foreach (SubHeadHeadNameModel mm in ExpenceSubHead.getAllMappedToSubHeadHeadNameModel())
            {
                dg_AllExpenceSubHeads.Items.Add(mm);
            }
            foreach(tbl_ExpenceHead item in ExpenceHead.getAll())
            {
                cb_AllExpenceHeads.ItemsSource = ExpenceHead.getAll();
                cb_AllExpenceHeads.DisplayMemberPath = "Name";
                cb_AllExpenceHeads.SelectedValuePath = "Id";
            }
        }
        private void btn_DeleteExpenceHead(object sender, RoutedEventArgs e)
        {
            if (dg_AllExpenceHeads.SelectedItem != null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Your step will delete all Sub-Heads,Expences related to this Head.", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    tbl_ExpenceHead eh = (tbl_ExpenceHead)dg_AllExpenceHeads.SelectedItem;
                    ExpenceHead.delete(eh);
                    AutoClosingMessageBox.Show("Expence Head Deleted", "Deleted", 3000);
                    initFormOperations();
                }
            }
            else { AutoClosingMessageBox.Show("Select A Item", "Alert", 3000); }
        }
        private void btn_UpdateExpenceHead(object sender, RoutedEventArgs e)
        {
            if (dg_AllExpenceHeads.SelectedItem != null)
            {
                    tbl_ExpenceHead eh = (tbl_ExpenceHead)dg_AllExpenceHeads.SelectedItem;
                    var dialog = new Form_InputDialog(eh.Name);
                    if (dialog.ShowDialog() == true) 
                    {
                        eh.Name = dialog.ResponseText;
                        ExpenceHead.update(eh);
                        AutoClosingMessageBox.Show("Expence Head Updated", "Success", 3000);
                        initFormOperations();
                    }
                    
            }
            else { AutoClosingMessageBox.Show("Select A Item", "Alert", 3000); }

        }
        private void btn_DeleteExpenceSubHead(object sender, RoutedEventArgs e)
        {
            if (dg_AllExpenceSubHeads.SelectedItem != null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Your step will delete all Expences Related to this Sub-Head.", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    SubHeadHeadNameModel mm = (SubHeadHeadNameModel)dg_AllExpenceSubHeads.SelectedItem;
                    tbl_ExpenceSubHead esh =  ExpenceSubHead.getById(mm.Id);
                    ExpenceSubHead.delete(esh);
                    AutoClosingMessageBox.Show("Expence Sub-Head Deleted", "Deleted", 3000);
                    initFormOperations();
                }
            }
            else { AutoClosingMessageBox.Show("Select A Item", "Alert", 3000); }

        }
        private void btn_UpdateExpenceSubHead(object sender, RoutedEventArgs e)
        {
            if (dg_AllExpenceSubHeads.SelectedItem != null)
            {
                SubHeadHeadNameModel mm = (SubHeadHeadNameModel)dg_AllExpenceSubHeads.SelectedItem;
                tbl_ExpenceSubHead esh = ExpenceSubHead.getById(mm.Id);
                var dialog = new Form_InputDialog(mm.Name);

                if (dialog.ShowDialog() == true)
                {
                    esh.Name = dialog.ResponseText;
                    ExpenceSubHead.update(esh);
                    AutoClosingMessageBox.Show("Expence Sub-Head Updated", "Success", 3000);
                    initFormOperations();
                }

            }

        }

        private void btn_SaveSubHead(object sender, RoutedEventArgs e)
        {
            
            
                if (tb_ExpenceSubHead.Text != "")
                {
                    if (cb_AllExpenceHeads.SelectedItem != null)
                    {
                        tbl_ExpenceSubHead esh = new tbl_ExpenceSubHead();
                        esh.Name = tb_ExpenceSubHead.Text;
                        esh.ExpenseHead_Id = (int)cb_AllExpenceHeads.SelectedValue;
                        ExpenceSubHead.insert(esh);
                        AutoClosingMessageBox.Show("Sub Head Added", "Success", 3000);
                        initFormOperations();
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("Select a Head", "Alert", 3000);
                    }

                }
                else { AutoClosingMessageBox.Show("Please Enter Name", "Alert", 3000); }
            }
        

        private void btn_SaveHead(object sender, RoutedEventArgs e)
        {
            if (tb_ExpenceHead.Text != "")
            {
                tbl_ExpenceHead eh = new tbl_ExpenceHead();
                eh.Name = tb_ExpenceHead.Text;
                ExpenceHead.insert(eh);
                AutoClosingMessageBox.Show("Head Added", "Success", 3000);
                initFormOperations();
            }
            else { AutoClosingMessageBox.Show("Please Enter Name", "Alert", 3000); }
        }
    }
}
