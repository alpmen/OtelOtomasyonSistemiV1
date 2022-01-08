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
    public partial class frmSekreterIslemleri : Form
    {
        public frmSekreterIslemleri()
        {
            InitializeComponent();
        }
        sqlBaglanti bgl = new sqlBaglanti();
        public void listele()
        {
            OracleCommand komut = new OracleCommand("select * from TBLSEKRETER", bgl.baglanti());
            OracleDataAdapter da = new OracleDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void frmSekreterIslemleri_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text!="" && textBox3.Text!="")
            {
                //OracleCommand komut = new OracleCommand("insert into TBLSEKRETER (ADSOYAD,SIFRE) values(:p1,:p2)", bgl.baglanti());
                //komut.Parameters.Add("p1", textBox2.Text);
                //komut.Parameters.Add("p2", textBox3.Text);
                //komut.ExecuteNonQuery();


                OracleCommand komut = new OracleCommand("insertSekreter", bgl.baglanti());
                komut.CommandType = CommandType.StoredProcedure;
                komut.Parameters.Add("PARAM1", OracleDbType.Varchar2).Value = textBox2.Text;
                komut.Parameters.Add("PARAM2", OracleDbType.Varchar2).Value = textBox3.Text;
                OracleDataAdapter da = new OracleDataAdapter(komut);
                komut.ExecuteNonQuery();


                MessageBox.Show("Ekleme İşlemi Başarılı");
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurunuz");
            }
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
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[x].Cells[2].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="")
            {
                OracleCommand komut = new OracleCommand("delete from TBLSEKRETER where SEKRETERID=:p1", bgl.baglanti());
                komut.Parameters.Add("p1", textBox1.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Silme İşlemi Başarılı");
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Lütfen Bir Sekreter Seçin");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                OracleCommand komut = new OracleCommand("update TBLSEKRETER set ADSOYAD=:p1 , SIFRE=:p2  where SEKRETERID=:p3", bgl.baglanti());
                komut.Parameters.Add("p1", textBox2.Text);
                komut.Parameters.Add("p2", textBox3.Text);
                komut.Parameters.Add("p3", textBox1.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Güncelleme İşlemi Başarılı");
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Lütfen Bir Sekreter Seçin");
            }
        }

        
    }
}
