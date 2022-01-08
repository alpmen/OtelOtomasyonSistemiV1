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
    public partial class frmRezervasyonIslemleri : Form
    {
        public frmRezervasyonIslemleri()
        {
            InitializeComponent();
        }
        sqlBaglanti bgl = new sqlBaglanti();
        public string eskiOda;
        public void listele()
        {
            OracleCommand komut = new OracleCommand("select * from TBLREZERVASYON", bgl.baglanti());
            OracleDataAdapter da = new OracleDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }

        public void odaListele()
        {
            comboBox1.Items.Clear();
            OracleCommand komut2 = new OracleCommand("select ODANO from TBLODA  where ODADURUM=0", bgl.baglanti());
            OracleDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                comboBox1.Items.Add(dr2[0].ToString());
            }
            bgl.baglanti().Close();
        }
        private void frmRezervasyonIslemleri_Load(object sender, EventArgs e)
        {
            listele();
            OracleCommand komut = new OracleCommand("select MUSTERIID from TBLMUSTERI ", bgl.baglanti());
            OracleDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0].ToString());
            }
            bgl.baglanti().Close();

            odaListele();




        }

        private void button4_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime bTarih = Convert.ToDateTime(dateTimePicker1.Text);
            DateTime kTarih = Convert.ToDateTime(dateTimePicker2.Text);
            TimeSpan Sonuc =  kTarih- bTarih;
            textBox2.Text = Sonuc.TotalDays.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleCommand komut = new OracleCommand("select ODAUCRET from TBLODA where ODANO=:p1", bgl.baglanti());
            komut.Parameters.Add("p1", comboBox1.Text);
            OracleDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                textBox3.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text!="" && textBox3.Text!="")
            {
                double gün = Convert.ToDouble(textBox2.Text);
                double ucret = Convert.ToDouble(textBox3.Text);
                double total = gün * ucret;
                label9.Text = total.ToString();
                button1.Enabled = true;
            }
            else
            {
                MessageBox.Show("Gün ve Oda Seçiniz");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text!="" && comboBox2.Text!="")
            {
                //OracleCommand komut = new OracleCommand("insert into TBLREZERVASYON (GIRISTARIHI,CIKISTARIHI,KALINANGUNSAYISI,UCRET,MUSTERIID,ODANO) values(:p1,:p2,:p3,:p4,:p5,:p6)", bgl.baglanti());
                //komut.Parameters.Add("p1", dateTimePicker1.Value);
                //komut.Parameters.Add("p2", dateTimePicker2.Value);
                //komut.Parameters.Add("p3", textBox2.Text);
                //komut.Parameters.Add("p4", label9.Text);
                //komut.Parameters.Add("p5", comboBox2.Text);
                //komut.Parameters.Add("p6", comboBox1.Text);
                //komut.ExecuteNonQuery();

                OracleCommand komut = new OracleCommand("insertRezervasyon", bgl.baglanti());
                komut.CommandType = CommandType.StoredProcedure;
                komut.Parameters.Add("PARAM1", OracleDbType.Date).Value = dateTimePicker1.Value;
                komut.Parameters.Add("PARAM2", OracleDbType.Date).Value = dateTimePicker2.Value;
                komut.Parameters.Add("PARAM3", OracleDbType.Decimal).Value = textBox2.Text;
                komut.Parameters.Add("PARAM4", OracleDbType.Decimal).Value = label9.Text;
                komut.Parameters.Add("PARAM5", OracleDbType.Decimal).Value = comboBox2.Text;
                komut.Parameters.Add("PARAM6", OracleDbType.Decimal).Value = comboBox1.Text;
                OracleDataAdapter da = new OracleDataAdapter(komut);
                komut.ExecuteNonQuery();

                MessageBox.Show("Ekleme İşlemi Başarılı");
                bgl.baglanti().Close();


                OracleCommand komut2 = new OracleCommand("update TBLODA set ODADURUM=1,MUSTERIID=:p1 where ODANO=:p2", bgl.baglanti());
                komut2.Parameters.Add("p1", comboBox2.Text);
                komut2.Parameters.Add("p2", comboBox1.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();



                int sayi = 1;
                OracleCommand komut3 = new OracleCommand("insert into TBLKASA (FIYAT,DURUM) values (:p1,:p2)", bgl.baglanti());
                komut3.Parameters.Add("p1", Convert.ToInt32(label9.Text));
                komut3.Parameters.Add("p2", sayi);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                int eskiBorc=0;
                OracleCommand komut4 = new OracleCommand("select BORC from TBLMUSTERI where MUSTERIID=:p1", bgl.baglanti());
                komut4.Parameters.Add("p1", comboBox2.Text);
                OracleDataReader dr = komut4.ExecuteReader();
                while (dr.Read())
                {
                    eskiBorc = Convert.ToInt32(dr[0]);
                }
                bgl.baglanti().Close();

                int yeniBorc = eskiBorc + Convert.ToInt32(label9.Text);
                OracleCommand komut5 = new OracleCommand("update TBLMUSTERI set BORC=:p1 where MUSTERIID=:p2", bgl.baglanti());
                komut5.Parameters.Add("p1", yeniBorc);
                komut5.Parameters.Add("p2", comboBox2.Text);
                komut5.ExecuteNonQuery();
                bgl.baglanti().Close();



                odaListele();
            }
            else
            {
                MessageBox.Show("Gün ve Oda Seçiniz");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = dataGridView1.SelectedCells[0].RowIndex;
            //DateTime time = Convert.ToDateTime(dataGridView1.Rows[x].Cells[4].Value);
            
            
            textBox1.Text =dataGridView1.Rows[x].Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.Rows[x].Cells[6].Value.ToString();
            eskiOda= dataGridView1.Rows[x].Cells[6].Value.ToString();
        //dateTimePicker2.Value=time.Date;
             textBox2.Text= dataGridView1.Rows[x].Cells[3].Value.ToString();
            textBox3.Text= dataGridView1.Rows[x].Cells[4].Value.ToString();
            comboBox2.Text= dataGridView1.Rows[x].Cells[5].Value.ToString();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="")
            {
                OracleCommand komut = new OracleCommand("delete from TBLREZERVASYON where REZERVASYONID=:p1", bgl.baglanti());
                komut.Parameters.Add("p1", textBox1.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Silme İşlemi Başarılı");
                bgl.baglanti().Close();

                OracleCommand komut2 = new OracleCommand("update TBLODA set ODADURUM=0 where ODANO=:p1", bgl.baglanti());
                komut2.Parameters.Add("p1", comboBox1.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Lütfen Bir Rezervasyon Seçin");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text != "")
            {
                OracleCommand komut = new OracleCommand("update TBLREZERVASYON set CIKISTARIHI=:p1,KALINANGUNSAYISI=:p2,UCRET=:p3,MUSTERIID=:p4,ODANO=:p5 where REZERVASYONID=:p6", bgl.baglanti());
                komut.Parameters.Add("p1",dateTimePicker2.Value);
                komut.Parameters.Add("p2",textBox2.Text);
                komut.Parameters.Add("p3",textBox3.Text);
                komut.Parameters.Add("p4",comboBox2.Text);
                komut.Parameters.Add("p5",comboBox1.Text);
                komut.Parameters.Add("p6",textBox1.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Güncelleme İşlemi Başarılı");
                bgl.baglanti().Close();


                OracleCommand komut2 = new OracleCommand("update TBLODA set ODADURUM=1 where ODANO=:p1", bgl.baglanti());
                komut2.Parameters.Add("p1", comboBox1.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();

                OracleCommand komut3 = new OracleCommand("update TBLODA set ODADURUM=0 where ODANO=:p1", bgl.baglanti());
                komut3.Parameters.Add("p1", eskiOda);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                odaListele();
            }
            else
            {
                MessageBox.Show("Lütfen Bir Rezervasyon Seçin");
            }
        }
    }
}
