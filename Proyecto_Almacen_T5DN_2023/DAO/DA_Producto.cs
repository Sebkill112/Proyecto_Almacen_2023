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
        public List<CProducto> ListProducto() 
        {
            SqlConnection cn = new SqlConnection(cnn);
            List<CProducto> list = new List<CProducto>();
            SqlCommand cmd = new SqlCommand("dbo.usp_listProductos", cn);
            cn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read()) 
            {
                list.Add(new CProducto()
                {
                    idProducto = rd.GetInt32(0),
                    nombreProducto = rd.GetString(1),
                    nombreProveedor = rd.GetString(2),
                    stock = rd.GetInt32(3),
                    precio = rd.GetDecimal(4),
                    nomCat = rd.GetString(5),
                });
            }
            cn.Close(); 
            return list;
        }
    }
}
