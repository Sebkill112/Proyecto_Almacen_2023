using Microsoft.AspNetCore.Mvc;
using Proyecto_Almacen_T5DN_2023.DAO;
using Proyecto_Almacen_T5DN_2023.Models;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using System.Xml.Linq;


namespace Proyecto_Almacen_T5DN_2023.Controllers
{
    public class IngresoController : Controller
    {
        private readonly IConfiguration _configuration; 
        
        public IngresoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DA_Ingreso dao = new DA_Ingreso();

       Producto buscar(int id = 0)
        {
            Producto reg = dao.ListarProductosIngreso().Where(p => p.idProducto == id).FirstOrDefault();
            if(reg == null)
                reg = new Producto();
            
            return reg;
        }


        public async Task<IActionResult> Portal()
        {
            if (HttpContext.Session.GetString("Canasta") == null)
                HttpContext.Session.SetString("Canasta", JsonConvert.SerializeObject(new List<IngresoItem>()));

            return View(await Task.Run(() => dao.ListarProductosIngreso()));


        }

        public int indice(int id)
        {
            List<IngresoItem> carrito =
                JsonConvert.DeserializeObject<List<IngresoItem>>(HttpContext.Session.GetString("Canasta"));
            for (int i = 0; i < carrito.Count; i++)
            {
                if (carrito[i].idProducto == id)
                    return i;
            }
            HttpContext.Session.SetString("Canasta", JsonConvert.SerializeObject(carrito)
                );

            return -1;
        }


        public async Task<ActionResult> Agregar(int id = 0)
        {
            return View(await Task.Run(() => buscar(id)));
        }



        [HttpPost] 
        public JsonResult Agregar(int codigo,int cantidad,decimal pre)
        {

            Producto reg = buscar(codigo);
            
            IngresoItem it = new IngresoItem();
            it.idProducto = codigo;
            it.nombreProducto = reg.nombreProducto;
            it.precio = pre;
            it.cantidad = cantidad;
            int posicion = indice(codigo);
            List<IngresoItem> carrito = JsonConvert.DeserializeObject<List<IngresoItem>>(HttpContext.Session.GetString("Canasta"));
            if(posicion == -1)
            {
                carrito.Add(it);
            }
            else
            {
                ViewData["mensaje"] = "Producto ya existe";
            }
            
            HttpContext.Session.SetString("Canasta",JsonConvert.SerializeObject(carrito));
            return Json(reg);
        }

        public IActionResult Ingreso()
        {
            return View();
        }


        public ActionResult DetalleIngreso()
        {
            ViewBag.fecha = DateTime.Now;
            if (HttpContext.Session.GetString("Canasta") == null) return RedirectToAction("Portal");

            IEnumerable<IngresoItem> carrito =
                JsonConvert.DeserializeObject<List<IngresoItem>>(HttpContext.Session.GetString("Canasta"));
            return View(carrito);

           

        }

        public IActionResult delete(int id)
        {
            List<IngresoItem> carrito =
                JsonConvert.DeserializeObject<List<IngresoItem>>(HttpContext.Session.GetString("Canasta"));

            IngresoItem reg = carrito.Where(it => it.idProducto == id).FirstOrDefault();

            carrito.Remove(reg);

            HttpContext.Session.SetString("Canasta", JsonConvert.SerializeObject(carrito)
                );

            return RedirectToAction("DetalleIngreso");
        }

        [HttpPost]
        public JsonResult GuardarIngreso([FromBody] Ingreso ingreso)
        {
            XElement Ingreso = new XElement("Ingreso",
                
                new XElement("idProveedor", ingreso.idProveedor),
                new XElement("fechaIngreso", ingreso.fechaIngreso),
                new XElement("total", ingreso.total)
                );

            XElement oIngresoDetalle = new XElement("DetalleIngreso");

            foreach(DetalleIngreso item in ingreso.lstdetalleIngreso)
            {
                oIngresoDetalle.Add(new XElement("Item",
                    new XElement("idProducto",item.idProducto),
                    new XElement("Precio",item.precioUnitario),
                    new XElement("Cantidad",item.cantidad),
                    new XElement("Subtotal",item.subtotal)
                    
                    ));
            }
            Ingreso.Add(oIngresoDetalle);

            return Json(true);
        }

    }
}
