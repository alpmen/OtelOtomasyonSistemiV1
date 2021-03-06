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
    public partial class frmYıyecekIcecekIslemlerı : Form
    {
        public frmYıyecekIcecekIslemlerı()
        {
            InitializeComponent();
        }
        sqlBaglanti bgl = new sqlBaglanti();
        public void listele()
        {
            OracleCommand komut = new OracleCommand("select * from TBLYIYECEKICECEK", bgl.baglanti());
            OracleDataAdapter da = new OracleDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();
        }
        private void frmYıyecekIcecekIslemlerı_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string durum = "Y";
            if (comboBox1.Text=="YİYECEK")
            {
                durum = "Y";
            }
            else
            {
                durum = "I";
            }
            if (textBox2.Text!="" && textBox3.Text!="")
            {
                //OracleCommand komut = new OracleCommand("insert into TBLYIYECEKICECEK (ISIM,DURUM,FIYAT) values(:p1,:p2,:p3)", bgl.baglanti());
                //komut.Parameters.Add("p1", textBox2.Text);
                //komut.Parameters.Add("p2", durum);
                //komut.Parameters.Add("p3", textBox3.Text);
                //komut.ExecuteNonQuery();


                OracleCommand komut = new OracleCommand("insertYiyecek", bgl.baglanti());
                komut.CommandType = CommandType.StoredProcedure;
                komut.Parameters.Add("PARAM1", OracleDbType.Varchar2).Value = textBox2.Text;
                komut.Parameters.Add("PARAM2", OracleDbType.Varchar2).Value = durum;
                komut.Parameters.Add("PARAM2", OracleDbType.Decimal).Value = textBox3.Text;
                OracleDataAdapter da = new OracleDataAdapter(komut);
                komut.ExecuteNonQuery();


                MessageBox.Show("Ekleme İşlemi Başarılı");
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurun");
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = dataGridView1.SelectedCells[0].RowIndex;
            string d;
            if (dataGridView1.Rows[x].Cells[2].Value.ToString()=="Y")
            {
                d = "YİYECEK";
            }
            else
            {
                d = "İÇECEK";
            }
           
            textBox1.Text = dataGridView1.Rows[x].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[x].Cells[1].Value.ToString();
            comboBox1.Text = d;
            textBox3.Text = dataGridView1.Rows[x].Cells[3].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="")
            {
                OracleCommand komut = new OracleCommand("delete from TBLYIYECEKICECEK where ID=:p1", bgl.baglanti());
                komut.Parameters.Add("p1", textBox1.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Silme İşlemi Başarılı");
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Lütfen Bir Ürün Seçiniz");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string d;
            if (comboBox1.Text=="YİYECEK")
            {
                d = "Y";
            }
            else
            {
                d = "I";
            }
            if (textBox1.Text!="")
            {
                OracleCommand komut = new OracleCommand("update TBLYIYECEKICECEK set ISIM=:p1, DURUM=:p2, FIYAT=:p3 where ID=:p4", bgl.baglanti());
                komut.Parameters.Add(":p1", textBox2.Text);
                komut.Parameters.Add(":p2", d);
                komut.Parameters.Add(":p3", textBox3.Text);
                komut.Parameters.Add(":p4", textBox1.Text);
                komut.ExecuteNonQuery();
                MessageBox.Show("Güncelleme İşlemi Başarılı");
                bgl.baglanti().Close();
            }
            else
            {
                MessageBox.Show("Lütfen Bir Ürün Seçin");
            }
        }
    }
}
