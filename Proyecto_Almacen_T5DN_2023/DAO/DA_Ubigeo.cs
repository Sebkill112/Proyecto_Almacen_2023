using Proyecto_Almacen_T5DN_2023.Models;
using System.Data.SqlClient;

namespace Proyecto_Almacen_T5DN_2023.DAO
{
    public class DA_Ubigeo
    {
        public string cnn = "";


        public DA_Ubigeo()
        {
            var builder = new ConfigurationBuilder().SetBasePath(
                Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json").Build();

            cnn = builder.GetSection("ConnectionStrings:cnDB").Value;
        }

        public List<Departamento> ListarDepartamentos()
        {


            SqlConnection cn = new SqlConnection(cnn);
            List<Departamento> lista = new List<Departamento>();
            SqlCommand cmd = new SqlCommand("usp_ObtenerDepartamento", cn);
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Departamento
                {
                    CodigoDepartamento = reader.GetInt32(0),
                    Nombre = reader.GetString(1)
  
                });
            }

            cn.Close();
            return lista;
        }

        public List<Provincia> ListarProvincias(int codDepa)
        {


            SqlConnection cn = new SqlConnection(cnn);
            List<Provincia> lista = new List<Provincia>();
            SqlCommand cmd = new SqlCommand("usp_ObtenerProvincia", cn);
            cmd.Parameters.AddWithValue("@CodigoDepartamento", codDepa);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Provincia
                {
                    CodigoProvincia = reader.GetInt32(0),
                    Nombre = reader.GetString(1)

                });
            }

            cn.Close();
            return lista;
        }

        public List<Distrito> ListarDistritos(int codDepa)
        {


            SqlConnection cn = new SqlConnection(cnn);
            List<Distrito> lista = new List<Distrito>();
            SqlCommand cmd = new SqlCommand("usp_ObtenerDistrito", cn);
            cmd.Parameters.AddWithValue("@CodigoProvincia", codDepa);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Distrito
                {
                    CodigoDistrito = reader.GetInt32(0),
                    Nombre = reader.GetString(1)

                });
            }

            cn.Close();
            return lista;
        }
    }
}
