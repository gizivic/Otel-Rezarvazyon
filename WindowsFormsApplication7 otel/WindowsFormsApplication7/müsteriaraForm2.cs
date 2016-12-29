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
    public partial class müsteriaraForm2 : Form
    {
        public SqlConnection baglantı;
        public müsteriaraForm2()
        {
            InitializeComponent();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection baglantı = new SqlConnection("Data Source=PC\\SA; Initial Catalog=otel; Integrated Security=true");

            if (baglantı.State == ConnectionState.Closed)
            {
                baglantı.Open();
            }
            

            int tc=int.Parse(textBox1.Text);
            SqlCommand müsteriarakomut = new SqlCommand("select * from odano where tckimlik='"+tc+"'", baglantı);

            müsteriarakomut.ExecuteNonQuery();
          
           SqlDataAdapter tablo = new SqlDataAdapter(müsteriarakomut);

            DataTable veriler = new DataTable();
            tablo.Fill(veriler);

            dataGridView1.DataSource = veriler;
            baglantı.Close();
}


        }
    }

