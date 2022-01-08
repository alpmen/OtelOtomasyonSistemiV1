using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Oracle.ManagedDataAccess.Client;
namespace OtelOtomasyonSistemiV1
{
    class sqlBaglanti
    {
        public OracleConnection baglanti()
        {
            OracleConnection baglan = new OracleConnection
               ("User Id=SYSTEM;Password=123;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=localhost)(PORT=1521)))");
            baglan.Open();
            return baglan;
        }

    }
}
