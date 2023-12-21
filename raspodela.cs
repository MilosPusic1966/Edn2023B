using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Edn2023B
{
    public partial class raspodela : Form
    {
        int br_reda=0;
        DataTable dtRaspodela;
        public raspodela()
        {
            InitializeComponent();
        }
        private void FillDt()
        {
            SqlConnection veza = konekcija.povezi();
            dtRaspodela = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM raspodela", veza);
            adapter.Fill(dtRaspodela);
        }
        private void Combo_Load()
        {
            if (br_reda == dtRaspodela.Rows.Count - 1)
            {
                button6.Enabled = false;
                button7.Enabled = false;
            }
            else
            {
                button6.Enabled = true;
                button7.Enabled = true;
            }
            if (br_reda == 0)
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }

            SqlConnection veza = konekcija.povezi();
            DataTable dtOsoba = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT id, ime+' '+prezime AS nastavnik FROM osoba", veza);
            adapter.Fill(dtOsoba);
            comboBox1.DataSource = dtOsoba;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "nastavnik";
            comboBox1.SelectedValue = dtRaspodela.Rows[br_reda]["nastavnik_id"].ToString();

            DataTable dtPredmet = new DataTable();
            adapter = new SqlDataAdapter("SELECT * from predmet", veza);
            adapter.Fill(dtPredmet);
            comboBox2.DataSource = dtPredmet;
            comboBox2.ValueMember = "id";
            comboBox2.DisplayMember = "naziv";
            comboBox2.SelectedValue = dtRaspodela.Rows[br_reda]["predmet_id"].ToString();

            DataTable dtGodina = new DataTable();
            adapter = new SqlDataAdapter("SELECT * from skolska_godina", veza);
            adapter.Fill(dtGodina);
            comboBox3.DataSource = dtGodina;
            comboBox3.ValueMember = "id";
            comboBox3.DisplayMember = "naziv";
            comboBox3.SelectedValue = dtRaspodela.Rows[br_reda]["godina_id"].ToString();

            DataTable dtOdeljenje = new DataTable();
            adapter = new SqlDataAdapter("SELECT id, STR(razred)+'/'+indeks AS naziv from odeljenje", veza);
            adapter.Fill(dtOdeljenje);
            comboBox4.DataSource = dtOdeljenje;
            comboBox4.ValueMember = "id";
            comboBox4.DisplayMember = "naziv";
            comboBox4.SelectedValue = dtRaspodela.Rows[br_reda]["odeljenje_id"].ToString();
        }
        private void raspodela_Load(object sender, EventArgs e)
        {
            FillDt();
            Combo_Load();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            br_reda = 0;
            Combo_Load();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            br_reda++;
            Combo_Load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            br_reda--;
            Combo_Load();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            br_reda=dtRaspodela.Rows.Count-1;
            Combo_Load();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // INSERT
            string naredba = "INSERT INTO raspodela VALUES(";
            naredba = naredba + "'" + comboBox1.SelectedValue.ToString() + "','";
            naredba = naredba + comboBox3.SelectedValue.ToString() + "','";
            naredba = naredba + comboBox2.SelectedValue.ToString() + "','";
            naredba = naredba + comboBox4.SelectedValue.ToString() + "')";
            
            SqlConnection veza = konekcija.povezi();
            SqlCommand ins_naredba = new SqlCommand(naredba, veza);
            veza.Open();
            ins_naredba.ExecuteNonQuery();
            veza.Close();
            FillDt();
            Combo_Load();
        }
    }
}
