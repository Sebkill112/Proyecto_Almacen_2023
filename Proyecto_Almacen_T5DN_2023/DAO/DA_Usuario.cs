using Proyecto_Almacen_T5DN_2023.Models;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Proyecto_Almacen_T5DN_2023.DAO
{
    public class DA_Usuario
    {
        public string cnn = ""; 
        

        public DA_Usuario()
        {
            var builder = new ConfigurationBuilder().SetBasePath(
                Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json").Build();

            cnn = builder.GetSection("ConnectionStrings:cnDB").Value;
        }

        public List<Usuario> ListarUsuarios()
        {
           

            SqlConnection cn = new SqlConnection(cnn);
            List<Usuario> lista = new List<Usuario>();
            SqlCommand cmd = new SqlCommand("usp_listarUsuarios", cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Usuario
                    {
                        idUsuario = reader.GetInt32(0),
                        nombre = reader.GetString(1),
                        correo = reader.GetString(2),
                        clave = reader.GetString(3),
                        des_Rol = reader.GetString(4)
                    });
                }

            cn.Close();
            return lista;
        }

        
    }
}
