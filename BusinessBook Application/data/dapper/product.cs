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
    [System.ComponentModel.DataAnnotations.Schema.Table("product")]

    public class product
    {
        public int id { get; set; }
        public string barcode { get; set; }
        public string category { get; set; }
        public Nullable<double> carrycost { get; set; }
        public Nullable<double> discount { get; set; }
        public string name { get; set; }
        public Nullable<double> purchaseprice { get; set; }
        public Nullable<bool> purchaseactive { get; set; }
        public Nullable<double> quantity { get; set; }
        public Nullable<double> saleprice { get; set; }
        public Nullable<bool> saleactive { get; set; }
    }
    public class productrepo
    {
        string conn = baserepo.connectionstring;
        public void  test()
        {
            //dapper.product p = new product { barcode = "1231321234234", carrycost = 0, discount = 0, name = "deal 1", purchaseprice = 40,purchaseactive=false, quantity = 0,saleprice=50,saleactive=true };
            //this.save(p);
        }
        public List<dapper.product> get() {
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.GetAll<dapper.product>().ToList();
                return res;
            }
        }
        public dapper.product get(int id)
        {
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Get<dapper.product>(id);
                return res;
            }
        }
        public dapper.product save(dapper.product product)
        {

            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Insert<dapper.product>(product);
                product.id = (int)res;
                return product;
            }
        }
        public bool update(dapper.product product)
        {

            using (var connection = new MySqlConnection(conn))
            {
                var identity = connection.Update<dapper.product>(product);
                return identity;
            }
        }
    }
}
