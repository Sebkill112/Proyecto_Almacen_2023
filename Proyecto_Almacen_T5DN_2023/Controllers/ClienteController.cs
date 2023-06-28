using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Configuration;
using Proyecto_Almacen_T5DN_2023.DAO;
using Proyecto_Almacen_T5DN_2023.Models;
using System.Data.SqlClient;


namespace Proyecto_Almacen_T5DN_2023.Controllers
{
    public class ClienteController : Controller
    {
        public readonly IConfiguration _configuration;

        public DA_Ubigeo dao = new DA_Ubigeo();

        public DA_Clientes daoo = new DA_Clientes();
        public ClienteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ClienteCorrelativo()
        {
            string codigo = string.Empty;
            using (SqlConnection cn = new(_configuration["ConnectionStrings:cnDB"]))
            {
                SqlCommand cmd = new SqlCommand("sp_generar_numeroCliente", cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    codigo = reader.GetString(0);

                }
                cn.Close();
            }
            return codigo;
        }

        IEnumerable<ClienteVista> listaClientes()
        {
            List<ClienteVista> lista = new List<ClienteVista>();
            using (SqlConnection cn = new(_configuration["ConnectionStrings:cnDB"]))
            {
                SqlCommand cmd = new SqlCommand("usp_ClientesListar", cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new ClienteVista
                    {
                        idCliente = reader.GetString(0),
                        RUC = reader.GetString(1),
                        nombreCompañia = reader.GetString(2),
                        direccion = reader.GetString(3),
                        idDistrito = reader.GetString(4),
                        telefono = reader.GetString(5),
                        idProvincia = reader.GetString(6),
                        idDepartamento = reader.GetString(7)
                    });
                }
                cn.Close();
            }
            return lista;
        }

        IEnumerable<Cliente> listaClientes2()
        {
            List<Cliente> lista = new List<Cliente>();
            using (SqlConnection cn = new(_configuration["ConnectionStrings:cnDB"]))
            {
                SqlCommand cmd = new SqlCommand("usp_ClientesListar2", cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Cliente
                    {
                        idCliente = reader.GetString(0),
                        RUC = reader.GetString(1),
                        nombreCompañia = reader.GetString(2),
                        direccion = reader.GetString(3),
                        idDistrito = reader.GetInt32(4),
                        telefono = reader.GetString(5),
                        idProvincia = reader.GetInt32(6),
                        idDepartamento = reader.GetInt32(7)
                    });
                }
                cn.Close();
            }
            return lista;
        }

        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => listaClientes()));
        }

        public async Task<IActionResult> Create(int codDep = 0, int codPro = 0)
        {

            ViewBag.codigo = ClienteCorrelativo();
            ViewBag.departamentos = new SelectList(await Task.Run(() => dao.ListarDepartamentos()), "CodigoDepartamento", "Nombre");
            ViewBag.distritos = new SelectList(await Task.Run(() => dao.ListarDistritos(codDep)), "CodigoDistrito", "Nombre");
            ViewBag.provincias = new SelectList(await Task.Run(() => dao.ListarProvincias(codDep)), "CodigoProvincia", "Nombre");
            return View(new Cliente());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cliente regCliente)
        {
            if (!ModelState.IsValid)
            {
                return View(regCliente);
            }
            string mensaje = string.Empty;
            using (SqlConnection cn = new SqlConnection(_configuration["ConnectionStrings:cnDB"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_clientes_merge", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCliente", regCliente.idCliente);
                    cmd.Parameters.AddWithValue("@RUC", regCliente.RUC);
                    cmd.Parameters.AddWithValue("@nombreCompañia", regCliente.nombreCompañia);
                    cmd.Parameters.AddWithValue("@direccion", regCliente.direccion);
                    cmd.Parameters.AddWithValue("@idDistrito", regCliente.idDistrito);
                    cmd.Parameters.AddWithValue("@telefono", regCliente.telefono);
                    cmd.Parameters.AddWithValue("@idProvincia", regCliente.idProvincia);
                    cmd.Parameters.AddWithValue("@idDepartamento", regCliente.idDepartamento);
                    cn.Open();
                    int cantidad = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha creado {cantidad} Cliente";

                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                ViewBag.mensaje = mensaje;
                ViewBag.departamentos = new SelectList(await Task.Run(() => dao.ListarDepartamentos()), "CodigoDepartamento", "Nombre", regCliente.idDepartamento);
                return View(regCliente);
            }
        }

        public async Task<IActionResult> Edit(string id, int codDep=0, int codPro=0)
        {
            Cliente regCliente = listaClientes2().Where(e => e.idCliente == id).FirstOrDefault();
            if (regCliente == null)
            {
                return RedirectToAction("Index");
            }



            ViewBag.departamentos = new SelectList(await Task.Run(() => dao.ListarDepartamentos()), "CodigoDepartamento", "Nombre");
            ViewBag.distritos = new SelectList(await Task.Run(() => dao.ListarDistritos(codDep)), "CodigoDistrito", "Nombre");
            ViewBag.provincias = new SelectList(await Task.Run(() => dao.ListarProvincias(codDep)), "CodigoProvincia", "Nombre");
            return View(regCliente);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Cliente regCliente, int codDep = 0, int codPro = 0)
        {
            if (!ModelState.IsValid)
            {
                return View(regCliente);
            }
            string mensaje = string.Empty;
            using (SqlConnection cn = new SqlConnection(_configuration["ConnectionStrings:cnDB"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_clientes_merge", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCliente", regCliente.idCliente);
                    cmd.Parameters.AddWithValue("@RUC", regCliente.RUC);
                    cmd.Parameters.AddWithValue("@nombreCompañia", regCliente.nombreCompañia);
                    cmd.Parameters.AddWithValue("@direccion", regCliente.direccion);
                    cmd.Parameters.AddWithValue("@idDistrito", regCliente.idDistrito);
                    cmd.Parameters.AddWithValue("@telefono", regCliente.telefono);
                    cmd.Parameters.AddWithValue("@idProvincia", regCliente.idProvincia);
                    cmd.Parameters.AddWithValue("@idDepartamento", regCliente.idDepartamento);
                    cn.Open();
                    int cantidad = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha Modificado {cantidad} Cliente";

                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                ViewBag.mensaje = mensaje;
                ViewBag.departamentos = new SelectList(await Task.Run(() => dao.ListarDepartamentos()), "CodigoDepartamento", "Nombre");
                ViewBag.distritos = new SelectList(await Task.Run(() => dao.ListarDistritos(codDep)), "CodigoDistrito", "Nombre");
                ViewBag.provincias = new SelectList(await Task.Run(() => dao.ListarProvincias(codDep)), "CodigoProvincia", "Nombre");
                return View(regCliente);
            }
        }

        public IActionResult Delete(string id)
        {
            Cliente regCliente = listaClientes2().Where(e => e.idCliente == id).FirstOrDefault();
            if (regCliente == null)
            {
                return RedirectToAction("Index");
            }



       
            return View(regCliente);
        }

       


      
        [HttpPost]
        public IActionResult Delete(Cliente reg)
        {
            string mensaje = string.Empty;
            using (SqlConnection cn = new SqlConnection(_configuration["ConnectionStrings:cnDB"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_clientes_delete", cn);
                    cmd.Parameters.AddWithValue("@idCliente", reg.idCliente);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cn.Open();
                    int cantidad = cmd.ExecuteNonQuery();
                    cn.Close();
                    mensaje = $"Se ha Eliminado {cantidad} Cliente";

                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                ViewBag.mensaje = mensaje;
      
                return View();
            }


        }



    }
}
