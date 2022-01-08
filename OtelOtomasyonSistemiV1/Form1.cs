using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using Oracle.ManagedDataAccess.Client;
namespace OtelOtomasyonSistemiV1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        sqlBaglanti bgl = new sqlBaglanti();
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();

            try
            {
                string api = "a89628607be204417aed37bea3dfb38f";
                string connection =
                    "https://api.openweathermap.org/data/2.5/weather?q=" + "ANKARA" + "&mode=xml&lang=tr&units=metric&appid=" + api;
                XDocument weather = XDocument.Load(connection);
                var temp = weather.Descendants("temperature").ElementAt(0).Attribute("value").Value;
                var state = weather.Descendants("weather").ElementAt(0).Attribute("value").Value;
                var nem = weather.Descendants("humidity").ElementAt(0).Attribute("value").Value;
                var rüzgar = weather.Descendants("speed").ElementAt(0).Attribute("value").Value;
                label9.Text = "Sıcaklık: " + temp + " ' ";
                label10.Text = "Durum: " + state + " ' ";
                label11.Text = "Nem: " + nem + " ' ";
                label12.Text = "Rüzgar: " + rüzgar + " ' ";
            }
            catch (Exception)
            {

                label9.Text = "";
                label10.Text = "";
                label11.Text = " ";
                label12.Text ="";
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            OracleCommand komut = new OracleCommand("select * from TBLYONETICI where YONETICIID=:p1 and SIFRE=:p2", bgl.baglanti());
            komut.Parameters.Add("p1", textBox8.Text);
            komut.Parameters.Add("p2", textBox7.Text);
            OracleDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                frmYonetici fr = new frmYonetici();
                fr.yoneticiID = textBox8.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("kullanıcı adı veya şifre hatalı");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
           
            OracleCommand komut = new OracleCommand("select * from tblmusterı where musterııd=:p1 and sıfre=:p2", bgl.baglanti());
            komut.Parameters.Add("p1", textBox4.Text);
            komut.Parameters.Add("p2", textBox3.Text);
            OracleDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                frmKullanici fr = new frmKullanici();
                fr.musteriid = textBox4.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("kullanıcı adı veya şifre hatalı");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmKullaniciKayit fr = new frmKullaniciKayit();
            fr.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OracleCommand komut = new OracleCommand("select * from TBLSEKRETER where SEKRETERID=:p1 and SIFRE=:p2", bgl.baglanti());
            komut.Parameters.Add("p1", textBox2.Text);
            komut.Parameters.Add("p2", textBox1.Text);
            OracleDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                frmSekreter fr = new frmSekreter();
                fr.sekreterID = textBox2.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı giriş");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
