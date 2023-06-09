﻿using Proyecto_Almacen_T5DN_2023.Models;
using System.Data.SqlClient;


namespace Proyecto_Almacen_T5DN_2023.DAO
{
    public class DA_Producto
    {
        public string cnn = "";
        public DA_Producto() 
        {
            var builder = new ConfigurationBuilder().SetBasePath(
                Directory.GetCurrentDirectory()).AddJsonFile("appSettings.json").Build();

            cnn = builder.GetSection("ConnectionStrings:cnDB").Value;
        }
        public List<Producto> ListProducto()
        {
            SqlConnection cn = new SqlConnection(cnn);
            List<Producto> list =new List<Producto>();
            SqlCommand cmd = new SqlCommand("", cn);
            cn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read()) 
            {
                list.Add(new Producto
                {
                    idProducto = rd.GetInt32(0),
                    nombreProducto = rd.GetString(1),
                    idProveedor = rd.GetString(2),
                    stock = rd.GetInt32(3),
                    precio = rd.GetDecimal(4),
                    idCat = rd.GetInt32(5)

                });
            }
            cn.Close();
            return list; 
        }

        public List<Categoria> ListCategory()
        {
            SqlConnection cn = new SqlConnection(cnn);
            List<Categoria> list = new List<Categoria>();
            SqlCommand cmd = new SqlCommand("list_Categoria", cn);
            cn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read()) 
            {
                list.Add(new Categoria
                {
                    idCat = rd.GetInt32(0),
                    nomCat = rd.GetString(1),
                });
            }
            cn.Close();
            return list;
        }

        public List<Proveedor> ListProveedor()
        {
            SqlConnection cn = new SqlConnection(cnn);
            List<Proveedor> list = new List<Proveedor>();
            SqlCommand cmd = new SqlCommand("list_Proveedor", cn);
            cn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                list.Add(new Proveedor
                {
                    idProveedor = rd.GetString(0),
                    nombreProveedor = rd.GetString(1)
                });
            }
            cn.Close();
            return list;
        }



    }
}
