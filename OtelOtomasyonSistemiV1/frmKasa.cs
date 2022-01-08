using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
namespace OtelOtomasyonSistemiV1
{
    public partial class frmKasa : Form
    {
        public frmKasa()
        {
            InitializeComponent();
        }
        sqlBaglanti bgl = new sqlBaglanti();
        public void listele()
        {
            OracleCommand komut = new OracleCommand("select * from TBLKASA",bgl.baglanti());
            OracleDataAdapter da = new OracleDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }
        private void frmKasa_Load(object sender, EventArgs e)
        {
            listele();
            int gelir = 0;
            int gider = 0;
            int total = 0;
            //1 gelir 0 gider
            OracleCommand komut = new OracleCommand("select * from TBLKASA where DURUM=1",bgl.baglanti());
            OracleDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                gelir += Convert.ToInt32(dr[1]);
            }
            bgl.baglanti().Close();


            OracleCommand komut2 = new OracleCommand("select * from TBLKASA where DURUM=0", bgl.baglanti());
            OracleDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                gider += Convert.ToInt32(dr2[1]);
            }
            bgl.baglanti().Close();

            total = gelir - gider;

            label13.Text = gelir.ToString()+" ₺";
            label12.Text = gider.ToString() + " ₺";
            label11.Text = total.ToString() + " ₺";
        }
    }
}
