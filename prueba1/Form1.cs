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
                errorProvider1.Clear();
                Boolean error = false;
                if (txtNombre.Text == "")
                {
                    error = true;
                    errorProvider1.SetError(txtNombre, "El campo es requerido");
                }
                if (txtApellidoP.Text == "")
                {
                    error = true;
                    errorProvider1.SetError(txtApellidoP, "El campo es requerido");
                }
                if (txtApellidoM.Text == "")
                {
                    error = true;
                    errorProvider1.SetError(txtApellidoM, "El campo es requerido");
                }
                if (txtCalle.Text == "")
                {
                    error = true;
                    errorProvider1.SetError(txtCalle, "El campo es requerido");
                }
                if (txtNumero.Text == "")
                {
                    error = true;
                    errorProvider1.SetError(txtNumero, "El campo es requerido");
                }
                if (txtColonia.Text == "")
                {
                    error = true;
                    errorProvider1.SetError(txtColonia, "El campo es requerido");
                }
                if (txtId.Text == "")
                {
                    error = true;
                    errorProvider1.SetError(txtId, "El campo es requerido");
                }
                if (dateTimePickerFecha.Text == "")
                {
                    error = true;
                    errorProvider1.SetError(dateTimePickerFecha, "El campo es requerido");
                }

                if (!error)
                {
                    conexion.Open();
                    string query = "INSERT INTO persons (nombre, apellido_paterno, apellido_materno) " +
                                    "VALUES ('" + txtNombre.Text + "', '" + txtApellidoP.Text + "', '" + txtApellidoM.Text + "');" +
                                    "SET @lastidPerson = LAST_INSERT_ID();" +
                                    "INSERT INTO addresses (calle, numero, colonia) " +
                                    "VALUES ('" + txtCalle.Text + "', '" + txtNumero.Text + "', '" + txtColonia.Text + "');" +
                                    "SET @lastidAddress = LAST_INSERT_ID();" +
                                    "INSERT INTO distributors (id_distributor, fecha_registro, id_person, id_address)" +
                                    "VALUES('" + txtId.Text + "', '" + dateTimePickerFecha.Value.ToString("yyyy-MM-dd") + "', @lastidPerson, @lastidAddress)";
                    MySqlCommand comando = new MySqlCommand(query, conexion);
                    comando.ExecuteNonQuery();                    
                    Clean(this);
                    dateTimePickerFecha.Value = DateTime.Now;
                    MessageBox.Show("Se ha guardado el distribuidor correctamente");
                }
            }
            catch (MySqlException error)
            {
                string message = "";
                switch(error.Number) {
                    case 1062:
                        message = "El ID " + txtId.Text + " ya ha sido registrado";
                        break;
                }
                MessageBox.Show("Ocurrió un errror. " + message);
            }
            conexion.Close();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "CALL getDistributor('" + txtBuscar.Text + "')";
                MySqlCommand comando = new MySqlCommand(query, conexion);
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                dataTable.DataSource = tabla;
            } catch(Exception)
            {
                MessageBox.Show("Ocurrió un errror");
            }
            
        }


        public static void Clean(Form form)
        {
            // Checar todos los textbox del formulario
            foreach (Control oControls in form.Controls)
            {
                if (oControls is TextBox)
                {
                    oControls.Text = ""; // Eliminar el texto del TextBox
                }
            }
        }


        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            //para tecla backspace
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            //si no cumple nada de lo anterior que no lo deje pasar
            else
            {
                e.Handled = true;
            }
        }


        private void letras_KeyPress(object sender, KeyPressEventArgs e)
        {
            //condicion para solo letras
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            //para backspace
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            //para que admita tecla de espacio
            else if (char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            //si no cumple nada de lo anterior que no lo deje pasar
            else
            {
                e.Handled = true;
            }

        }

 
        private void letrasynumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            //condicion para solo letras
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            //condicion para solo números
            else if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            //para backspace
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            //para que admita tecla de espacio
            else if (char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            //si no cumple nada de lo anterior que no lo deje pasar
            else
            {
                e.Handled = true;
            }
        }
    }
}
