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
    [System.ComponentModel.DataAnnotations.Schema.Table("salepurchaseproduct")]

    public class salepurchaseproduct
    {
        public int id { get; set; }
        public Nullable<double> price { get; set; }
        public Nullable<double> quantity { get; set; }
        public Nullable<double> total { get; set; }
        public Nullable<int> fk_product_in_salepurchaseproduct { get; set; }
        public Nullable<int> fk_financetransaction_in_salepurchaseproduct { get; set; }
    }
    public class salepurchaseproductextended : salepurchaseproduct
    {
        public string productname { get; set; }
    }
    public class salepurchaseproductrepo
    {
        string joinselect = "t1.id,t1.price,t1.quantity,t1.total,t1.fk_product_in_salepurchaseproduct,t1.fk_financetransaction_in_salepurchaseproduct,t2.name as productname from salepurchaseproduct t1 join product t2 on t1.fk_product_in_salepurchaseproduct = t2.id";

        string conn = baserepo.connectionstring;
        
        public List<dapper.salepurchaseproductextended> getmultiplebytransactionid(int financetransactionid)
        {
            var sql = "select " + joinselect + " where fk_financetransaction_in_salepurchaseproduct=" + financetransactionid + ";";
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Query<dapper.salepurchaseproductextended>(sql).ToList();
                return res;
            }
        }
        public dapper.salepurchaseproduct save(dapper.salepurchaseproduct salepurchaseproduct)
        {

            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Insert<dapper.salepurchaseproduct>(salepurchaseproduct);
                salepurchaseproduct.id = (int)res;
                return salepurchaseproduct;
            }
        }
    }
}
