using Microsoft.Data.SqlClient;
using Proyecto_Almacen_T5DN_2023.Models;

namespace Proyecto_Almacen_T5DN_2023.DAO
{
    public class DA_Egreso
    {

        public string cnn = "";

        public DA_Egreso()
        {
            var builder = new ConfigurationBuilder().SetBasePath(
                Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json").Build();

            cnn = builder.GetSection("ConnectionStrings:cnDB").Value;
        }

        public List<Producto> ListarProductosEgreso()
        {


            SqlConnection cn = new SqlConnection(cnn);
            List<Producto> lista = new List<Producto>();
            SqlCommand cmd = new SqlCommand("ListarProductosEgreso", cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Producto
                {
                    idProducto = reader.GetInt32(0),
                    nombreProducto = reader.GetString(1),
                    precio = reader.GetDecimal(2)

                });
            }

            cn.Close();
            return lista;
        }

    }
}
