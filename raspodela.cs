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

namespace Edn2023B
{
    public partial class raspodela : Form
    {
        int tek_red;
        DataTable dtRaspodela;
        public raspodela()
        {
            InitializeComponent();
        }

        private void raspodela_Load(object sender, EventArgs e)
        {
            SqlConnection veza = konekcija.povezi();
            dtRaspodela = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT id, ime+' '+prezime AS nastavnik FROM osoba", veza);
            adapter.Fill(dtRaspodela);
            comboBox1.DataSource = dtRaspodela;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "nastavnik";
            comboBox1.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(comboBox1.SelectedValue.ToString());
        }
    }
}
