using Microsoft.AspNetCore.Mvc;
using Proyecto_Almacen_T5DN_2023.DAO;
using Proyecto_Almacen_T5DN_2023.Models;

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
        Producto Search(int id)
        { 
            Producto b = dao.ListProducto().Where(p => p.idProducto == id).FirstOrDefault();

        }









        public IActionResult Index()
        {
            return View();
        }
    }
}
