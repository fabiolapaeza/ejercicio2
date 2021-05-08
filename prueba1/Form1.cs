using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prueba1
{
    public partial class Form1 : Form
    {
        MySqlConnection conexion = new MySqlConnection("Server=localhost; Database=prueba; Uid=root; Pwd=; Allow User Variables=True");
        public Form1()
        {
            InitializeComponent();
        }   

        private void Form1_Load(object sender, EventArgs e)
        {
           
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();
                string query = "INSERT INTO persons (nombre, apellido_paterno, apellido_materno) " +
                                "VALUES ('" + txtNombre.Text + "', '" + txtApellidoP.Text + "', '" + txtApellidoM.Text + "');" +
                                "SET @lastidPerson = LAST_INSERT_ID();" +
                                "INSERT INTO addresses (calle, numero, colonia) " +
                                "VALUES ('" + txtCalle.Text + "', '" + txtNumero.Text + "', '" + txtColonia.Text + "');" +
                                "SET @lastidAddress = LAST_INSERT_ID();" +
                                "INSERT INTO distributors (id_distributor, fecha_registro, id_person, id_address)" +
                                "VALUES('83JK', CURRENT_DATE, @lastidPerson, @lastidAddress)";
                MySqlCommand comando = new MySqlCommand(query, conexion);
                comando.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Se ha guardado el distribuidor correctamente");
            } catch(Exception)
            {
                MessageBox.Show("Ocurrió un errror");
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            MySqlCommand comando = new MySqlCommand("SELECT CONCAT(nombre, ' ', apellido_paterno, ' ', apellido_materno) AS 'Nombre Completo', " +
                                                    "calle AS Calle, numero AS Número, colonia AS Colonia FROM distributors " +
                                                    "INNER JOIN addresses on addresses.id_address = distributors.id_address " +
                                                    "INNER JOIN persons on persons.id_person = distributors.id_person", conexion);
            MySqlDataAdapter adaptador = new MySqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataTable.DataSource = tabla;
        }
    }
}
