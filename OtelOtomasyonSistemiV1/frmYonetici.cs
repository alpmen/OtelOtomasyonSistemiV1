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
    public partial class frmYonetici : Form
    {
        public frmYonetici()
        {
            InitializeComponent();
        }
        public string yoneticiID;
        sqlBaglanti bgl = new sqlBaglanti();
        private void frmYonetici_Load(object sender, EventArgs e)
        {
            label5.Text = yoneticiID;

            OracleCommand komut = new OracleCommand("select ADSOYAD FROM TBLYONETICI where YONETICIID=:p1", bgl.baglanti());
            komut.Parameters.Add("p1", yoneticiID);
            OracleDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                
                label4.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmKasa fr = new frmKasa();
            fr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmSekreterIslemleri fr = new frmSekreterIslemleri();
            fr.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmYıyecekIcecekIslemlerı fr = new frmYıyecekIcecekIslemlerı();
            fr.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmYoneticiIslemleri fr = new frmYoneticiIslemleri();
            fr.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmMusteriIslemleri fr = new frmMusteriIslemleri();
            fr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 fr = new Form1();
            fr.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmodaLog fr = new frmodaLog();
            fr.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmYiyecekLog fr = new frmYiyecekLog();
            fr.Show();
        }
    }
}
