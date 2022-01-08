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
    public partial class frmSiparişTablosu : Form
    {
        public frmSiparişTablosu()
        {
            InitializeComponent();
        }
        sqlBaglanti bgl = new sqlBaglanti();
        private void frmSiparişTablosu_Load(object sender, EventArgs e)
        {
            OracleCommand komut = new OracleCommand("select * from TBLSIPARIS", bgl.baglanti());
            OracleDataAdapter da = new OracleDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }
    }
}
