using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb; //Veritabanı bağlantı kütüphanesi // Emir Öztürk 2115101053 
namespace isci
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Veri Tabanı Değişkenlerini Tanımlama Bölümü
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=isci.accdb");
        OleDbCommand komut = new OleDbCommand();
        OleDbDataAdapter adaptor = new OleDbDataAdapter();      
        DataSet tasima = new DataSet();
        string oresim;

        //DataGridWiev de kayıtları listeleme bölümü
        void listele()
        {
            baglanti.Open();
            OleDbDataAdapter adaptor = new OleDbDataAdapter("Select * from isci", baglanti);
            adaptor.Fill(tasima, "isci");
            dataGridView1.DataSource = tasima.Tables["isci"];
            adaptor.Dispose();
            baglanti.Close();
        }
        private void label13_Click(object sender, EventArgs e)
        {
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
                i_resim.Text = openFileDialog1.FileName;
            }

            // "\" karakterinin kodunu alma bölümü
            int s = 92;
            string harf = ((char)s).ToString();

            // Resmin adresinin tersten yazdırama bölümü
            string yazi = i_resim.Text; 
            string metin = "";
            int yaziuzunlugu = yazi.Length;
            for (int i = yaziuzunlugu; i > 0; i--)
            {
            if (yazi.Substring(i - 1, 1) == harf)
            {
            break;
            }
            metin = metin + (yazi.Substring(i - 1, 1));
            }

            // Bulunan resim adını yazdırma bölümü
            int uzunluk = metin.Length; string kelime = "";
            for (int a = uzunluk; a > 0; a--)
            {
            kelime = kelime + (metin.Substring(a - 1, 1));
            }

            //resim adını resim kutusuna yazdırma bölümü
            i_resim.Text = "resimler/" + kelime;
            oresim = i_resim.Text;
            
        }

        //Kayıt Ekleme Butonu
        private void button1_Click(object sender, EventArgs e)
        {
            oresim = pictureBox1.ImageLocation;
            if ( i_tc.Text != "" && i_adi.Text != "" && i_soyadi.Text != "" && i_adresi.Text != "" && i_baslama_tarihi.Text != "" && i_sehir.Text != ""&& i_maas.Text != "" && i_resim.Text != "" && i_telefonu.Text != "")
            {
            komut.Connection = baglanti;
            komut.CommandText = "Insert Into isci(i_tc,i_adi,i_soyadi,i_mail,i_adresi,i_baslama_tarihi,i_cikis_tarihi,i_sehir,i_aciklama,i_telefonu,i_resim) Values ('" + i_tc.Text + "','" + i_adi.Text + "','" + i_soyadi.Text + "','" + i_mail.Text + "','" + i_adresi.Text + "','" + i_baslama_tarihi.Text + "','" + i_cikis_tarihi.Text + "','" + i_sehir.Text + "','" + i_aciklama.Text + "','" + i_telefonu.Text + "','" + i_resim.Text + "')";
            baglanti.Open();
            komut.ExecuteNonQuery();
            komut.Dispose();
            baglanti.Close();
            MessageBox.Show("Kayıt Tamamlandı!");
            tasima.Clear();
            listele();
            }
            else
            {
            MessageBox.Show("Boş alan geçmeyiniz!");
            }
        }

        //yeni kayıt ekleme
        private void button6_Click(object sender, EventArgs e)
        {
            i_id.Text = "";
            i_tc.Text = "";
            i_adi.Text = "";
            i_soyadi.Text = "";
            i_telefonu.Text = "";
            i_mail.Text = "";
            i_adresi.Text = "";
            i_baslama_tarihi.Text = "";
            i_cikis_tarihi.Text = "";
            i_sehir.Text = "";
            i_aciklama.Text = "";
            i_maas.Text = "";
            i_resim.Text = "";
            pictureBox1.ImageLocation = "";
        }

        //Kayıt Silme Bölümü
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult c;
            c = MessageBox.Show("Silmek istediğinizden emin misiniz?", "Uyarı!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (c == DialogResult.Yes)
            {
                baglanti.Open();
                komut.Connection = baglanti;
                komut.CommandText = "Delete from isci where i_id=" + textBox1.Text + "";
                komut.ExecuteNonQuery();
                komut.Dispose();
                baglanti.Close();
                tasima.Clear();
                listele();
            }
        }

        // Kayıt güncelleme bölümü
        private void button3_Click(object sender, EventArgs e)
        {
            komut = new OleDbCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "update isci set i_tc='" + i_tc.Text + "', i_adi='" + i_adi.Text + "', i_soyadi='" + i_soyadi.Text + "', i_telefonu='" + i_telefonu.Text + "', i_mail='" + i_mail.Text + "', i_adresi='" + i_adresi.Text + "', i_baslama_tarihi='" + i_baslama_tarihi.Text + "', i_cikis_tarihi='" + i_cikis_tarihi.Text + "', i_sehir='" + i_sehir.Text + "', i_aciklama='" + i_aciklama.Text + "', i_maas='" + i_maas.Text + "',i_resim='" + i_resim.Text + "' where i_id=" + i_id.Text + "";
      
            komut.ExecuteNonQuery();
            baglanti.Close();
            tasima.Clear();
            listele();
        }
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            i_id.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            i_tc.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            i_adi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            i_soyadi.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            i_telefonu.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            i_mail.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            i_adresi.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            i_baslama_tarihi.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            i_cikis_tarihi.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            i_sehir.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            i_aciklama.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            i_maas.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            i_resim.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.CurrentRow.Cells[12].Value.ToString();
        }
        
         //numarasına göre arama
        private void button4_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=isci.accdb"); con.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from isci where i_id=" + textBox2.Text + "", con);
            OleDbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i_id.Text = dr["i_id"].ToString();
                i_adi.Text = dr["i_adi"].ToString();
                i_soyadi.Text = dr["i_soyadi"].ToString();
                i_telefonu.Text = dr["i_telefonu"].ToString();
                pictureBox1.ImageLocation = dr["i_resim"].ToString();
                i_resim.Text = dr["i_resim"].ToString();
            }
            con.Close();
        }
    }
}