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

namespace Edn2023B
{
    public partial class Osoba : Form
    {
        int tek_red;
        DataTable dtOsoba;
        
        public Osoba()
        {
            InitializeComponent();
        }
       
        private void osvezi()
        {
            SqlConnection veza = konekcija.povezi();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Osoba", veza);
            dtOsoba = new DataTable();
            adapter.Fill(dtOsoba);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dtOsoba = new DataTable();
            tek_red = 0;
            osvezi();
            Text_Load();
        }

        private void Text_Load()
        {
            if (dtOsoba.Rows.Count == 0)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
            }
            else
            {
                textBox1.Text = dtOsoba.Rows[tek_red][1].ToString();
                textBox2.Text = dtOsoba.Rows[tek_red][2].ToString();
                textBox3.Text = dtOsoba.Rows[tek_red][3].ToString();
                textBox4.Text = dtOsoba.Rows[tek_red][4].ToString();
                textBox5.Text = dtOsoba.Rows[tek_red][5].ToString();
                textBox6.Text = dtOsoba.Rows[tek_red][6].ToString();
                textBox7.Text = dtOsoba.Rows[tek_red][7].ToString();
            }
            if (tek_red == dtOsoba.Rows.Count - 1)
            {
                Btn_next.Enabled = false;
                Btn_last.Enabled = false;
            }            
            else
            {
                Btn_next.Enabled = true;
                Btn_last.Enabled = true;
            }
            if (tek_red == 0)
            {
                Btn_prev.Enabled = false;
                Btn_first.Enabled = false;
            }
            else
            {
                Btn_prev.Enabled = true;
                Btn_first.Enabled = true;
            }
        }

        private void Btn_prev_Click(object sender, EventArgs e)
        {
            tek_red--;
            Text_Load();
        }

        private void Btn_first_Click(object sender, EventArgs e)
        {
            tek_red = 0;
            Text_Load();
        }

        private void Btn_next_Click(object sender, EventArgs e)
        {
            tek_red++;
            Text_Load();
        }

        private void Btn_last_Click(object sender, EventArgs e)
        {
            tek_red=dtOsoba.Rows.Count - 1;
            Text_Load();
        }

        private void Btn_ins_Click(object sender, EventArgs e)
        {
            string naredba = "INSERT INTO Osoba VALUES(";
            naredba = naredba + "'"+textBox1.Text+"','";
            naredba = naredba + textBox2.Text + "','";
            naredba = naredba + textBox3.Text + "','";
            naredba = naredba + textBox4.Text + "','";
            naredba = naredba + textBox5.Text + "','";
            naredba = naredba + textBox6.Text + "','";
            naredba = naredba + textBox7.Text + "')";
            textBox8.Text=naredba;
            SqlConnection veza = konekcija.povezi();
            SqlCommand ins_naredba = new SqlCommand(naredba, veza);
            veza.Open();
            ins_naredba.ExecuteNonQuery();
            veza.Close();
            osvezi();
            
        }

        private void Btn_upd_Click(object sender, EventArgs e)
        {
            String naredba = "UPDATE Osoba SET ";
            naredba = naredba + "ime='" + textBox1.Text + "',";
            naredba = naredba + "prezime='" + textBox2.Text + "',";
            naredba = naredba + "adresa='" + textBox3.Text + "',";
            naredba = naredba + "jmbg='" + textBox4.Text + "',";
            naredba = naredba + "email='" + textBox5.Text + "',";
            naredba = naredba + "pass='" + textBox6.Text + "',";
            naredba = naredba + "uloga='" + textBox7.Text + "'";
            naredba = naredba + " WHERE id=" + dtOsoba.Rows[tek_red][0];
            SqlConnection veza = konekcija.povezi();
            SqlCommand ins_naredba = new SqlCommand(naredba, veza);
            textBox8.Text = naredba;
            try {
                veza.Open();
                ins_naredba.ExecuteNonQuery();
            } catch 
            {
                MessageBox.Show("Greska");
            }
            veza.Close();
            osvezi();
            Text_Load();
        }

        private void Btn_del_Click(object sender, EventArgs e)
        {
            string naredba = "DELETE FROM Osoba WHERE id="+dtOsoba.Rows[tek_red][0];
            SqlConnection veza = konekcija.povezi();
            SqlCommand ins_naredba = new SqlCommand(naredba, veza);
            veza.Open();
            ins_naredba.ExecuteNonQuery();
            veza.Close();
            osvezi();
            if (tek_red != 0)
            {
                tek_red--;
            }
            Text_Load();
        }
    }
}
