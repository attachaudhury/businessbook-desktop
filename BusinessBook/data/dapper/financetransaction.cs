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
    [System.ComponentModel.DataAnnotations.Schema.Table("financetransaction")]

    public class financetransaction
    {
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<double> amount { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string details { get; set; }
        public Nullable<int> fk_user_createdby_in_financetransaction { get; set; }
        public Nullable<int> fk_user_targetto_in_financetransaction { get; set; }
        public Nullable<int> fk_financeaccount_in_financetransaction { get; set; }

    }
    public class financetransactionrepo
    {
        string conn = baserepo.connectionstring;
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
        public List<dapper.financetransaction> getmanybyfinanceaccountname(string financeaccountname)
        {
            financeaccountrepo financeaccountrepo = new financeaccountrepo();
            financeaccount financeaccount = financeaccountrepo.getonebyname(financeaccountname);

            string sql = "select * from financetransaction where fk_financeaccount_in_financetransaction=" + financeaccount.id + ";";
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.GetAll<dapper.financetransaction>().ToList();
                return res;
            }
        }
        public List<dapper.financetransaction> getmanybyselfnameandfinanceaccountname(string selfname,string financeaccountname)
        {
            financeaccountrepo financeaccountrepo = new financeaccountrepo();
            financeaccount financeaccount = financeaccountrepo.getonebyname(financeaccountname);

            string sql = "select * from financetransaction where name='"+selfname+"' and fk_financeaccount_in_financetransaction=" + financeaccount.id + ";";
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.GetAll<dapper.financetransaction>().ToList();
                return res;
            }
        }
        public List<dapper.financetransaction> getmanybyfinanceaccounttype(string financeaccounttype)
        {
            financeaccountrepo financeaccountrepo = new financeaccountrepo();
            List<financeaccount> financeaccounts = financeaccountrepo.getmanybytype(financeaccounttype);
            object[] financeaccountids = new object[financeaccounts.Count()];
            for (int i = 0; i < financeaccounts.Count(); i++)
            {

                financeaccountids[i] = financeaccounts[i];
            }
            string whereincontent = baserepo.getWhereInSql(financeaccountids);
            string sql = "select * from financetransaction where fk_financeaccount_in_financetransaction in" + whereincontent + ";";
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Query<dapper.financetransaction>(sql).ToList();
                return res;
            }
        }
        public List<dapper.financetransaction> getusersales(int userid)
        {
            financeaccountrepo financeaccountrepo = new financeaccountrepo();
            var list = new List<KeyValuePair<string, object>>();
            list.Add(new KeyValuePair<string, object>("name", "pos sale"));
            list.Add(new KeyValuePair<string, object>("name", "sale"));

            List<financeaccount> financeaccounts = financeaccountrepo.getmanybysqlor(list);
            object[] financeaccountids = new object[financeaccounts.Count()];
            for (int i = 0; i < financeaccounts.Count(); i++)
            {

                financeaccountids[i] = financeaccounts[i];
            }
            string whereincontent = baserepo.getWhereInSql(financeaccountids);
            string sql = "select * from financetransaction where fk_user_targetto_in_financetransaction="+ userid + " and fk_financeaccount_in_financetransaction in" + whereincontent + ";";
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Query<dapper.financetransaction>(sql).ToList();
                return res;
            }
        }
        public int getuserreceiveablessum(int userid)
        {
            financeaccountrepo financeaccountrepo = new financeaccountrepo();
            financeaccount financeaccount = financeaccountrepo.getonebyname("account receivable");
            string sql = "select sum(amount) from financetransaction where fk_user_targetto_in_financetransaction=" + userid + " and fk_financeaccount_in_financetransaction=" + financeaccount.id + ";";
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.ExecuteScalar<int>(sql);
                return res;
            }
        }
        public List<dapper.financetransaction> getuserpurchases(int userid)
        {
            financeaccountrepo financeaccountrepo = new financeaccountrepo();
            financeaccount financeaccounts = financeaccountrepo.getonebyname("inventory");
            var list = new List<KeyValuePair<string, object>>();
            list.Add(new KeyValuePair<string, object>("fk_user_targetto_in_financetransaction", userid));
            list.Add(new KeyValuePair<string, object>("name", "--inventory--on--purchase--"));
            list.Add(new KeyValuePair<string, object>("fk_financeaccount_in_financetransaction", financeaccounts.id));
            string sqland = baserepo.getkeyValuestoSqlAnd(list);
            string sql = "select * from financetransaction where "+ sqland + ";";
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Query<dapper.financetransaction>(sql).ToList();
                return res;
            }
        }
        public int getuserpayablesum(int userid)
        {
            financeaccountrepo financeaccountrepo = new financeaccountrepo();
            financeaccount financeaccount = financeaccountrepo.getonebyname("account payable");
            var list = new List<KeyValuePair<string, object>>();
            list.Add(new KeyValuePair<string, object>("fk_user_targetto_in_financetransaction", userid));
            list.Add(new KeyValuePair<string, object>("fk_financeaccount_in_financetransaction", financeaccount.id));
            string and = baserepo.getkeyValuestoSqlAnd(list);
            string sql = "select sum(amount) from financetransaction where "+and+";";
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.ExecuteScalar<int>(sql);
                return res;
            }
        }

        public int gettransactionsumbyaccountname(string name)
        {
            financeaccountrepo financeaccountrepo = new financeaccountrepo();
            financeaccount financeaccount = financeaccountrepo.getonebyname(name);
            string sql = "select sum(amount) from financetransaction where fk_financeaccount_in_financetransaction=" + financeaccount.id  + ";";
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.ExecuteScalar<int>(sql);
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
