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
    public partial class Form_ExpenceReport : Window
    {
        public Form_ExpenceReport()
        {
            InitializeComponent();
            initFormOperations();
        }
        void initFormOperations() 
        {
            dg_AllExpences.ItemsSource = Expence.getAll();
            int total = 0;
            foreach (tbl_Expence item in Expence.getAll())
            {
                total += (int)item.Amount;
            }
            lbl_total.Content = total;

            foreach (tbl_ExpenceHead head in ExpenceHead.getAll())
            {
                cb_Head.ItemsSource = ExpenceHead.getAll();
                cb_Head.SelectedValuePath = "Id";
                cb_Head.DisplayMemberPath = "Name";
            }
            foreach (tbl_ExpenceSubHead subHead in ExpenceSubHead.getAll())
            {
                cb_SubHead.ItemsSource = ExpenceSubHead.getAll();
                cb_SubHead.SelectedValuePath = "Id";
                cb_SubHead.DisplayMemberPath = "Name";
            }
        }

        private void cb_Head_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_Head.SelectedValue != null)
            {
                cb_SubHead.ItemsSource = null;
                int headId = (int)cb_Head.SelectedValue;
                foreach (tbl_ExpenceSubHead subHead in ExpenceSubHead.getAllByExpenceHeadId(headId))
                {
                    cb_SubHead.ItemsSource = ExpenceSubHead.getAllByExpenceHeadId(headId);
                    cb_SubHead.SelectedValuePath = "Id";
                    cb_SubHead.DisplayMemberPath = "Name";
                }
            }
            
        }

        private void btn_Search(object sender, RoutedEventArgs e)
        {
            if (dp_StartDate.SelectedDate != null && dp_EndDate.SelectedDate != null)
            {
                DateTime startDate = TimeUtils.getStartDate(dp_StartDate.SelectedDate);
                DateTime endDate = TimeUtils.getEndDate(dp_EndDate.SelectedDate);
                dg_AllExpences.ItemsSource = null;
                dg_AllExpences.ItemsSource = Expence.getAllCustomDate(startDate, endDate);

                int total = 0;
                foreach (tbl_Expence item in Expence.getAllCustomDate(startDate, endDate))
                {
                    total += (int)item.Amount;
                }
                lbl_total.Content = total;
            }
        }
        private void btn_FindExpencesByHead(object sender, RoutedEventArgs e)
        {
            if (cb_Head.SelectedValue != null && dp_StartDate.SelectedDate != null && dp_EndDate.SelectedDate != null)
            {
                int headId = (int)cb_Head.SelectedValue;
                DateTime startDate = TimeUtils.getStartDate(dp_StartDate.SelectedDate);
                DateTime endDate = TimeUtils.getEndDate(dp_EndDate.SelectedDate);
                dg_AllExpences.ItemsSource = null;

                dg_AllExpences.ItemsSource = Expence.getAllByHeadIdAndDateTime(headId, startDate, endDate);
                int total = 0;
                foreach (tbl_Expence item in Expence.getAllByHeadIdAndDateTime(headId, startDate, endDate))
                {
                    total += (int)item.Amount;
                }
                lbl_total.Content = total;
            }
        }
        private void btn_FindExpencesBySubHead(object sender, RoutedEventArgs e)
        {
            if (cb_SubHead.SelectedValue != null && dp_StartDate.SelectedDate != null && dp_EndDate.SelectedDate != null)
            {
                int subHeadId = (int)cb_SubHead.SelectedValue;
                DateTime startDate = TimeUtils.getStartDate(dp_StartDate.SelectedDate);
                DateTime endDate = TimeUtils.getEndDate(dp_EndDate.SelectedDate);
                dg_AllExpences.ItemsSource = null;
                dg_AllExpences.ItemsSource = Expence.getAllBySubHeadIdAndDateTime(subHeadId, startDate, endDate);
                int total = 0;
                foreach (tbl_Expence item in Expence.getAllBySubHeadIdAndDateTime(subHeadId, startDate, endDate))
                {
                    total += (int)item.Amount;
                }
                lbl_total.Content = total;
            }
        }
    }
}
