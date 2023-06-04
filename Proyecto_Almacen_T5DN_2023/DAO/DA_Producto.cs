
using Proyecto_Almacen_T5DN_2023.Models;
using System.Data.SqlClient;

namespace Proyecto_Almacen_T5DN_2023.DAO
{
    public class DA_Producto
    {
        public string cnn = "";
        public DA_Producto() 
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json").Build();
            cnn = builder.GetSection("ConnectionStrings:cnDB").Value;
        }
        public List<Producto> ListarProductos() 
        {
            SqlConnection cn = new SqlConnection(cnn);
            List<Producto> lista = new List<Producto>();
            SqlCommand cmd = new SqlCommand("usp_listarProductos", cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Producto
                {
                    idProducto = reader.GetString(0),
                    nombreProducto = reader.GetString(1),
                    descripcionP = reader.GetString(2),
                    nombreProveedor = reader.GetString(3),
                    stock = reader.GetInt32(4),
                    precio = reader.GetDecimal(5)
                });
            }
            cn.Close();
            return lista;
        }
        public List<Proveedor> ListarProveedores() 
        {
            SqlConnection cn = new SqlConnection(cnn);
            List<Proveedor> lista = new List<Proveedor>();
            SqlCommand cmd = new SqlCommand("usp_ObtenerProveedor", cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Proveedor
                {
                    idProveedor = reader.GetString(0),
                    nombreProveedor = reader.GetString(1)
                });
            }
            cn.Close();
            return lista;
        }




        }
}
