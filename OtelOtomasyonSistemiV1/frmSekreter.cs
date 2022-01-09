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
    public partial class frmSekreter : Form
    {
        public frmSekreter()
        {
            InitializeComponent();
        }
        public string sekreterID;
        sqlBaglanti bgl = new sqlBaglanti();
        private void frmSekreter_Load(object sender, EventArgs e)
        {
            label5.Text = sekreterID;

            OracleCommand komut = new OracleCommand("select ADSOYAD FROM TBLSEKRETER where SEKRETERID=:p1", bgl.baglanti());
            komut.Parameters.Add("p1", label5.Text);
            OracleDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                label4.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmRezervasyonIslemleri fr = new frmRezervasyonIslemleri();
            fr.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmMusteriIslemleri fr = new frmMusteriIslemleri();
            fr.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmDuyurular fr = new frmDuyurular();
            fr.sekreterıd = label5.Text;
            fr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmOdaIslemleri fr = new frmOdaIslemleri();
            fr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 fr = new Form1();
            fr.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmSiparişTablosu fr = new frmSiparişTablosu();
            fr.Show();
        }
    }
}
