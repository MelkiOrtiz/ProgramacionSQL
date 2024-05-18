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
            int respuesta = personaje.CrearPersonaje(nombre, raza, nivelPoder);
            if (respuesta >0)
            {
                MessageBox.Show("Creado con exito");
                dataGridViewPersonajes.DataSource = personaje.LeerPersonajes();
            } else
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
                textBoxNombre.Text = nombre;
                comboBoxRaza.Text = raza;
                numericUpDownNivelPoder.Value = niverlPoder;
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
    }
}
