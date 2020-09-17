using Dapper;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using Remotion.Linq.Clauses.ResultOperators;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIAB_Restaurent_Management_System.data.dapper
{
    [System.ComponentModel.DataAnnotations.Schema.Table("financeaccount")]

    public class financeaccount
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public Nullable<int> fk_parent_in_financeaccount { get; set; }
    }
    public class financeaccountrepo
    {
        string conn = baserepo.connectionstring;
        public void  test()
        {
            //dapper.financeaccount p = new financeaccount { barcode = "1231321234234", carrycost = 0, discount = 0, name = "deal 1", purchaseprice = 40,purchaseactive=false, quantity = 0,saleprice=50,saleactive=true };
            //this.save(p);
        }
        public List<dapper.financeaccount> get() {
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.GetAll<dapper.financeaccount>().ToList();
                return res;
            }
        }
        public dapper.financeaccount get(int id)
        {
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Get<dapper.financeaccount>(id);
                return res;
            }
        }
        public dapper.financeaccount save(dapper.financeaccount financeaccount)
        {

            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Insert<dapper.financeaccount>(financeaccount);
                financeaccount.id = (int)res;
                return financeaccount;
            }
        }
        public bool update(dapper.financeaccount financeaccount)
        {

            using (var connection = new MySqlConnection(conn))
            {
                var identity = connection.Update<dapper.financeaccount>(financeaccount);
                return identity;
            }
        }
    }
}
