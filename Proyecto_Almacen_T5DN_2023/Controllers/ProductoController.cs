using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Almacen_T5DN_2023.DAO;
using Proyecto_Almacen_T5DN_2023.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace Proyecto_Almacen_T5DN_2023.Controllers
{
    public class ProductoController : Controller
    {
        DA_Producto dao = new DA_Producto();
        public IConfiguration Configuration { get; }
        public ProductoController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListadoProducto() 
        {
            return View(dao.ListarProductos());
        }
        public IActionResult RegistrarProducto()
        {
            RProducto obj = new RProducto();
            ViewBag.Proveedor = new SelectList(dao.ListarProveedores(), "idProveedor",
                                                "nombreProveedor");
            return View(obj);
        }
        [HttpPost]
        public IActionResult RegistrarProducto(RProducto producto)            
        {
            using (SqlConnection con = new(Configuration["ConnectionStrings:cnDB"]))
            {
                using (SqlCommand cmd = new("SP_NuevoProducto", con))
                { 
                    cmd.CommandType= System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idProducto", System.Data.SqlDbType.VarChar).Value = producto.idProducto;
                    cmd.Parameters.Add("@nombreP", System.Data.SqlDbType.VarChar).Value = producto.nombreProducto;
                    cmd.Parameters.Add("@descripcion", System.Data.SqlDbType.VarChar).Value = producto.descripcionP;
                    cmd.Parameters.Add("@idProveedor", System.Data.SqlDbType.Int).Value = producto.idProveedor;
                    cmd.Parameters.Add("@stock", System.Data.SqlDbType.Int).Value = producto.stock;
                    cmd.Parameters.Add("@precio", System.Data.SqlDbType.Decimal).Value = producto.precio;
                    con.Open();
                    cmd.ExecuteNonQuery();  
                    con.Close();
                }
                ViewData["MENSAJE"] = "Producto registrado";
                return View("Index","Producto");
            }
        }
    }
}
