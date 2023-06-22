using Proyecto_Almacen_T5DN_2023.Models;
using System.Data;
using System.Data.SqlClient;

namespace Proyecto_Almacen_T5DN_2023.DAO
{
    public class DA_Clientes
    {
        public string cnn = "";

        public DA_Clientes()
        {
            var builder = new ConfigurationBuilder().SetBasePath(
                Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json").Build();

            cnn = builder.GetSection("ConnectionStrings:cnDB").Value;
        }
        public bool Eliminar(string id)
        {
            bool rpta;

            try
            {
                SqlConnection cn = new SqlConnection(cnn);
                SqlCommand cmd = new SqlCommand("usp_clientes_delete", cn); 
                cmd.Parameters.AddWithValue("@idCliente", id );
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                cn.Open();
                rpta = true;


            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }
    }
}
