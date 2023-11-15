using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace AlemDaEntrega
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dgvCliente_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CarregarDadosBanco();
        }

        private void CarregarDadosBanco()
        {
            string conexao = "server=localhost;database=alemDaEntrega;uid=root;pwd=etec";
            MySqlConnection conexaoMYSQL = new MySqlConnection(conexao);
            conexaoMYSQL.Open();

            MySqlDataAdapter adapter = new MySqlDataAdapter("select * from tbCliente", conexaoMYSQL);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvCliente.DataSource = dt;

        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            string conexao = "server=localhost;database=alemDaEntrega;uid=root;pwd=etec";
            MySqlConnection conexaoMYSQL = new MySqlConnection(conexao);
            conexaoMYSQL.Open();
            MySqlCommand comando = new MySqlCommand("update tbCliente set nmCliente='" + txtNome.Text + "', endcliente='" + txtEndereco.Text + "' where idCliente=" + txtIdCliente.Text, conexaoMYSQL);
            comando.ExecuteNonQuery();
            MessageBox.Show("Dados alterados!!!");
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtIdCliente.Text = "";
            pnlAlterar.Visible = false;
            CarregarDadosBanco();
        }

        private void dgvCliente_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtIdCliente.Text = dgvCliente.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtNome.Text = dgvCliente.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtEndereco.Text = dgvCliente.Rows[e.RowIndex].Cells[2].Value.ToString();
            pnlAlterar.Visible = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtIdCliente.Text == "" || txtNome.Text == "" || txtEndereco.Text == "")
            {

                MessageBox.Show("Por favor complete todos os campos");
            }
            else
            {
                MySqlConnection mySql = new MySqlConnection("server=localhost;database=alemDaEntrega;uid=root;pwd=etec");
                mySql.Open();
                MySqlCommand comando = new MySqlCommand("Insert into tbCliente (idCliente, nmCliente, endCliente) values ('" + txtIdCliente.Text + "','" + txtNomeCliente.Text + "','" + txtEmailCliente.Text + "');", mySql);
                comando.ExecuteNonQuery();

                MessageBox.Show("Cliente registrado com sucesso!");
                txtNomeCliente.Text = "";
                txtIdCliente.Text = "";
                txtEmailCliente.Text = "";
                CarregarDadosBanco();
            }
        }
    }
}
