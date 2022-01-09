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
    public partial class frmDuyurular : Form
    {
        public frmDuyurular()
        {
            InitializeComponent();
        }
        sqlBaglanti bgl = new sqlBaglanti();
        public string sekreterıd;
        public void listele()
        {
            OracleCommand komut = new OracleCommand("select * from TBLDUYURU", bgl.baglanti());
            OracleDataAdapter da = new OracleDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }
        private void frmDuyurular_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text!="")
            {
                //OracleCommand komut = new OracleCommand("insert into TBLDUYURU (duyuru) values(:p1)", bgl.baglanti());
                //komut.Parameters.Add("p1", richTextBox1.Text);
                //komut.ExecuteNonQuery();

                OracleCommand komut = new OracleCommand("insertDuyuru", bgl.baglanti());
                komut.CommandType = CommandType.StoredProcedure;
                komut.Parameters.Add("PARAM1", OracleDbType.Varchar2).Value = richTextBox1.Text;
                komut.Parameters.Add("PARAM2", OracleDbType.Decimal).Value = Convert.ToInt32(sekreterıd);
                OracleDataAdapter da = new OracleDataAdapter(komut);
                komut.ExecuteNonQuery();

                MessageBox.Show("Ekleme işlemi başarılı");
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Lütfen bir mesaj ekleyin");
            }
            listele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            richTextBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                OracleCommand komut = new OracleCommand("delete from TBLDUYURU where DUYURUID=:p1", bgl.baglanti());
                komut.Parameters.Add("p1", textBox1.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Silme işlemi başarılı");
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Lütfen bir mesaj seçin");
            }
            listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                OracleCommand komut = new OracleCommand("update TBLDUYURU set duyuru=:p1 where DUYURUID=:p2", bgl.baglanti());
                komut.Parameters.Add("p1", richTextBox1.Text);
                komut.Parameters.Add("p2", textBox1.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Güncelleme işlemi başarılı");
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Lütfen bir mesaj seçin");
            }
            listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
            richTextBox1.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
        }
    
    }
}
