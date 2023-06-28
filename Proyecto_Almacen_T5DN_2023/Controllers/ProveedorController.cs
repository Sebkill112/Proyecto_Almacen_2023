using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Proyecto_Almacen_T5DN_2023.DAO;
using Proyecto_Almacen_T5DN_2023.Models;

namespace Proyecto_Almacen_T5DN_2023.Controllers
{
    public class ProveedorController : Controller
    {
        public readonly IConfiguration _configuration;

        public DA_Ubigeo dao = new DA_Ubigeo();

        public DA_Clientes daoo = new DA_Clientes();
        public ProveedorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string proveedorCorrelativo()
        {
            string codigo = string.Empty;
            using (SqlConnection cn = new(_configuration["ConnectionStrings:cnDB"]))
            {
                SqlCommand cmd = new SqlCommand("sp_generar_numero", cn);
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

        IEnumerable<ProveedorVista> listaProveedor()
        {
            List<ProveedorVista> lista = new List<ProveedorVista>();
            using (SqlConnection cn = new(_configuration["ConnectionStrings:cnDB"]))
            {
                SqlCommand cmd = new SqlCommand("usp_ProveedorListar", cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new ProveedorVista
                    {
                        idProveedor = reader.GetString(0),
                        RUC = reader.GetString(1),
                        nombreProveedor = reader.GetString(2),
                        direccion = reader.GetString(3),
                        idDistrito = reader.GetString(4),
                        telefono = reader.GetString(5),
                        idProvincia = reader.GetString(6),
                        idDepartamento = reader.GetString(7),
                        correo = reader.GetString(8)
                    });
                }
                cn.Close();
            }
            return lista;
        }

        IEnumerable<Proveedor> listaProveedor2()
        {
            List<Proveedor> lista = new List<Proveedor>();
            using (SqlConnection cn = new(_configuration["ConnectionStrings:cnDB"]))
            {
                SqlCommand cmd = new SqlCommand("usp_ProveedorListar2", cn);
                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Proveedor
                    {
                        idProveedor = reader.GetString(0),
                        RUC = reader.GetString(1),
                        nombreProveedor = reader.GetString(2),
                        direccion = reader.GetString(3),
                        idDistrito = reader.GetInt32(4),
                        telefono = reader.GetString(5),
                        idProvincia = reader.GetInt32(6),
                        idDepartamento = reader.GetInt32(7),
                        correo = reader.GetString(8)
                    });
                }
                cn.Close();
            }
            return lista;
        }


        public async Task<IActionResult> Index()
        {
            return View(await Task.Run(() => listaProveedor()));
        }

        public async Task<IActionResult> Create(int codDep = 0, int codPro = 0)
        {
            ViewBag.codigo = proveedorCorrelativo();
            ViewBag.departamentos = new SelectList(await Task.Run(() => dao.ListarDepartamentos()), "CodigoDepartamento", "Nombre");
 
            return View(new Proveedor());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Proveedor proveedor)
        {
            if (!ModelState.IsValid)
            {
                return View(proveedor);
            }
            string mensaje = string.Empty;
            using (SqlConnection cn = new SqlConnection(_configuration["ConnectionStrings:cnDB"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_proveedor_merge", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProveedor", proveedor.idProveedor);
                    cmd.Parameters.AddWithValue("@RUC", proveedor.RUC);
                    cmd.Parameters.AddWithValue("@nombreProveedor", proveedor.nombreProveedor);
                    cmd.Parameters.AddWithValue("@direccion", proveedor.direccion);
                    cmd.Parameters.AddWithValue("@idDistrito", proveedor.idDistrito);
                    cmd.Parameters.AddWithValue("@telefono", proveedor.telefono);
                    cmd.Parameters.AddWithValue("@idProvincia", proveedor.idProvincia);
                    cmd.Parameters.AddWithValue("@idDepartamento", proveedor.idDepartamento);
                    cmd.Parameters.AddWithValue("@correo", proveedor.correo);
                    cn.Open();
                    int cantidad = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha creado {cantidad} Proveedor";

                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                ViewBag.mensaje = mensaje;
                ViewBag.departamentos = new SelectList(await Task.Run(() => dao.ListarDepartamentos()), "CodigoDepartamento", "Nombre", proveedor.idDepartamento);
                return View(proveedor);
            }
        }

        public async Task<IActionResult> Edit(string id, int codDep = 0, int codPro = 0)
        {
            Proveedor proveedor = listaProveedor2().Where(e => e.idProveedor == id).FirstOrDefault();
            if (proveedor == null)
            {
                return RedirectToAction("Index");
            }



            ViewBag.departamentos = new SelectList(await Task.Run(() => dao.ListarDepartamentos()), "CodigoDepartamento", "Nombre");
            ViewBag.distritos = new SelectList(await Task.Run(() => dao.ListarDistritos(codDep)), "CodigoDistrito", "Nombre");
            ViewBag.provincias = new SelectList(await Task.Run(() => dao.ListarProvincias(codDep)), "CodigoProvincia", "Nombre");
            return View(proveedor);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Proveedor proveedor, int codDep = 0, int codPro = 0)
        {
            if (!ModelState.IsValid)
            {
                return View(proveedor);
            }
            string mensaje = string.Empty;
            using (SqlConnection cn = new SqlConnection(_configuration["ConnectionStrings:cnDB"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_proveedor_merge", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProveedor", proveedor.idProveedor);
                    cmd.Parameters.AddWithValue("@RUC", proveedor.RUC);
                    cmd.Parameters.AddWithValue("@nombreProveedor", proveedor.nombreProveedor);
                    cmd.Parameters.AddWithValue("@direccion", proveedor.direccion);
                    cmd.Parameters.AddWithValue("@idDistrito", proveedor.idDistrito);
                    cmd.Parameters.AddWithValue("@telefono", proveedor.telefono);
                    cmd.Parameters.AddWithValue("@idProvincia", proveedor.idProvincia);
                    cmd.Parameters.AddWithValue("@idDepartamento", proveedor.idDepartamento);
                    cmd.Parameters.AddWithValue("@correo", proveedor.correo);
                    cn.Open();
                    int cantidad = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha Modificado {cantidad} Proveedor";

                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                ViewBag.mensaje = mensaje;
                ViewBag.departamentos = new SelectList(await Task.Run(() => dao.ListarDepartamentos()), "CodigoDepartamento", "Nombre");
   
                return View(proveedor);
            }
        }

        public IActionResult Delete(string id)
        {
            Proveedor regCliente = listaProveedor2().Where(e => e.idProveedor == id).FirstOrDefault();
            if (regCliente == null)
            {
                return RedirectToAction("Index");
            }




            return View(regCliente);
        }





        [HttpPost]
        public IActionResult Delete(Proveedor reg)
        {
            string mensaje = string.Empty;
            using (SqlConnection cn = new SqlConnection(_configuration["ConnectionStrings:cnDB"]))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_proveedor_delete", cn);
                    cmd.Parameters.AddWithValue("@idProveedor", reg.idProveedor);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cn.Open();
                    int cantidad = cmd.ExecuteNonQuery();
                    cn.Close();
                    mensaje = $"Se ha Eliminado {cantidad} Proveedor";

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
