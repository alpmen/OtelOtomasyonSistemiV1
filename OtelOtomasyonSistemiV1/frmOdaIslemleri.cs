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
    public partial class frmOdaIslemleri : Form
    {
        public frmOdaIslemleri()
        {
            InitializeComponent();
        }
        sqlBaglanti bgl = new sqlBaglanti();
        public void listele()
        {
            OracleCommand komut = new OracleCommand("select * from TBLODA", bgl.baglanti());
            OracleDataAdapter da = new OracleDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }
        private void frmOdaIslemleri_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int secenek = 0;
            if (comboBox1.Text=="DOLU")
            {
                secenek = 1;
            }
            else
            {
                secenek = 0;
            }
            if (textBox2.Text!="" && textBox4.Text!="")
            {
                //OracleCommand komut = new OracleCommand("insert into TBLODA (ODAKAT,ODADURUM,ODAUCRET) values(:p1,:p2,:p3)", bgl.baglanti());
                //komut.Parameters.Add("p1", textBox2.Text);
                //komut.Parameters.Add("p2", secenek);
                //komut.Parameters.Add("p3", textBox4.Text);
                //komut.ExecuteNonQuery();


                OracleCommand komut = new OracleCommand("insertOda", bgl.baglanti());
                komut.CommandType = CommandType.StoredProcedure;
                komut.Parameters.Add("PARAM1", OracleDbType.Decimal).Value = textBox2.Text;
                komut.Parameters.Add("PARAM2", OracleDbType.Decimal).Value = secenek;
                komut.Parameters.Add("PARAM3", OracleDbType.Decimal).Value = textBox4.Text;
                OracleDataAdapter da = new OracleDataAdapter(komut);
                komut.ExecuteNonQuery();



                MessageBox.Show("Ekleme İşlemi Başarılı");
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Kat ve Ücret Bilgilerini Giriniz");
            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = dataGridView1.SelectedCells[0].RowIndex;
            string sec;
            if (dataGridView1.Rows[x].Cells[2].Value.ToString()=="1")
            {
                sec = "DOLU";
            }
            else
            {
                sec = "BOŞ";
            }
            
            textBox1.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
            comboBox1.Text = sec;
            textBox4.Text = dataGridView1.Rows[x].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[x].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="")
            {
                try
                {
                    OracleCommand komut = new OracleCommand("delete from TBLODA where ODANO=:p1", bgl.baglanti());
                    komut.Parameters.Add("p1", textBox1.Text);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Silme İşlemi Başarılı");
                    bgl.baglanti().Close();
                }
                catch (Exception)
                {

                    MessageBox.Show("Oda Dolu Silme Başarısız");
                }
            }
            else
            {
                MessageBox.Show("Lütfen Bir Oda Seçiniz");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int durum=0;
            if (comboBox1.Text=="DOLU")
            {
                durum = 1;
            }
            else
            {
                durum = 0;
            }
            if (textBox1.Text!="")
            {
                OracleCommand komut = new OracleCommand("update TBLODA set ODAKAT=:P1,ODADURUM=:P2,ODAUCRET=:P3 where ODANO=:P4", bgl.baglanti());
                komut.Parameters.Add("P1", textBox2.Text);
                komut.Parameters.Add("P2", durum);
                komut.Parameters.Add("P3", textBox4.Text);
                komut.Parameters.Add("P4", textBox1.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Güncelleme İşlemi Başarılı");
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Lütfen Bir Öğe Seçin");
            }

        }
    }
}
