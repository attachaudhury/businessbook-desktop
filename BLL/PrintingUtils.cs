using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DBOperations;
using BLL.DBOperations.TmpModels;
using DAL;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Shapes;
using System.Drawing.Printing;
using System.Printing;
using System.Windows.Media;
using BLL.Properties;

namespace BLL
{
    public class PrintingUtils
    {
        public static void printSaleReceipt(int salesId, List<ItemOrDealSaleModel> list, int totalBill,int remaining,int saleType, string customerAddress)
        {
            PrintDialog pd = new PrintDialog();
            var doc = ((IDocumentPaginatorSource)getFlowDocument(salesId, list, totalBill, remaining,saleType,customerAddress)).DocumentPaginator;
            
            pd.PrintQueue = new PrintQueue(new PrintServer(), new PrinterSettings().PrinterName);
            pd.PrintDocument(doc, "Print Document");
        }
        static FlowDocument getFlowDocument(int salesId, List<ItemOrDealSaleModel> list, int totalBill,int remaining, int saleType,string customerAddress)
        {
            FlowDocument fd = new FlowDocument();
            //fd.PageWidth = 260;
            fd.PageWidth = Settings1.Default.PrinterPageWidth;
            fd.LineHeight = Settings1.Default.Reciptlineheight;
            fd.FontFamily = new FontFamily("Arial");

            //fd.PagePadding = new Thickness(40, 0, 0, 0);
            fd.PagePadding = new Thickness(Settings1.Default.PrinterMarginLeft, 0, 0, 0);
            fd.TextAlignment = TextAlignment.Center;
            Section header = new Section();
            //Paragraph header1 = new Paragraph(new Bold(new Run("3 Brothers Fast Food")));
            //Paragraph header2 = new Paragraph(new Run("Madni Shopping Mall, Darya Khan Road, Near Boys Degree College Bhakkar. 0305-5189661"));

            Paragraph header1 = new Paragraph(new Bold(new Run(Settings1.Default.Title)));
            Paragraph header2 = new Paragraph(new Run(Settings1.Default.SubTitle));

            string date = DateTime.Now.ToShortDateString();
            Paragraph header3 = new Paragraph(new Run("Sales Id: " + salesId+"   Date:  "+date));
            Paragraph header4 = new Paragraph(new Run("______________________________________"));
            header1.FontSize = 14;
            header2.FontSize = 10;
            header3.FontSize = 9;
            header4.FontSize = 8;
            header.Blocks.Add(header1);
            header.Blocks.Add(header2);
            header.Blocks.Add(header3);
            header.Blocks.Add(header4);


            Section middle = new Section();
            middle.FontSize = 9;
            Table table = new Table();
            table.TextAlignment = TextAlignment.Left;
            TableColumn tb1 = new TableColumn();
            tb1.Width = new GridLength(140);
            TableColumn tb2 = new TableColumn();
            TableColumn tb3 = new TableColumn();
            TableColumn tb4 = new TableColumn();
            table.Columns.Add(tb1);
            table.Columns.Add(tb2);
            table.Columns.Add(tb3);
            table.Columns.Add(tb4);
            table.RowGroups.Add(new TableRowGroup());
            TableRow trHeader = new TableRow();
            table.RowGroups[0].Rows.Add(trHeader);
            trHeader.Cells.Add(new TableCell(new Paragraph(new Run("Name"))));
            trHeader.Cells.Add(new TableCell(new Paragraph(new Run("Rs"))));
            trHeader.Cells.Add(new TableCell(new Paragraph(new Run("Qty"))));
            trHeader.Cells.Add(new TableCell(new Paragraph(new Run("Ttl"))));
            foreach (ItemOrDealSaleModel item in list)
            {
                TableRow tr = new TableRow();
                table.RowGroups[0].Rows.Add(tr);
                tr.Cells.Add(new TableCell(new Paragraph(new Run(item.Name))));
                tr.Cells.Add(new TableCell(new Paragraph(new Run(Convert.ToString(item.SalePrice)))));
                tr.Cells.Add(new TableCell(new Paragraph(new Run(Convert.ToString(item.Quantity)))));
                tr.Cells.Add(new TableCell(new Paragraph(new Run(Convert.ToString(item.Total)))));
            }
            middle.Blocks.Add(table);
            middle.Blocks.Add(new Paragraph(new Run("______________________________________")));
            middle.Blocks.Add(new Paragraph(new Bold(new Run("Total                              " + totalBill))));
            middle.Blocks.Add(new Paragraph(new Run("Paid:   " + (totalBill+remaining) +"   Remaining:   "+remaining)));






            Section footer = new Section();
            //Paragraph footer1 = new Paragraph(new Run("Thank You for Purchaings. For Home Delivery please call us at: 0453-510066"));
            Paragraph footer1 = new Paragraph(new Run(Settings1.Default.Footer));
            Paragraph footer2 = new Paragraph(new Run("Software Powered By RIAB"));
            Paragraph footer3 = new Paragraph(new Run("                "));
            Paragraph footer4 = new Paragraph(new Run(customerAddress));
            footer1.FontSize = 9;
            footer2.FontSize = 7;
            footer4.FontSize = 9;
            footer.Blocks.Add(footer1);
            footer.Blocks.Add(footer2);

            //if saleType is 3, then it will also print customer address on recipt
            if (saleType == 3)
            {
                footer.Blocks.Add(footer4);
                footer.Blocks.Add(footer3);
            }
            else
            {
                footer.Blocks.Add(footer3);
            }
            fd.Blocks.Add(header);
            fd.Blocks.Add(middle);
            fd.Blocks.Add(footer);
            return fd;
        }
    }
}
