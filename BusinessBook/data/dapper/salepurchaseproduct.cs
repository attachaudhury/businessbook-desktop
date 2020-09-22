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
    public class salepurchaseproductrepo
    {
        string conn = baserepo.connectionstring;
        public void  test()
        {
            //dapper.salepurchaseproduct p = new salepurchaseproduct { barcode = "1231321234234", carrycost = 0, discount = 0, name = "deal 1", purchaseprice = 40,purchaseactive=false, quantity = 0,saleprice=50,saleactive=true };
            //this.save(p);
        }
        public List<dapper.salepurchaseproduct> get() {
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.GetAll<dapper.salepurchaseproduct>().ToList();
                return res;
            }
        }
        public dapper.salepurchaseproduct get(int id)
        {
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Get<dapper.salepurchaseproduct>(id);
                return res;
            }
        }

        public List<dapper.salepurchaseproduct> getmultiplebytransactionid(int financetransactionid)
        {
            var sql = "select * from salepurchaseproduct where fk_financetransaction_in_salepurchaseproduct="+ financetransactionid + ";";
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Query<dapper.salepurchaseproduct>(sql).ToList();
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
        public bool update(dapper.salepurchaseproduct salepurchaseproduct)
        {

            using (var connection = new MySqlConnection(conn))
            {
                var identity = connection.Update<dapper.salepurchaseproduct>(salepurchaseproduct);
                return identity;
            }
        }
    }
}
