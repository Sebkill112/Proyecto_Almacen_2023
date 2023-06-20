using Microsoft.AspNetCore.Mvc;
using Proyecto_Almacen_T5DN_2023;
using Proyecto_Almacen_T5DN_2023.DAO;
using Proyecto_Almacen_T5DN_2023.Models;
using System.Data.SqlClient;

namespace Proyecto_Almacen_T5DN_2023.Controllers
{
    public class ProductoController : Controller
    {
        DA_Producto dao = new DA_Producto();
        public IConfiguration Configuration { get;}
        public ProductoController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListProductos()
        { 
            return View(dao.ListProducto());
        }
    }
}
