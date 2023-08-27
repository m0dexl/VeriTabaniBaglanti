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
using System.Net;
using System.Xml.Linq;

namespace VeriTabaniBaglanti
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection connection;
        SqlDataAdapter dataAdapter;
        DataSet dataSet;

        private void Form1_Load(object sender, EventArgs e)
        {
            getir();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ekle();
        }
        
        public void getir()
        {
            connection = new SqlConnection("server=DESKTOP-DID83I7\\SQLEXPRESS; Initial Catalog=Okullar_db; Integrated Security=true"); // sqle baglantiyi hazirladik ilki sunucu adi ikincisi kullanacagimiz tablo ucuncusu guvenlikle ilgili
            dataAdapter = new SqlDataAdapter("Select * from Okul_ogr", connection); // alacagimiz datayi belirledik
            dataSet = new DataSet(); //
            connection.Open(); // sql sunucuna baglan
            dataAdapter.Fill(dataSet, "Okul_ogr"); // dataset icerisine dataadapter daki verileri aktardim
            dataGridView1.DataSource = dataSet.Tables["Okul_ogr"];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AllowUserToResizeColumns = true;
            connection.Close();
        }

        public void ekle()
        {
            //string name, surname, phone, address;
            //name = textBox1.Text.ToString();
            //surname = textBox2.Text.ToString();
            //phone = textBox3.Text.ToString();
            //address = textBox4.Text.ToString();
            //string query1 = $"INSERT INTO Okul_ogr(first_name,last_name,tel_no,home_address)Values({name},{surname},{phone},{address})";
            string query2 = $"INSERT INTO Okul_ogr(first_name,last_name,tel_no,home_address)Values(@name,@surname,@phone,@address)";

            try
            {
                //connection = new SqlConnection("server=DESKTOP-DID83I7\\SQLEXPRESS; Initial Catalog=Okullar_db; Integrated Security=true"); // sqle baglantiyi hazirladik ilki sunucu adi ikincisi kullanacagimiz tablo ucuncusu guvenlikle ilgili
                //dataAdapter = new SqlDataAdapter(query1, connection);
                //dataSet = new DataSet(); //
                //connection.Open(); // sql sunucuna baglan
                //dataAdapter.Fill(dataSet, "Okul_ogr"); // dataset icerisine dataadapter daki verileri aktardim
                //dataGridView1.DataSource = dataSet.Tables["Okul_ogr"];
                //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                //dataGridView1.AllowUserToOrderColumns = true;
                //dataGridView1.AllowUserToResizeColumns = true;
                //connection.Close();

                connection = new SqlConnection("server=DESKTOP-DID83I7\\SQLEXPRESS; Initial Catalog=Okullar_db; Integrated Security=true"); // sqle baglantiyi hazirladik ilki sunucu adi ikincisi kullanacagimiz tablo ucuncusu guvenlikle ilgili

                SqlCommand command = new SqlCommand(query2, connection);
                command.Parameters.AddWithValue("@name",textBox1.Text);
                command.Parameters.AddWithValue("@surname", textBox2.Text);
                command.Parameters.AddWithValue("@phone", textBox3.Text);
                command.Parameters.AddWithValue("@address", textBox4.Text);
                connection.Open(); // sql sunucuna baglan

                command.ExecuteNonQuery();
                connection.Close();
                getir();

                MessageBox.Show("Kayıt işlemi başarılı");

            } catch(Exception ex)
            {
                MessageBox.Show("Hata oluştu" + ex.Message);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string queryDelete = "DELETE from Okul_ogr WHERE first_name=@name AND last_name=@surname AND tel_no=@phone";

            try
            {
                connection = new SqlConnection("server=DESKTOP-DID83I7\\SQLEXPRESS; Initial Catalog=Okullar_db; Integrated Security=true"); // sqle baglantiyi hazirladik ilki sunucu adi ikincisi kullanacagimiz tablo ucuncusu guvenlikle ilgili

                SqlCommand command = new SqlCommand(queryDelete, connection);
                command.Parameters.AddWithValue("@name", dataGridView1.CurrentRow.Cells[1].Value.ToString());
                command.Parameters.AddWithValue("@surname", dataGridView1.CurrentRow.Cells[2].Value.ToString());
                command.Parameters.AddWithValue("@phone", dataGridView1.CurrentRow.Cells[3].Value.ToString());
                command.Parameters.AddWithValue("@address", dataGridView1.CurrentRow.Cells[4].Value.ToString());

                connection.Open(); // sql sunucuna baglan

                command.ExecuteNonQuery();
                connection.Close();
                getir();

                MessageBox.Show("Kaydı silme işlemi başarılı");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu" + ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string queryUpdate = "UPDATE Okul_ogr SET first_name=@name, last_name=@surname, tel_no=@phone, home_address=@address WHERE student_id=@id";

            try
            {
                connection = new SqlConnection("server=DESKTOP-DID83I7\\SQLEXPRESS; Initial Catalog=Okullar_db; Integrated Security=true"); // sqle baglantiyi hazirladik ilki sunucu adi ikincisi kullanacagimiz tablo ucuncusu guvenlikle ilgili

                
                SqlCommand command = new SqlCommand(queryUpdate, connection);
                command.Parameters.AddWithValue("@id", Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
                command.Parameters.AddWithValue("@name",textBox1.Text);
                command.Parameters.AddWithValue("@surname", textBox2.Text);
                command.Parameters.AddWithValue("@phone",textBox3.Text);
                command.Parameters.AddWithValue("@address",textBox4.Text);
                connection.Open(); // sql sunucuna baglan

                command.ExecuteNonQuery();
                connection.Close();
                getir();

                MessageBox.Show("Kaydı güncelleme işlemi başarılı");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu" + ex.Message);
            }
        }
    }
}
