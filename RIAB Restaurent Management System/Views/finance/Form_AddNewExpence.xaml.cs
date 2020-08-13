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

namespace RIAB_Restaurent_Management_System.Views.finance
{
    public partial class Form_AddNewExpence : Window
    {
        public Form_AddNewExpence()
        {
            InitializeComponent();
            initFormOperations();
        }
        void initFormOperations()
        {
            foreach(tbl_ExpenceHead eh in ExpenceHead.getAll())
            {
                dg_AllExpenceHeads.Items.Add(eh);
            }
            foreach(tbl_ExpenceSubHead esh in ExpenceSubHead.getAll())
            {
                dg_AllExpenceSubHeads.Items.Add(esh);
            }
        }
        
        private void dg_AllExpenceHeads_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dg_AllExpenceSubHeads.Items.Clear();
            tbl_ExpenceHead eh = (tbl_ExpenceHead)dg_AllExpenceHeads.SelectedItem;
            foreach (tbl_ExpenceSubHead esh in ExpenceSubHead.getAllByExpenceHeadId(eh.Id))
            {
                dg_AllExpenceSubHeads.Items.Add(esh);
            }
        }
        private void btn_Save(object sender, RoutedEventArgs e)
        {
            if (dg_AllExpenceHeads.SelectedItem != null || dg_AllExpenceSubHeads.SelectedItem != null || dg_AllExpenceSubHeads.SelectedItem != null || tb_Cost.Text != null)
            {
                try
                {
                    tbl_ExpenceHead eh = (tbl_ExpenceHead)dg_AllExpenceHeads.SelectedItem;
                    tbl_ExpenceSubHead esh = (tbl_ExpenceSubHead)dg_AllExpenceSubHeads.SelectedItem;
                    string name = tb_Name.Text;
                    int cost = Convert.ToInt32(tb_Cost.Text);
                    tbl_Expence expence = new tbl_Expence();
                    expence.Comment = name;
                    expence.Amount = cost;
                    expence.ExpenseHead_Id = eh.Id;
                    expence.ExpenceSubHead_Id = esh.Id;
                    DateTime date = DateTime.Now;
                    expence.DatenTime = date;
                    Expence.insert(expence);
                    AutoClosingMessageBox.Show("Expence Added", "Success", 3000);
                    Close();
                }
                catch
                {
                    AutoClosingMessageBox.Show("Some Error Occures", "Failed", 3000);
                }
            }
            else
            {
                AutoClosingMessageBox.Show("Please Insert Data Properly", "Alert", 3000);
            }

        }
    }
}
