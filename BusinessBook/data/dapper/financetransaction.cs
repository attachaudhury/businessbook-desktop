using Dapper;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

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
    public class financetransactionextended : financetransaction 
    {
        public string accountname { get; set; }
        public string createdby { get; set; }
        public string target { get; set; }
    }
    public class financetransactionrepo
    {
        string joinselect = "t1.id,t1.name,t1.amount,t1.status,t1.details,t1.date,t1.fk_user_createdby_in_financetransaction,t1.fk_user_targetto_in_financetransaction,t1.fk_financeaccount_in_financetransaction,t2.name as accountname,t3.name as createdby,t4.name as target  from financetransaction t1 join financeaccount t2 on t1.fk_financeaccount_in_financetransaction = t2.id join `user` t3 on t1.fk_user_createdby_in_financetransaction=t3.id join `user` t4 on  t1.fk_user_targetto_in_financetransaction=t4.id";
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
        public List<dapper.financetransactionextended> getmanybyfinanceaccountname(string financeaccountname)
        {
            financeaccountrepo financeaccountrepo = new financeaccountrepo();
            financeaccount financeaccount = financeaccountrepo.getonebyname(financeaccountname);
            string sql = "select "+joinselect+" where t1.fk_financeaccount_in_financetransaction=" + financeaccount.id + ";";
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Query<dapper.financetransactionextended>(sql).ToList();
                return res;
            }
        }
        public List<dapper.financetransactionextended> getmanybyselfnameandfinanceaccountname(string selfname,string financeaccountname)
        {
            financeaccountrepo financeaccountrepo = new financeaccountrepo();
            financeaccount financeaccount = financeaccountrepo.getonebyname(financeaccountname);

            string sql = "select " + joinselect + " where t1.name='" + selfname+"' and t1.fk_financeaccount_in_financetransaction=" + financeaccount.id + ";";
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Query<dapper.financetransactionextended>(sql).ToList();
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
            string sql = "select " + joinselect + " where fk_financeaccount_in_financetransaction in" + whereincontent + ";";
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
        public List<financetransactionextended> getusertransactions(int userid)
        {
            
            string sql = "select " + joinselect + " where t1.fk_user_targetto_in_financetransaction=" + userid + ";";
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Query<dapper.financetransactionextended>(sql).ToList();
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
