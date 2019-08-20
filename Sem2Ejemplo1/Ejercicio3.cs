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
    public partial class Ejercicio3 : Form
    {
        public Ejercicio3()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["asd"].ConnectionString);

        public void ListaAnios()
        {
            using (SqlCommand cmd = new SqlCommand("Usp_ListaAnios", cn))
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
                    CboAnio.DataSource = df;
                    CboAnio.DisplayMember = "Anios";
                    CboAnio.ValueMember = "Anios";

                }
            }
        }

        private void Ejercicio3_Load(object sender, EventArgs e)
        {
            ListaAnios();
        }
    }
}
