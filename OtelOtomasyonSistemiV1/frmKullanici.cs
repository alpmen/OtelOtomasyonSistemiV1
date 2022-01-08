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
    public partial class frmKullanici : Form
    {
        public frmKullanici()
        {
            InitializeComponent();
        }
        public string musteriid;
        sqlBaglanti bgl = new sqlBaglanti();

        public void listele()
        {
            OracleCommand komut = new OracleCommand("select * from TBLSIPARIS where MUSTERIID=:p1", bgl.baglanti());
            komut.Parameters.Add("p1", musteriid);
            OracleDataAdapter da = new OracleDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }
        private void frmKullanici_Load(object sender, EventArgs e)
        {
            label5.Text = musteriid;
            OracleCommand komut = new OracleCommand("select ADSOYAD,BORC FROM TBLMUSTERI where MUSTERIID=:p1", bgl.baglanti());
            komut.Parameters.Add("p1", musteriid);
            OracleDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                label6.Text = dr[0].ToString();
                label4.Text = dr[1].ToString();
            }
            bgl.baglanti().Close();

            listele();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmIcecekSiparis fr = new frmIcecekSiparis();
            fr.ID = label5.Text;
            fr.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmduyuruekrani fr = new frmduyuruekrani();
            fr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 fr = new Form1();
            fr.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listele();

            OracleCommand komut = new OracleCommand("select ADSOYAD,BORC FROM TBLMUSTERI where MUSTERIID=:p1", bgl.baglanti());
            komut.Parameters.Add("p1", musteriid);
            OracleDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                label6.Text = dr[0].ToString();
                label4.Text = dr[1].ToString();
            }
            bgl.baglanti().Close();
        }
    }
}
