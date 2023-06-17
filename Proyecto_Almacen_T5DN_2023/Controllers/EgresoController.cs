using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Proyecto_Almacen_T5DN_2023.DAO;
using Proyecto_Almacen_T5DN_2023.Models;
using System.Configuration;
using System.Data;
using System.Xml.Linq;

namespace Proyecto_Almacen_T5DN_2023.Controllers
{
    public class EgresoController : Controller
    {
        private readonly string cadenaSql;
        public EgresoController(IConfiguration configuration) {

            cadenaSql = configuration.GetConnectionString("cnDB");
        }

        public DA_Egreso dao = new DA_Egreso();

        public async Task<IActionResult> Portal()
        {
            if (HttpContext.Session.GetString("Egreso") == null)
                HttpContext.Session.SetString("Egreso", JsonConvert.SerializeObject(new List<IngresoItem>()));

            return View(await Task.Run(() => dao.ListarProductosEgreso()));


        }

        public int indice(int id)
        {
            List<IngresoItem> carrito =
                JsonConvert.DeserializeObject<List<IngresoItem>>(HttpContext.Session.GetString("Egreso"));
            for (int i = 0; i < carrito.Count; i++)
            {
                if (carrito[i].idProducto == id)
                    return i;
            }
            HttpContext.Session.SetString("Egreso", JsonConvert.SerializeObject(carrito)
                );

            return -1;
        }

        Producto buscar(int id = 0)
        {
            Producto reg = dao.ListarProductosEgreso().Where(p => p.idProducto == id).FirstOrDefault();
            if (reg == null)
                reg = new Producto();

            return reg;
        }


        public async Task<ActionResult> Agregar(int id = 0)
        {
            return View(await Task.Run(() => buscar(id)));
        }


        [HttpPost]
        public JsonResult Agregar(int codigo, int cantidad)
        {

            Producto reg = buscar(codigo);

            IngresoItem it = new IngresoItem();
            it.idProducto = codigo;
            it.nombreProducto = reg.nombreProducto;
            it.precio = reg.precio;
            it.cantidad = cantidad;
            int posicion = indice(codigo);
            List<IngresoItem> carrito = JsonConvert.DeserializeObject<List<IngresoItem>>(HttpContext.Session.GetString("Egreso"));
            if (posicion == -1)
            {
                carrito.Add(it);
            }
            else
            {
                ViewData["mensaje"] = "Producto ya existe";
            }

            HttpContext.Session.SetString("Egreso", JsonConvert.SerializeObject(carrito));
            return Json(reg);
        }

        public IActionResult Ingreso()
        {
            return View();
        }


        public ActionResult DetalleEgreso()
        {
            ViewBag.fecha = DateTime.Now;
            if (HttpContext.Session.GetString("Egreso") == null) return RedirectToAction("Portal");

            IEnumerable<IngresoItem> carrito =
                JsonConvert.DeserializeObject<List<IngresoItem>>(HttpContext.Session.GetString("Egreso"));
            return View(carrito);



        }

        public IActionResult delete(int id)
        {
            List<IngresoItem> carrito =
                JsonConvert.DeserializeObject<List<IngresoItem>>(HttpContext.Session.GetString("Egreso"));

            IngresoItem reg = carrito.Where(it => it.idProducto == id).FirstOrDefault();

            carrito.Remove(reg);

            HttpContext.Session.SetString("Egreso", JsonConvert.SerializeObject(carrito)
                );

            return RedirectToAction("DetalleEgreso");
        }

        [HttpPost]
        public JsonResult GuardarEgreso(string dato)
        {

            Egreso i = new Egreso();

            i = JsonConvert.DeserializeObject<Egreso>(dato);

            XElement egreso = new XElement("Egreso",

                 new XElement("idCliente", i.idCliente),
                 new XElement("fechaEgreso", i.fechaEgreso),        
                 new XElement("total", i.total),
                 new XElement("destino", i.destino)
                 );

            XElement oDetalleEgreso = new XElement("DetalleIngreso");

            foreach (DetalleEgreso item in i.lstdetalleEgreso)
            {
                oDetalleEgreso.Add(new XElement("Item",
                    new XElement("idProducto", item.idProducto),
                    new XElement("Precio", item.precioUnitario),
                    new XElement("Cantidad", item.cantidad),
                    new XElement("Subtotal", item.subtotal)

                    ));
            }
            egreso.Add(oDetalleEgreso);


            using (SqlConnection conexion = new SqlConnection(cadenaSql))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_guardarEgreso", conexion);
                cmd.Parameters.Add("@egreso_xml", SqlDbType.Xml).Value = egreso.ToString();
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.ExecuteNonQuery();
                List<IngresoItem> carrito =
               JsonConvert.DeserializeObject<List<IngresoItem>>(HttpContext.Session.GetString("Egreso"));

                carrito = new List<IngresoItem>();

                HttpContext.Session.SetString("Egreso", JsonConvert.SerializeObject(carrito));


                return Json(new { respuesta = true });


            }
        }
        [HttpPost]
        public JsonResult Editar(string dato)
        {
            IngresoItem i = new IngresoItem();

            i = JsonConvert.DeserializeObject<IngresoItem>(dato);

            List<IngresoItem> carrito =
               JsonConvert.DeserializeObject<List<IngresoItem>>(HttpContext.Session.GetString("Egreso"));

            IngresoItem resultado = carrito.FirstOrDefault(p => p.idProducto == i.idProducto);
            if (resultado != null)
            {
                resultado.precio = i.precio;
                resultado.cantidad = i.cantidad;

            }

            HttpContext.Session.SetString("Egreso", JsonConvert.SerializeObject(carrito)
               );

            return Json(new { respuesta = true });

        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
