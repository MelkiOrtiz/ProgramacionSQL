using ProgramacionSQL.Data.DataAcces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProgramacionSQL
{
    public partial class Form1 : Form
    {
        //definir clase de instancia 
        private personajeDB personaje;
        // Lista de razas
        private string[] razasDragonBall = {
    "Android",
    "Bio-Android",
    "Humana",
    "Humano",
    "Majin",
    "Namekuseijin",
    "Saiyajin",
    "Saiyajin/Humano",
    "Saiyajin/Saiyajin"
};
        public Form1()
        {
            InitializeComponent();
            personaje = new personajeDB();
        }

        private void buttonPrueba_Click(object sender, EventArgs e)
        {
            if (personaje.ProbarConexion())
            {
                MessageBox.Show("Simoncho");
            }
            else
            {
                MessageBox.Show("Nel pastel");
            }
        }

        private void buttonCargar_Click(object sender, EventArgs e)
        {
            DataTable dt = personaje.LeerPersonajes();
            dataGridViewPersonajes.DataSource = dt;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void buttonCrear_Click(object sender, EventArgs e)
        {
            string nombre = textBoxNombre.Text;
            string raza = comboBoxRaza.Text;
            int nivelPoder = (int)numericUpDownNivelPoder.Value;
            DateTime fecha_creacion = DateTime.Now;
            string historia = textBoxHistoria.Text;
            int respuesta = personaje.CrearPersonaje(nombre, raza, nivelPoder, fecha_creacion, historia);
            if (respuesta > 0)
            {
                MessageBox.Show("Creado con exito");
                dataGridViewPersonajes.DataSource = personaje.LeerPersonajes();
            }
            else
            {
                MessageBox.Show("Algo hiciste mal");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxRaza.Items.AddRange(razasDragonBall);
        }

        private void BuscarPorId()
        {
            int idPersonajeBuscar = int.Parse(textBoxID.Text);
            DataTable personajeEncontrado = personaje.BuscarPersonajePorId(idPersonajeBuscar);
            if (personajeEncontrado.Rows.Count > 0)
            {
                string nombre = personajeEncontrado.Rows[0]["nombre"].ToString();
                string raza = personajeEncontrado.Rows[0]["raza"].ToString();
                int niverlPoder = int.Parse(personajeEncontrado.Rows[0]["nivel_poder"].ToString());
                DateTime fecha_creacion = (DateTime)personajeEncontrado.Rows[0]["fecha_creacion"];
                string historia = personajeEncontrado.Rows[0]["historia"].ToString();
                textBoxNombre.Text = nombre;
                comboBoxRaza.Text = raza;
                numericUpDownNivelPoder.Value = niverlPoder;
                dateTimePicker1.Value = fecha_creacion;
                textBoxHistoria.Text = historia;
            }
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            BuscarPorId();
        }

        private void textBoxID_Leave(object sender, EventArgs e)
        {
            BuscarPorId();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void buttonActualizar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBoxID.Text);
            string nombre = textBoxNombre.Text;
            string raza = comboBoxRaza.Text;
            int nivelPoder = (int)numericUpDownNivelPoder.Value;
            string historia = textBoxHistoria.Text;
            int respuesta = personaje.ActualizarPersonaje(id, nombre, raza, nivelPoder, historia);
            if (respuesta > 0)
            {
                MessageBox.Show("Si se pudo tilin");
                dataGridViewPersonajes.DataSource = personaje.LeerPersonajes();
            }
            else
            {
                MessageBox.Show("Algo hiciste mal");
            }
        }

        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBoxID.Text);
            int respuesta = personaje.BorrarPersonaje(id);
            if (respuesta > 0)
            {
                MessageBox.Show("Si se pudo mi rey");
                textBoxID.Clear();
                textBoxNombre.Clear();
                comboBoxRaza.SelectedIndex = -1;
                numericUpDownNivelPoder.Value = 0;
                textBoxHistoria.Clear();
                dataGridViewPersonajes.DataSource = personaje.LeerPersonajes();
            }
            else
            {
                MessageBox.Show("Algo hiciste mal");
            }
        }
    }
}
