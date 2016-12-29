using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication7
{
    public partial class doluodaForm2 : Form
    {

        public SqlConnection baglantı;
        public doluodaForm2()
        {
            InitializeComponent();
        }
        public void doluodagetir()
        {
            SqlConnection baglantı = new SqlConnection("data source=PC\\SA;initial catalog=otel;Integrated security=true");
            if (baglantı.State == ConnectionState.Closed)
            {
                baglantı.Open();

            }
            SqlCommand doluodakomut = new SqlCommand("select * from odano where durum='true'", baglantı);

            doluodakomut.ExecuteNonQuery();

            SqlDataAdapter tablo = new SqlDataAdapter(doluodakomut);

            DataTable veriler = new DataTable();
            tablo.Fill(veriler);

            dataGridView1.DataSource = veriler;
            baglantı.Close();
        }
        private void doluodaForm2_Load(object sender, EventArgs e)
        {
            SqlConnection baglantı = new SqlConnection("data source=PC\\SA;initial catalog=otel;Integrated security=true");
            doluodagetir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                    SqlConnection baglantı = new SqlConnection("data source=PC\\SA;initial catalog=otel;Integrated security=true");
                    if (baglantı.State == ConnectionState.Closed)
                    {
                    baglantı.Open();
                    }

                    string odano= dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["odanumarası"].Value.ToString();
                
                    int tcno = int.Parse(textBox3.Text);
                    string ad1 = textBox1.Text;
                    string soyad1 = textBox2.Text;
                    SqlCommand komutgüncelleme = new SqlCommand("update odano set tckimlik='" + tcno + "', ad='"+ ad1 + "', soyad='"+ soyad1 +"' where odanumarası='"+odano+"' " , baglantı);
                   

                    komutgüncelleme.ExecuteNonQuery();

                    SqlDataAdapter tablo = new SqlDataAdapter(komutgüncelleme);

                    DataTable veriler = new DataTable();
                    tablo.Fill(veriler);

                    dataGridView1.DataSource = veriler;
            
                    baglantı.Close();
            }
            catch (SqlException hata)
            {
                MessageBox.Show(hata.Message);
            }
            doluodagetir();
        } 

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int satır = dataGridView1.CurrentRow.Index;
            textBox1.Text = dataGridView1.Rows[satır].Cells["ad"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[satır].Cells["soyad"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[satır].Cells["tckimlik"].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[satır].Cells["çıkıştarihi"].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int odano = int.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["odanumarası"].Value.ToString());
         
            
            if (baglantı.State == ConnectionState.Closed)
            {
                baglantı.Open();

            }

            SqlCommand komutbosalt = new SqlCommand("update odano set tckimlik='',ad='',soyad='',giriştarihi=''çıkıştarihi='', durum=true where odanumarası='"+odano+"'" , baglantı);

           baglantı.Close();
        }
}

    }
