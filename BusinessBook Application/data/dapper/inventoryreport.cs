using BusinessBook.bll;
using Dapper;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessBook.data.dapper
{
    [System.ComponentModel.DataAnnotations.Schema.Table("inventoryreport")]

    public class inventoryreport
    {
        public int id { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string note { get; set; }
        public Nullable<double> quantity { get; set; }
        public Nullable<int> fk_product_in_inventoryreport { get; set; }
    }
    public class inventoryreportrepo
    {
        string conn = baserepo.connectionstring;

        public List<dapper.inventoryreport> get(int productid, DateTime? FromDate = null, DateTime? ToDate = null)
        {
            
            var sql = "select * from inventoryreport where fk_product_in_inventoryreport=" + productid + ";";
            if (FromDate != null)
            {
                TimeUtils.getStartDate(FromDate);
            }
            if (ToDate != null)
            {
                TimeUtils.getEndDate(FromDate);
            }
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Query<dapper.inventoryreport>(sql).ToList();
                return res;
            }
        }
        public bool save(dapper.inventoryreport product)
        {
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Insert<dapper.inventoryreport>(product);
                return true;
            }
        }
    }
}
