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
    public partial class Ejercicio2 : Form
    {
        public Ejercicio2()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["asd"].ConnectionString);

        private void Ejercicio2_Load(object sender, EventArgs e)
        {
            ListaPedidos();
        }

        public void ListaPedidos()
        {
            using (SqlCommand cmd = new SqlCommand("Usp_ListaPedidos", cn))
            {
                using (SqlDataAdapter Da = new SqlDataAdapter())
                {
                    Da.SelectCommand = cmd;
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    
                    Da.Fill(dt);
                    //Mostrar los datos en el Datagridview
                    DgPedidos.DataSource = dt;            
                    
                }
            }
        }

        private void DgPedidos_DoubleClick(object sender, EventArgs e)
        {
            //Capturar la columna del pedido
            int Codigo;
            Codigo = Convert.ToInt32(DgPedidos.CurrentRow.Cells[0].Value);
            using (SqlCommand cmd = new SqlCommand("Usp_Detalle_Pedido", cn))
            {
                using (SqlDataAdapter Da = new SqlDataAdapter())
                {
                    Da.SelectCommand = cmd;
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.AddWithValue("@idpedido", Codigo);


                    DataTable dt = new DataTable();
                    
                    Da.Fill(dt);
                    //Mostrar los datos en el Datagridview
                    DgDatosProductos.DataSource = dt;
                    

                }
            }
        }
    }
}
