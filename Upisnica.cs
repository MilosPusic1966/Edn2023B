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
        public Upisnica()
        {
            InitializeComponent();
        }

        private void Upisnica_Load(object sender, EventArgs e)
        {
            SqlConnection veza = konekcija.povezi();
            SqlDataAdapter adapter = new SqlDataAdapter("Select id, ime+' '+prezime as naziv from osoba", veza);
            DataTable dtOsoba = new DataTable();
            adapter.Fill(dtOsoba);
            comboBox2.DataSource = dtOsoba;
            comboBox2.ValueMember = "id";
            comboBox2.DisplayMember = "naziv";
            SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT upisnica.id, osoba_id, odeljenje_id, ime+prezime as naziv, STR(razred)+indeks as gde FROM upisnica JOIN odeljenje ON odeljenje_id = odeljenje.id JOIN osoba ON osoba.id = osoba_id", veza);
            dtUpisnica = new DataTable();
            adapter2.Fill(dtUpisnica);
            dataGridView1.DataSource = dtUpisnica;
            dataGridView1.AllowUserToAddRows = false;
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            int broj_sloga = dataGridView1.CurrentRow.Index;
            if (dtUpisnica.Rows.Count != 0 && broj_sloga >=0)
            {
                comboBox2.SelectedValue = dataGridView1.Rows[broj_sloga].Cells["osoba_id"].Value.ToString();
            }
        }
    }
}
