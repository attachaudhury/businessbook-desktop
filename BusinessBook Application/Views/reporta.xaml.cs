using System;
using System.Collections.Generic;
using System.Data;
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
using Microsoft.Reporting.WinForms;

namespace BusinessBook.Views
{
    /// <summary>
    /// Interaction logic for report.xaml
    /// </summary>
    public partial class reporta : Window
    {
        public reporta()
        {
            InitializeComponent();
            _reportviewer.LocalReport.ReportPath = @"C:\Users\atta\Desktop\Untitled.rdl";

            
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("name", typeof(string));

            DataRow myDataRow = dt.NewRow();
            myDataRow["id"] = "1";
            myDataRow["name"] = "aaa";
            dt.Rows.Add(myDataRow);

            DataRow myDataRow1 = dt.NewRow();
            myDataRow1["id"] = "2";
            myDataRow1["name"] = "bbb";
            dt.Rows.Add(myDataRow1);

            var reportDataSource = new ReportDataSource("DataSet1", dt);
            _reportviewer.LocalReport.DataSources.Add(reportDataSource);
            var prms = new ReportParameter("myparam", "atta");
            _reportviewer.LocalReport.SetParameters(prms);
            _reportviewer.RefreshReport();
        }
        
    }

}
