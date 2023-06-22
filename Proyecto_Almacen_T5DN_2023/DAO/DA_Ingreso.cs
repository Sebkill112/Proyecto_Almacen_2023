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

 


    }
}
