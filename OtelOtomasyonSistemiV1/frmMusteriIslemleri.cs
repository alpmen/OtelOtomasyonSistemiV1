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
    public partial class frmMusteriIslemleri : Form
    {
        public frmMusteriIslemleri()
        {
            InitializeComponent();
        }
        sqlBaglanti bgl = new sqlBaglanti();
        public void listele()
        {
            OracleCommand komut = new OracleCommand("select * from TBLMUSTERI", bgl.baglanti());
            OracleDataAdapter da = new OracleDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();

        }
        private void frmMusteriIslemleri_Load(object sender, EventArgs e)
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
            textBox3.Text = "";
            textBox4.Text = "";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string cinsiyet = "K";
            if (textBox2.Text != "" && textBox3.Text != "")
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
                //komut.Parameters.Add("p1", textBox2.Text);
                //komut.Parameters.Add("p2", textBox3.Text);
                //komut.Parameters.Add("p3", cinsiyet);
                //komut.Parameters.Add("p4", 1);
                //komut.ExecuteNonQuery();

                OracleCommand komut = new OracleCommand("insertKullanici", bgl.baglanti());
                komut.CommandType = CommandType.StoredProcedure;
                komut.Parameters.Add("PARAM1", OracleDbType.Varchar2).Value = textBox2.Text;
                komut.Parameters.Add("PARAM2", OracleDbType.Varchar2).Value = textBox3.Text;
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
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[x].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[x].Cells[4].Value.ToString();

            if (dataGridView1.Rows[x].Cells[3].Value.ToString()=="K")
            {
                radioButton1.PerformClick();
            }
            else
            {
                radioButton2.PerformClick();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string c="K";
            if (radioButton1.Checked)
            {
                c = "K";
            }
            else
            {
                c = "E";
            }
            if (textBox1.Text!="" && textBox2.Text!="" && textBox3.Text!="" && textBox4.Text!="")
            {
                OracleCommand komut = new OracleCommand("update TBLMUSTERI set ADSOYAD=:p1, SIFRE=:p2, CINSIYET=:p3, BORC=:p4 where MUSTERIID=:P5", bgl.baglanti());
                komut.Parameters.Add("p1", textBox2.Text);
                komut.Parameters.Add("p2", textBox3.Text);
                komut.Parameters.Add("p3", c);
                komut.Parameters.Add("p4", textBox4.Text);
                komut.Parameters.Add("p5", textBox1.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Güncelleme Başarılı");
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Lütfen Tüm Alanların Dolu Olduğundan Emin Olun");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="")
            {
                OracleCommand komut = new OracleCommand("delete from TBLMUSTERI where MUSTERIID=:p1", bgl.baglanti());
                komut.Parameters.Add("p1", textBox1.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Silme Başarılı");
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Lütfen bir müşteri seçin");
            }
        }
    }
}
