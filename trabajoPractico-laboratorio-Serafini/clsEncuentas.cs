using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace trabajoPractico_laboratorio_Serafini
{
    internal class clsEncuentas
    {
        private OleDbConnection conector;
        private OleDbCommand comando;
        private OleDbDataAdapter adaptador;
        private DataTable tabla;

        public clsEncuentas()
        {
            conector = new OleDbConnection(Properties.Settings.Default.CADENA);
            comando = new OleDbCommand();

            comando.Connection = conector;
            comando.CommandType = CommandType.TableDirect;
            comando.CommandText = "Encuestas";

            adaptador = new OleDbDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);

            DataColumn[] dc = new DataColumn[2];
            dc[0] = tabla.Columns["localidad"];
            dc[1] = tabla.Columns["profesion"];
            tabla.PrimaryKey = dc;
        }
        public bool Actualizar(Int32 localidad, Int32 profesion, Int32 cantidad)
        {
            bool valor = true;
            Object[] clave = new Object[2];
            clave[0] = localidad;
            clave[1] = profesion;
            DataRow filabuscada = tabla.Rows.Find(clave);

            if (filabuscada == null)
            {
                DataRow fila = tabla.NewRow();
                fila["localidad"] = localidad;
                fila["profesion"] = profesion;
                fila["cantidad"] = cantidad;
                tabla.Rows.Add(fila);
                OleDbCommandBuilder cb = new OleDbCommandBuilder(adaptador);
                adaptador.Update(tabla);
            }
            else
            {
                valor = false;
            }
            return valor;
        }
        public DataTable GetAll()
        {
            return tabla;
        }
    }
}
