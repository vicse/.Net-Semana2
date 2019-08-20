using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Sem2Ejemplo1
{
    public partial class Ejercicio1 : Form
    {
        public Ejercicio1()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["asd"].ConnectionString);

        public void ListaNombres()
        {
            using (SqlCommand cmd = new SqlCommand("Usp_ListaEmpleados", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    //Rellenar con Datatable
                    DataTable df = new DataTable();

                    //El método fill cargar los datos del procedimiento almacenado
                    da.Fill(df);
                    //Enviar los datos al combo box
                    CboNombreE.DataSource = df;
                    CboNombreE.DisplayMember = "Nombre";
                    CboNombreE.ValueMember = "IdEmpleado";

                }
            }
        }


        private void Ejercicio1_Load(object sender, EventArgs e)
        {
            ListaNombres();

        }

        private void CboNombreE_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (SqlCommand cmd = new SqlCommand("Usp_ListaEmpleados_Filtro", cn))           
            {
                using (SqlDataAdapter Da = new SqlDataAdapter())
                {
                    Da.SelectCommand = cmd;
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.AddWithValue("@idEmpleado", CboNombreE.SelectedValue);

                    DataTable dt = new DataTable();
                    Da.Fill(dt);
                    DgEmpleados.DataSource = dt;

                }

            }

        }
    }
}
