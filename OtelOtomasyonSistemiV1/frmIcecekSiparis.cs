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
    public partial class frmIcecekSiparis : Form
    {
        public frmIcecekSiparis()
        {
            InitializeComponent();
        }
        public string ID;
        sqlBaglanti bgl = new sqlBaglanti();
        public int yiyecekFiyat = 0;


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleCommand komut = new OracleCommand("select fiyat from TblYiyecekIcecek where isim=:p1", bgl.baglanti());
            komut.Parameters.Add("p1", comboBox1.Text);
            OracleDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                yiyecekFiyat = Convert.ToInt32(dr[0]);
            }
            bgl.baglanti().Close();

            label4.Text = yiyecekFiyat.ToString() + " ₺";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && comboBox2.Text != "")
            {


                //OracleCommand komut = new OracleCommand("insert into TBLSIPARIS (URUN,MUSTERIID,ADET,FIYAT) values (:p1,:p2,:p3,:p4)", bgl.baglanti());
                //komut.Parameters.Add("p1", comboBox1.Text);
                //komut.Parameters.Add("p2", label5.Text);
                //komut.Parameters.Add("p3", comboBox2.Text);
                //komut.Parameters.Add("p4", label1.Text);
                //komut.ExecuteNonQuery();

                //Aynı işlemin Procedure ile yapılması
                OracleCommand komut = new OracleCommand("insertSiparis", bgl.baglanti());
                komut.CommandType = CommandType.StoredProcedure;
                komut.Parameters.Add("PARAM1", OracleDbType.Varchar2).Value = comboBox1.Text;
                komut.Parameters.Add("PARAM2", OracleDbType.Decimal).Value = label5.Text;
                komut.Parameters.Add("PARAM3", OracleDbType.Decimal).Value = comboBox2.Text;
                komut.Parameters.Add("PARAM4", OracleDbType.Decimal).Value = label1.Text;

                OracleDataAdapter da = new OracleDataAdapter(komut);
                komut.ExecuteNonQuery();

                bgl.baglanti().Close();
                int borc = Convert.ToInt32(label1.Text);
                label11.Text = borc.ToString();
                MessageBox.Show("Sipariş Başarılı");
                comboBox1.Text = "";
                comboBox2.Text = "";
                label11.Text = "-";

                int sayi = 1;
                OracleCommand komut2 = new OracleCommand("insert into TBLKASA (FIYAT,DURUM) values (:p1,:p2)", bgl.baglanti());
                komut2.Parameters.Add("p1", borc);
                komut2.Parameters.Add("p2",sayi);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();


                OracleCommand komut3 = new OracleCommand("select BORC from TBLMUSTERI where MUSTERIID=:p1", bgl.baglanti());
                komut3.Parameters.Add("p1", label5.Text);
                OracleDataReader dr = komut3.ExecuteReader();
                while (dr.Read())
                {
                    borc += Convert.ToInt32(dr[0]);
                }
                bgl.baglanti().Close();

                OracleCommand komut4 = new OracleCommand("update TBLMUSTERI set BORC=:p1 where MUSTERIID=:p2", bgl.baglanti());
                komut4.Parameters.Add("p1", borc);
                komut4.Parameters.Add("p2", label5.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Lütfen bir ürün ve adet bilgisi girin");
            }
        }



        private void frmIcecekSiparis_Load(object sender, EventArgs e)
        {

            label5.Text = ID;
            OracleCommand komut = new OracleCommand("select isim from TblYiyecekIcecek", bgl.baglanti());
            OracleDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            bgl.baglanti().Close();


            OracleCommand komut2 = new OracleCommand("select ADSOYAD from TBLMUSTERI where MUSTERIID=:p1", bgl.baglanti());
            komut2.Parameters.Add("p1", label5.Text);
            OracleDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                label13.Text = dr2[0].ToString();
            }
            bgl.baglanti().Close();


            for (int i = 1; i < 11; i++)
            {
                comboBox2.Items.Add(i);

            }

        }





        private void button2_Click_1(object sender, EventArgs e)
        {
            label1.Text = (yiyecekFiyat * (Convert.ToDouble(comboBox2.Text))).ToString();
        }
    }
}
