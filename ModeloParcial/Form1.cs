using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModeloParcial
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Creo una lista de diccionarios de forma global y le precargo alunos diccionarios.
        List<Dictionary<string, object>> listaTareas = new List<Dictionary<string, object>>() {
    new Dictionary<string, object> { { "Title", "Study" }, { "Description", "Study for the exam" }, { "State", "Active" } },
    new Dictionary<string, object> { { "Title", "Gym" }, { "Description", "Go to the gym in the afternoon" }, { "State", "Active" } },
    new Dictionary<string, object> { { "Title", "Walk" }, { "Description", "Walk with the dog in the park" }, { "State", "Active" } },
    new Dictionary<string, object> { { "Title", "Take medicine" }, { "Description", "Take antibiotic" }, { "State", "Completed" } },
    new Dictionary<string, object> { { "Title", "Cook" }, { "Description", "Cook lunch" }, { "State", "Completed" } },
};


        private void Form1_Load(object sender, EventArgs e)
        {
            //Precargo el combo estado en el inicio del programa
            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("Active");
            cmbEstado.Items.Add("Completed");
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnFinalizar.Enabled = true;
            //Estado seleccionado en el combo lo guardo en una variable
            string estadoSeleccionado = cmbEstado.SelectedItem.ToString();

            //Limpio el combo tarea.
            cmbTareas.Items.Clear();

            //Recorro la lista de tareas preguntando por las tareas que tenga el mismo estado que el 
           //estado seleccionado en el combo.
          //Cargo en el combo el titulo de las tareas que cumplen la condicion.
            foreach (var item in listaTareas)
            {
                string estado = (string)item["State"];

                if (estadoSeleccionado == estado)
                {
                    cmbTareas.Items.Add(item["Title"]);
                }
            }

            //Bloqueo el boton de finalizar tarea si quiero filtrar por tareas ya finalizadas.
            if(estadoSeleccionado == "Completed")
            {
                btnFinalizar.Enabled = false;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Guardo en variables lo que me viene por formulario.
            string titulo = txtTitulo.Text;
            string descripcion = txtDescripcion.Text;


            //Si el titulo no es nulo y la descripcion menor a 50 creo un diccionario con las
           //variables y lo agrego a la lista de tareas.
            if (!string.IsNullOrWhiteSpace(titulo) && descripcion.Length < 50)
            {
             
                Dictionary<string, object> nuevaTarea = new Dictionary<string, object>
        
                {
            { "Title", titulo },
            { "Description", descripcion },
            { "State", "Active" }
        
                };

                listaTareas.Add(nuevaTarea);
            }
            else
            {
                //Manejo el error segun corresponda.
                if (string.IsNullOrWhiteSpace(titulo))
                {
                    MessageBox.Show("The title cannot be empty.");
                }
                if (descripcion.Length >= 50)
                {
                    MessageBox.Show("The description must be less than 50 characters long.");
                }
            }
            //Limpio las cajas de texto.
            txtTitulo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;

        }

        private void cmbTareas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Limpio el resumen cada que elijo una nueva tarea.
            lstResumen.Items.Clear();

            //Titulo de la tarea seleccionada en el combo.
            string tituloSeleccionado = cmbTareas.SelectedItem.ToString();

           //Recorro la lista de tareas buscando la que tenga el titulo seleecionado y cargo el resumen.
            foreach (var item in listaTareas)
            {
                string titulo = (string)item["Title"];

                if (tituloSeleccionado == titulo)
                {
                    lstResumen.Items.Add($"Name: {item["Title"]}");
                    lstResumen.Items.Add($"Description: {item["Description"]}");
                    lstResumen.Items.Add($"State: {item["State"]}");
                    break;

                }
            }

        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            // Verificar si hay un título seleccionado en el ComboBox
            if (cmbTareas.SelectedItem != null)
            {
                // Título de tarea seleccionado en el combo
                string tituloSeleccionado = cmbTareas.SelectedItem.ToString();

                // Recorro la lista buscando el título y cambio el estado a Finalizada
                foreach (var item in listaTareas)
                {
                    string titulo = (string)item["Title"];

                    if (tituloSeleccionado == titulo)
                    {
                        item["State"] = "Completed";
                        MessageBox.Show($"Task '{titulo}' completed");
                        break;
                    }
                }
            }
            else
            {
                // Manejar el caso donde no hay ningún título seleccionado
                MessageBox.Show("Please select a task from the list.");
            }




            lstResumen.Items.Clear();
            cmbEstado.Items.Clear();
            cmbTareas.Items.Clear();
            cmbEstado.Items.Add("Active");
            cmbEstado.Items.Add("Completed");



        }
    }
}
