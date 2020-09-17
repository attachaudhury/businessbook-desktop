using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIAB_Restaurent_Management_System.data.dapper
{
    public static class baserepo
    {
        public static string connectionstring = "Server=localhost;Database=bbdb;Uid=root;Pwd=;";
        public static string getkeyValuestoSqlAnd(dynamic keyvaluepairs) 
        {
            string s = "";
            foreach (KeyValuePair<string, object> kvp in keyvaluepairs)
            {
                s += getkeyValueToEqualTo(kvp.Key,kvp.Value);
                s += " and ";  
             }
            var ss = s.Remove(s.Length - 5);
            return ss;
        }
        public static string getkeyValuesToSqlOr(dynamic keyvaluepairs)
        {
            string s = "";
            foreach (KeyValuePair<string, object> kvp in keyvaluepairs)
            {
                s += getkeyValueToEqualTo(kvp.Key, kvp.Value);
                s += " or ";
            }
            var ss = s.Remove(s.Length - 4);
            return ss;
        }
        public static string getkeyValueToEqualTo(string key,object value) {
            var s = "";
            s += key + "=";
            if (value is string)
            {
                s += "'" + value + "'";
            }
            else
            {
                s += value;
            }
            return s;
        }
        public static string getWhereInSql(List<object> values)
        {
            var s = "";
            foreach (object value in values) {
                if (value is string)
                {
                    s += "'" + value + "'";
                }
                else
                {
                    s += value;
                }
                s += ",";
            }
            var ss = s.Remove(s.Length - 1);
            return ss;
        }
    }
    
}
