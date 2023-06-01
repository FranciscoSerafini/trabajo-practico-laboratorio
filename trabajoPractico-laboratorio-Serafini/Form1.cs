using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trabajoPractico_laboratorio_Serafini
{
    public partial class Form1 : Form
    {
        clsEncuentas objEncuestas;
        clsLocalidad objLocalidad;
        clsProfesion objProefesion;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            objEncuestas = new clsEncuentas();
            objLocalidad = new clsLocalidad();
            objProefesion = new clsProfesion();
            //llenados de combos
            objLocalidad.Llenado(cmbLocalidad);
            objProefesion.Llenado(cmbProfesion);
            //traemos las tablas
            DataTable TablaProfe = objProefesion.getAll();
            DataTable TablaLoca = objLocalidad.getAll();
            DataTable TablaEncue = objEncuestas.GetAll();

            //buscamos el nombre de la localidad y profesion y los agregamos a la grilla
            foreach (DataRow columna in TablaProfe.Rows)
            {
                dataGridView1.Columns.Add("Profesion", columna.ItemArray[1].ToString());
            }
            foreach (DataRow columna in TablaLoca.Rows)
            {
                dataGridView1.Rows.Add(columna.ItemArray[1].ToString());
            }
            foreach (DataRow dr in TablaEncue.Rows)
            {
                int localidadId = Convert.ToInt32(dr.ItemArray[0]);
                int profesionId = Convert.ToInt32(dr.ItemArray[1]);

                
                string localidad = objLocalidad.Busqueda(localidadId);
                string profesion = objProefesion.Busqueda(profesionId);

                int columnaProfesionIndex = -1;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (dataGridView1.Columns[i].HeaderText == profesion)
                    {
                        columnaProfesionIndex = i;
                        break;
                    }
                }

                if (columnaProfesionIndex != -1)
                {
                    foreach (DataGridViewRow filaGrilla in dataGridView1.Rows)
                    {
                        if (filaGrilla.Cells[0].Value.ToString() == localidad)
                        {
                            filaGrilla.Cells[columnaProfesionIndex].Value = dr["cantidad"];
                        }
                    }
                }
            }

            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoResizeRows();


        }

        private void cmdRegistrar_Click(object sender, EventArgs e)
        {
            objLocalidad = new clsLocalidad();
            objLocalidad.IdLocalidad = Convert.ToInt32(txtIdLocalidad.Text);
            objLocalidad.NombreLocalidad = txtLocalidad.Text;
            objLocalidad.Registro();
            if (objLocalidad.IdLocalidad == 0)
            {
                MessageBox.Show("El id de la localidad a ingresar ya existe");
            }
            else
            {
                txtLocalidad.Text = "";
                txtIdLocalidad.Text = "";
                txtLocalidad.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            objProefesion = new clsProfesion();
            objProefesion.IdProfesion = Convert.ToInt32(txtIdProfesion.Text);
            objProefesion.NombreProfesion = txtProfesion.Text;
            objProefesion.Registro();
            if (objProefesion.IdProfesion == 0)
            {
                MessageBox.Show("El id de la profesion a ingresar ya existe");
            }
            else
            {
                txtProfesion.Text = "";
                txtIdProfesion.Text = "";
                txtProfesion.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
             objEncuestas = new clsEncuentas();

            bool valor = objEncuestas.Actualizar((int)cmbLocalidad.SelectedValue, (int)cmbProfesion.SelectedValue, Convert.ToInt32(txtCantidad.Text));
            if (valor == true)
            {
                txtCantidad.Text = "";
               cmbLocalidad.SelectedIndex = -1;
                cmbProfesion.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Ya fue registrado", "ERORR");
            }
        }
    }
}
