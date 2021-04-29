using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace kelimeogren
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\dogukaN\Desktop\dbSozluk.accdb");
        Random rast = new Random();
        int sure = 90;
        int kelime = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            
            getir();
            textBox2.Focus();
            textBox1.Enabled = false;
            label1.Visible = false;            
            MessageBox.Show("Bu sistemde kullanılan veritabanı 4000 adet kelime barındırmaktadır. Bazen karşınıza uzun açıklayıcı kelimeler çıkabilir.. Veritabanını düzenleyene kadar şimdilik böyle kalacak.. Herkese iyi çalışmalar:)", "Merhaba!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            timer1.Start();
        }
        void getir()
        {
            int sayi;
            sayi = rast.Next(0, 2400);
            label1.Text = sayi.ToString();
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Select * from sozluk where id=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", sayi);
            OleDbDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                textBox1.Text = dr[1].ToString();
                label1.Text = dr[2].ToString();
                label1.Text = label1.Text.ToLower();
            }
            baglanti.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = textBox2.Text.ToLower();
            textBox2.SelectionStart = textBox2.Text.Length;
            if (textBox2.Text == label1.Text)
            {
                kelime++;
                label7.Text = kelime.ToString();
                getir();
                textBox2.Clear();
            }
            label1.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sure--;
            lblsure.Text = sure.ToString();
            if (sure == 0)
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                timer1.Stop();
                System.Threading.Thread.Sleep(2000);
                DialogResult secenek = MessageBox.Show(label7.Text+"  adet kelime bildiniz.Tekrar Oynamak İster misiniz?", "Tebrikler!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (secenek == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (secenek == DialogResult.No)
                {
                    Application.Exit();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label1.Visible)
                label1.Visible = false;
            else
                label1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getir();
        }
    }
}
