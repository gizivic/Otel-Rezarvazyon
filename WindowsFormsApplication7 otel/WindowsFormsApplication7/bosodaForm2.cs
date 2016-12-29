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
    public partial class bosodaForm2 : Form
    {
        public SqlConnection baglantı;
        public bosodaForm2()
        {
            InitializeComponent();
        }
        public void bosodagetir()
        {

            if (baglantı.State == ConnectionState.Closed)
            {
                baglantı.Open();

            }
            SqlCommand bosodakomut = new SqlCommand("select* from odano where durum='false'", baglantı);

            bosodakomut.ExecuteNonQuery();
          
           SqlDataAdapter tablo = new SqlDataAdapter(bosodakomut);

            DataTable veriler = new DataTable();
            tablo.Fill(veriler);

            dataGridView1.DataSource = veriler;
            baglantı.Close();
}


        private void button1_Click(object sender, EventArgs e)
        {

            yenikayıtekle();
           
        }

        private void bosodaForm2_Load(object sender, EventArgs e)
        {
            baglantı = new SqlConnection("data source=PC\\SA;initial catalog=otel;Integrated security=true");
            bosodagetir();
        }

        public void secilihücrekayıtekle()
        {
            int satır = dataGridView1.CurrentRow.Index;


            dataGridView1.Rows[satır].Cells["ad"].Value = textBox1.Text;
            dataGridView1.Rows[satır].Cells["soyad"].Value = textBox2.Text;
            dataGridView1.Rows[satır].Cells["tckimlik"].Value = textBox3.Text;
            dataGridView1.Rows[satır].Cells["giriştarihi"].Value = dateTimePicker1.Text;
            dataGridView1.Rows[satır].Cells["çıkıştarihi"].Value = dateTimePicker2.Text;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            secilihücrekayıtekle();
        }

        public void yenikayıtekle() {

            string odano = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["odanumarası"].Value.ToString();
            string ad = textBox1.Text;
            string soyad = textBox2.Text;
            string tckimlik = textBox3.Text;
            string giriştarih = String.Format("{0:yyyy-MM-dd}",dateTimePicker1.Value);
            string çıkıştarih = String.Format("{0:yyyy-MM-dd}",dateTimePicker2.Value);
            Boolean durum = false;
            if (durum == false)
            {
                durum = true;
            }
            try
            {
                if (baglantı.State == ConnectionState.Closed)
                {
                    baglantı.Open();
                }

                SqlCommand eklemekomutu = new SqlCommand("insert into odano (odanumarası, ad,soyad,tckimlik,giriştarihi,çıkıştarihi,durum) values (@odano,@ad,@soyad,@tckimlik,@giriştarih,@çıkıştarih,@durum)", baglantı);
                eklemekomutu.Parameters.AddWithValue("@odano",odano);                
                eklemekomutu.Parameters.AddWithValue("@ad", ad);
                eklemekomutu.Parameters.AddWithValue("@soyad", soyad);
                eklemekomutu.Parameters.AddWithValue("@tckimlik", tckimlik);
                eklemekomutu.Parameters.AddWithValue("@giriştarih", giriştarih);
                eklemekomutu.Parameters.AddWithValue("@çıkıştarih", çıkıştarih);
                eklemekomutu.Parameters.AddWithValue("@durum",durum);

                
                
                eklemekomutu.ExecuteNonQuery();
                baglantı.Close();
               
                MessageBox.Show("eklendi");
            }
            catch (SqlException hata)
            {
                MessageBox.Show(hata.Message);
            }
        }
    }
    }

