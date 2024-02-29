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
    public partial class Upisnica : Form
    {
        DataTable dtUpisnica;
        int broj_sloga;
        public Upisnica()
        {
            InitializeComponent();
        }

        private void popuni_grid()
        {
            SqlConnection veza = konekcija.povezi();
            // Upisnica datagridview
            SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT upisnica.id, osoba_id, odeljenje_id, ime+prezime as naziv, STR(razred)+indeks as gde FROM upisnica JOIN odeljenje ON odeljenje_id = odeljenje.id JOIN osoba ON osoba.id = osoba_id", veza);
            dtUpisnica = new DataTable();
            adapter2.Fill(dtUpisnica);
            dataGridView1.DataSource = dtUpisnica;
            dataGridView1.AllowUserToAddRows = false;
        }
        private void Upisnica_Load(object sender, EventArgs e)
        {
            SqlConnection veza = konekcija.povezi();
            // Osoba combo
            SqlDataAdapter adapter = new SqlDataAdapter("Select id, ime+' '+prezime as naziv from osoba", veza);
            DataTable dtOsoba = new DataTable();
            adapter.Fill(dtOsoba);
            comboBox2.DataSource = dtOsoba;
            comboBox2.ValueMember = "id";
            comboBox2.DisplayMember = "naziv";
            popuni_grid();
            
            // Odeljenje combo1
            SqlDataAdapter adapter3 = new SqlDataAdapter("Select id, str(razred)+'/'+indeks as naziv from odeljenje", veza);
            DataTable dtOdeljenje = new DataTable();
            adapter3.Fill(dtOdeljenje);
            comboBox1.DataSource = dtOdeljenje;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "naziv";
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            broj_sloga = dataGridView1.CurrentRow.Index;
            if (dtUpisnica.Rows.Count != 0 && broj_sloga >=0)
            {
                comboBox2.SelectedValue = dataGridView1.Rows[broj_sloga].Cells["osoba_id"].Value.ToString();
                comboBox1.SelectedValue = dataGridView1.Rows[broj_sloga].Cells["odeljenje_id"].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // INSERT INTO upisnica VALUES(2, 5) 
            string naredba = "INSERT INTO upisnica VALUES(";
            naredba = naredba+comboBox2.SelectedValue.ToString()+", ";
            naredba = naredba+comboBox1.SelectedValue.ToString()+")";
            SqlConnection veza = konekcija.povezi();
            SqlCommand ins_naredba = new SqlCommand(naredba, veza);
            veza.Open();
            ins_naredba.ExecuteNonQuery();
            veza.Close();
            popuni_grid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            string naredba = "DELETE FROM upisnica WHERE id=" + dataGridView1.Rows[broj_sloga].Cells["id"].Value.ToString();
            SqlConnection veza = konekcija.povezi();
            SqlCommand ins_naredba = new SqlCommand(naredba, veza);
            veza.Open();
            ins_naredba.ExecuteNonQuery();
            veza.Close();
            popuni_grid();
        }
    }
}
