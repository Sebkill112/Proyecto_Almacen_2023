using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Almacen_T5DN_2023.DAO;
using Proyecto_Almacen_T5DN_2023.Models;
using System.Data.SqlClient;

namespace Proyecto_Almacen_T5DN_2023.Controllers
{
    public class ProductoController : Controller
    {
        // cadena de connecion I
        private readonly string cadena_cn;
        public ProductoController(IConfiguration configuration)
        {
            cadena_cn = configuration.GetConnectionString("cnDB");
        }
        // cadena de connecion F
        public DA_Producto dao = new DA_Producto();
        IEnumerable<CProducto> listProduct()
        {
            List<CProducto> list = new List<CProducto>();

                using (SqlConnection cn = new SqlConnection(cadena_cn))
                {
                SqlCommand cmd = new SqlCommand("lis_Producto", cn);
                cn.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    list.Add(new CProducto
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
            }
            return list;
        }
        //
        IEnumerable<Producto> listProductReg()
        {
            List<Producto> list = new List<Producto>();

            using (SqlConnection cn = new SqlConnection(cadena_cn))
            {
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
                        idCat = rd.GetInt32(5),

                    });
                }
                cn.Close();
            }
            return list;
        }
        //
        public async Task<IActionResult> Index()
        { 
            return View(await Task.Run(()=> listProduct()));
        }
        // Create 
        public async Task<IActionResult> Registrar()
        {
            ViewBag.CATEGORIA = new SelectList(await Task.Run(() => dao.ListCategory()), "idCat", "nombreProveedor");
            ViewBag.PROVEEDOR = new SelectList(await Task.Run(() => dao.ListProveedor()), "idProveedor", "nomCat");
            return View(new Producto());
        }
        [HttpPost]
        public async Task<IActionResult> Registrar(Producto regProduct)
        {
            if (!ModelState.IsValid)
            {
                return View(regProduct);
            }
            string sms = string.Empty;
            using (SqlConnection cn = new SqlConnection(cadena_cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cn.Open();
                    int cant = cmd.ExecuteNonQuery();
                    sms = $"Se ha registrado con exito {cant} producto";
                }
                catch (Exception ex)
                {
                    sms = ex.Message;
                }
                ViewBag.MENSAJE = sms ;
                ViewBag.PROVEEDOR = new SelectList(await Task.Run(() => dao.ListProveedor()), "idProveedor", "nombreProveedor", regProduct.idProveedor);
                ViewBag.CATEGORIA = new SelectList(await Task.Run(() => dao.ListCategory()), "idCat", "nomCat", regProduct.idCat);
                return View(regProduct);

            }
        }
        //
        public async Task<IActionResult> Editar(int id)
        {
            Producto regProducto = listProductReg().Where(r => r.idProducto == id).FirstOrDefault();
            if(regProducto == null) 
            {
                return RedirectToAction("Index");
            }
            ViewBag.CATEGORIA = new SelectList(await Task.Run(() => dao.ListCategory()), "idCat", "nombreProveedor");
            ViewBag.PROVEEDOR = new SelectList(await Task.Run(() => dao.ListProveedor()), "idProveedor", "nomCat");
            return View(regProducto);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Producto regProducto)
        {
            if (!ModelState.IsValid)
            {
                return View(regProducto);
            }
            string sms = string.Empty;
            using (SqlConnection cn = new SqlConnection(cadena_cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cn.Open();
                    int cant = cmd.ExecuteNonQuery();
                    sms = $"Se ha actualizo con exito {cant} producto";
                }
                catch (Exception ex)
                {
                    sms = ex.Message;
                }
                ViewBag.MENSAJE = sms;
                ViewBag.PROVEEDOR = new SelectList(await Task.Run(() => dao.ListProveedor()), "idProveedor", "nombreProveedor", regProducto.idProveedor);
                ViewBag.CATEGORIA = new SelectList(await Task.Run(() => dao.ListCategory()), "idCat", "nomCat", regProducto.idCat);
                return View(regProducto);

            }
        }

        public IActionResult Eliminar(int id)
        {
            Producto delProducto = listProductReg().Where(e => e.idProducto == id).FirstOrDefault();
            if (delProducto == null)
            {
                return RedirectToAction("Index");
            }
            return View(delProducto);
        }
        [HttpPost]
        public IActionResult Eliminar(Producto delete)
        {
            string sms = string.Empty;
            using (SqlConnection cn = new SqlConnection(cadena_cn))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("", cn);
                    cmd.Parameters.AddWithValue("", delete.idProducto);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cn.Open(); 
                    int cant = cmd.ExecuteNonQuery();
                    cn.Close();
                    sms = $"Se ha eliminado {cant} Producto exitosamente"; 
                }
                catch (Exception ex)
                {
                    sms = ex.Message;
                }
                ViewBag.MENSAJE = sms;
                return View();
            }
        }
    }
  
}
