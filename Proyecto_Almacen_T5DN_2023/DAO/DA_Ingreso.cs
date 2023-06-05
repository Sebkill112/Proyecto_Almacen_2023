using Proyecto_Almacen_T5DN_2023.Models;
using System.Data.SqlClient;

namespace Proyecto_Almacen_T5DN_2023.DAO
{
    public class DA_Ingreso
    {

        public string cnn = "";


        public DA_Ingreso()
        {
            var builder = new ConfigurationBuilder().SetBasePath(
                Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json").Build();

            cnn = builder.GetSection("ConnectionStrings:cnDB").Value;
        }

        public List<Producto> ListarProductosIngreso()
        {


            SqlConnection cn = new SqlConnection(cnn);
            List<Producto> lista = new List<Producto>();
            SqlCommand cmd = new SqlCommand("ListarProductosIngreso", cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Producto
                {
                    idProducto = reader.GetInt32(0),
                    nombreProducto = reader.GetString(1)
  
                });
            }

            cn.Close();
            return lista;
        }

        public Producto BuscarProductosIngreso(string cod)
        {


            SqlConnection cn = new SqlConnection(cnn);
             Producto prod = new Producto();
            SqlCommand cmd = new SqlCommand("sp_buscarProducto", cn);
            cmd.Parameters.AddWithValue("@cod", cod);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                prod.idProducto = reader.GetInt32(0);
                prod.nombreProducto = reader.GetString(1);
                prod.idProveedor = reader.GetString(2);
                prod.stock = reader.GetInt32(3);
                prod.precio = reader.GetDecimal(4);
                prod.idCat = reader.GetInt32(5);

            }
            cn.Close();
            return prod;
        }



    }
}
