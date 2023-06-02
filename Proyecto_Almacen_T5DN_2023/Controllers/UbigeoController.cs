using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto_Almacen_T5DN_2023.DAO;

namespace Proyecto_Almacen_T5DN_2023.Controllers
{
    public class UbigeoController : Controller
    {

        public DA_Ubigeo dao = new DA_Ubigeo();


        public async Task<IActionResult> Ubigeo()
        {
            ViewBag.departamentos = new SelectList(await Task.Run(() => dao.ListarDepartamentos()), "CodigoDepartamento", "Nombre");

            return View();
        }

        [HttpGet]
        public JsonResult Provincia(int codDepa)
        {
            return Json(dao.ListarProvincias(codDepa));
        }

        [HttpGet]
        public JsonResult Distrito(int codProv)
        {
            return Json(dao.ListarDistritos(codProv));
        }
    }
}
