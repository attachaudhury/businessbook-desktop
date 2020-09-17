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
    [System.ComponentModel.DataAnnotations.Schema.Table("financetransaction")]

    public class financetransaction
    {
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<double> amount { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string paymentmethod { get; set; }
        public string referencenumber { get; set; }
        public string bank { get; set; }
        public string branch { get; set; }
        public Nullable<System.DateTime> chequedate { get; set; }
        public string details { get; set; }
        public Nullable<int> fk_user_createdby_in_financetransaction { get; set; }
        public Nullable<int> fk_user_targetto_in_financetransaction { get; set; }
        public Nullable<int> fk_financeaccount_in_financetransaction { get; set; }

    }
    public class financetransactionrepo
    {
        string conn = baserepo.connectionstring;
        public void  test()
        {
            //dapper.financetransaction p = new financetransaction { barcode = "1231321234234", carrycost = 0, discount = 0, name = "deal 1", purchaseprice = 40,purchaseactive=false, quantity = 0,saleprice=50,saleactive=true };
            //this.save(p);
        }
        public List<dapper.financetransaction> get() {
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.GetAll<dapper.financetransaction>().ToList();
                return res;
            }
        }
        public dapper.financetransaction get(int id)
        {
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Get<dapper.financetransaction>(id);
                return res;
            }
        }
        public dapper.financetransaction save(dapper.financetransaction financetransaction)
        {

            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Insert<dapper.financetransaction>(financetransaction);
                financetransaction.id = (int)res;
                return financetransaction;
            }
        }
        public bool update(dapper.financetransaction financetransaction)
        {

            using (var connection = new MySqlConnection(conn))
            {
                var identity = connection.Update<dapper.financetransaction>(financetransaction);
                return identity;
            }
        }
    }
}
