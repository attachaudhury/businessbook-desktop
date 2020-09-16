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
    [System.ComponentModel.DataAnnotations.Schema.Table("subproduct")]

    public class subproduct
    {
        public int id { get; set; }
        public int fk_product_product_subproduct { get; set; }
        public int fk_subproduct_product_subproduct { get; set; }
        public double quantity { get; set; }

    }
    public class subproductrepo
    {
        string conn = baserepo.connectionstring;
        public void  test()
        {
            //dapper.subproduct p = new subproduct { fk_product_product_subproduct=3,fk_subproduct_product_subproduct=1,quantity=2};
            //dapper.subproduct p2 = new subproduct { fk_product_product_subproduct=3,fk_subproduct_product_subproduct=2,quantity=2};
            //var save = this.save(p);
            //var save2 = this.save(p2);
            var subproduct = this.get(1);
            var subproducts = this.get();
            var i = 0;
        }
        public List<dapper.subproduct> get() {
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.GetAll<dapper.subproduct>().ToList();
                return res;
            }
        }
        public dapper.subproduct get(int id)
        {
            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Get<dapper.subproduct>(id);
                return res;
            }
        }
        public int save(dapper.subproduct subproduct)
        {

            using (var connection = new MySqlConnection(conn))
            {
                var res = connection.Insert<dapper.subproduct>(subproduct);
                return (int)res;
            }
        }
        public bool update(dapper.subproduct subproduct)
        {

            using (var connection = new MySqlConnection(conn))
            {
                var identity = connection.Update<dapper.subproduct>(subproduct);
                return identity;
            }
        }
    }
}
