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
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["asd"].ConnectionString);

        //Crear método para la lista de los años en el combo box

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
                    CboAnios.DataSource = df;
                    CboAnios.DisplayMember = "Anios";
                    CboAnios.ValueMember = "Anios";
                    
                }
            }
        }

       


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //LLamamos al método
            ListaAnios();

        }

        private void CboAnios_SelectionChangeCommitted(object sender, EventArgs e)
        {
            using (SqlCommand cmd =  new SqlCommand("Usp_Lista_Pedidos_Anios", cn))
            {
                using (SqlDataAdapter Da = new SqlDataAdapter())
                {
                    Da.SelectCommand = cmd;
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.AddWithValue("@anio", CboAnios.SelectedValue);
                    using (DataSet df = new DataSet())
                    {
                        Da.Fill(df, "Pedidos");
                        //Mostrar los datos en el Datagridview
                        DgPedidos.DataSource = df.Tables["Pedidos"];
                        lblNroPedidos.Text = df.Tables["Pedidos"].Rows.Count.ToString();
                    }
                }
            }
        }

        private void DgPedidos_DoubleClick(object sender, EventArgs e)
        {
            //Capturar la columna del pedido
            int Codigo;
            Codigo = Convert.ToInt32(DgPedidos.CurrentRow.Cells[0].Value);
            using (SqlCommand cmd= new SqlCommand("Usp_Detalle_Pedido", cn))
            {
                using (SqlDataAdapter Da = new SqlDataAdapter())
                {
                    Da.SelectCommand = cmd;
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.AddWithValue("@idpedido", Codigo);
                    using (DataSet df= new DataSet())
                    {
                        Da.Fill(df, "Detalles");
                        //Mostrar los datos en el Datagridview
                        DgDetalle.DataSource = df.Tables["Detalles"];
                        lblMonto.Text = df.Tables["Detalles"].Compute("Sum(Monto)", "").ToString();
                    }

                }
            }
            
        }
    }
}
