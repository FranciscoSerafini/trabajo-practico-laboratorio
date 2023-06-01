using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace trabajoPractico_laboratorio_Serafini
{
    internal class clsProfesion
    {
        private OleDbConnection conector;
        private OleDbCommand comando;
        private OleDbDataAdapter adaptador;
        private DataTable tabla;

        //variables de las propiedas
        private string Nombre;
        private int Id;

        //propiedades 
        public string NombreProfesion
        {
            get { return Nombre; }
            set { Nombre = value; }
        }
        public int IdProfesion
        {
            get { return Id; }
            set { Id = value; }
        }

        public clsProfesion()
        {
            conector = new OleDbConnection(Properties.Settings.Default.CADENA);
            comando = new OleDbCommand();

            comando.Connection = conector;
            comando.CommandType = CommandType.TableDirect;
            comando.CommandText = "Profesiones";

            adaptador = new OleDbDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);

            DataColumn[] dc = new DataColumn[1];
            dc[0] = tabla.Columns["profesion"];
            tabla.PrimaryKey = dc;
        }

        public void Registro()
        {
            DataRow filaBusqueda = tabla.Rows.Find(Id);
            if (filaBusqueda == null)
            {
                DataRow fila = tabla.NewRow();
                fila["nombre"] = Nombre;
                fila["profesion"] = Id;
                tabla.Rows.Add(fila);
                OleDbCommandBuilder cb = new OleDbCommandBuilder();
                adaptador.Update(tabla);

            }
            else
            {
                Id = 0;
                Nombre = "";
            }

        }
        public DataTable getAll()
        {
            return tabla;
        }
        public void Llenado(ComboBox combo)
        {
            combo.DisplayMember = "nombre";
            combo.ValueMember = "profesion";
            combo.DataSource = tabla;
        }
        public string Busqueda(int profesion)
        {
            DataRow[] filasEncontradas = tabla.Select("profesion = " + profesion);
            //select nos permite buscar las filas que coincidan con el ID
            if (filasEncontradas.Length > 0)
            {
                return filasEncontradas[0][1].ToString();
            }
            else
            {
                return "";
            }
        }

    }
}
