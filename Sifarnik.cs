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
    public partial class Sifarnik : Form
    {
        public Sifarnik()
        {
            InitializeComponent();
        }
        public Sifarnik(string naziv)
        {
            naziv_tabele = naziv;
            InitializeComponent();
        }
        string naziv_tabele;
        DataTable tabela;
        SqlDataAdapter adapter;
        private void Sifarnik_Load(object sender, EventArgs e)
        {
            tabela = new DataTable();
            adapter = new SqlDataAdapter("SELECT * FROM "+naziv_tabele, konekcija.povezi());
            adapter.Fill(tabela);
            dataGridView1.DataSource = tabela;
            dataGridView1.Columns["id"].ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable menjano = tabela.GetChanges();
            // dataGridView2.DataSource = menjano;
            if (menjano != null )
            {
                adapter.UpdateCommand = new SqlCommandBuilder(adapter).GetUpdateCommand();
                adapter.Update(menjano);
            }
        }
    }
}
