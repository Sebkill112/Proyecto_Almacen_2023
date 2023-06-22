using Microsoft.AspNetCore.Mvc;
using Proyecto_Almacen_T5DN_2023.DAO;
using Proyecto_Almacen_T5DN_2023.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Proyecto_Almacen_T5DN_2023.Controllers
{
    public class UsuarioController : Controller
    {

        

        public IActionResult Index()
        {
            
                return View();          
        }


        [Authorize]
        public IActionResult Listado()
        {
            DA_Usuario dao = new DA_Usuario();

            return View(dao.ListarUsuarios());
        }


        [HttpPost]
        public  async Task<IActionResult> Index(Usuario usu)
        {

            DA_Usuario dao = new DA_Usuario();

            List<Usuario> lista = dao.ListarUsuarios();

             Usuario  u = lista.Where(item => item.correo == usu.correo && item.clave == usu.clave).FirstOrDefault();

            if (u != null)
            {

                var claims = new List<Claim> {

                new Claim(ClaimTypes.Name,u.correo),
                new Claim("Correo",u.correo),
                new Claim(ClaimTypes.Role, u.des_Rol)

                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity));


                return RedirectToAction("Listado", "Usuario");
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario Incorrecto";
                return View();
            }

            
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
             return RedirectToAction("Index", "Usuario"); ;
        }



    }
}
