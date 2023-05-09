using Microsoft.AspNetCore.Mvc;

using Proyecto_Almacen_T5DN_2023.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Data;

namespace Proyecto_Almacen_T5DN_2023.Controllers
{
    public class RegistroController : Controller
    {
        public IConfiguration Configuration { get; }


        public RegistroController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Registrar()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Registrar(RUsuario usuario)
        {
            
            {
                using (SqlConnection con = new(Configuration["ConnectionStrings:cnDB"]))
                {

                    using (SqlCommand cmd = new("SP_NUEVOUSUARIO", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("@nom", System.Data.SqlDbType.VarChar).Value = usuario.nombre;
                        cmd.Parameters.Add("@correo", System.Data.SqlDbType.VarChar).Value = usuario.correo;
                        cmd.Parameters.Add("@clave", System.Data.SqlDbType.VarChar).Value = usuario.clave;
                        cmd.Parameters.Add("@rol", System.Data.SqlDbType.Int).Value = usuario.idRol;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                
                return Redirect("Usuario/Index");

            }
           
        }

        
    }
}
