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
    public partial class frmKullaniciKayit : Form
    {
        public frmKullaniciKayit()
        {
            InitializeComponent();
        }
        sqlBaglanti bgl = new sqlBaglanti();
        private void button1_Click(object sender, EventArgs e)
        {
            string cinsiyet="K";
            if (textBox1.Text!="" && textBox2.Text!="")
            {
                if (radioButton1.Checked)
                {
                    cinsiyet = "K";
                }
                else
                {
                    cinsiyet = "E";
                }
                //OracleCommand komut = new OracleCommand("insert into TBLMUSTERI (adsoyad,sıfre,cınsıyet,borc) values(:p1,:p2,:p3,:p4)", bgl.baglanti());
                //komut.Parameters.Add("p1", textBox1.Text);
                //komut.Parameters.Add("p2", textBox2.Text); 
                //komut.Parameters.Add("p3", cinsiyet);
                //komut.Parameters.Add("p4", 1);
                //komut.ExecuteNonQuery();


                OracleCommand komut = new OracleCommand("insertKullanici", bgl.baglanti());
                komut.CommandType = CommandType.StoredProcedure;
                komut.Parameters.Add("PARAM1", OracleDbType.Varchar2).Value = textBox1.Text;
                komut.Parameters.Add("PARAM2", OracleDbType.Varchar2).Value = textBox2.Text;
                komut.Parameters.Add("PARAM3", OracleDbType.Varchar2).Value = cinsiyet;
                komut.Parameters.Add("PARAM4", OracleDbType.Varchar2).Value = 1;
                OracleDataAdapter da = new OracleDataAdapter(komut);
                komut.ExecuteNonQuery();





                MessageBox.Show("Ekleme işlemi başarılı");
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Lütfen tüm alanları doldurun");
            }

            OracleCommand komut2 = new OracleCommand("select max(musterııd) from tblmusterı", bgl.baglanti());
            OracleDataReader dr = komut2.ExecuteReader();
            while (dr.Read())
            {
                label5.Text = "Müşteri ID'niz: " + dr[0];
            }
            bgl.baglanti().Close();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void frmKullaniciKayit_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }
    }
}
