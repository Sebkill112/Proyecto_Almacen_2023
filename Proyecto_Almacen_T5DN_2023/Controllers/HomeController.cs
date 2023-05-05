using Microsoft.AspNetCore.Mvc;
using Proyecto_Almacen_T5DN_2023.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;



namespace Proyecto_Almacen_T5DN_2023.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize(Roles = "Administrador,Proveedor")]
        public IActionResult Ingreso()
        {
            return View();
        }
        [Authorize(Roles = "Administrador,Asistente")]
        public IActionResult Egreso()
        {
            return View();
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult Productos()
        {
            return View();
        }
        [Authorize(Roles = "Administrador,Asistente")]
        public IActionResult Clientes()
        {
            return View();
        }
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}