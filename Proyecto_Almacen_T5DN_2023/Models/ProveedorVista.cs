using System.ComponentModel.DataAnnotations;

namespace Proyecto_Almacen_T5DN_2023.Models
{
    public class ProveedorVista
    {
        [Display(Name = "Codigo")]
        public string idProveedor { get; set; } = string.Empty;

        [Display(Name = "RUC")]
        public string RUC { get; set; } = string.Empty;


        [Display(Name = "Compañia")]
        public string nombreProveedor { get; set; } = string.Empty;


        [Display(Name = "Dirección")]
        public string direccion { get; set; } = string.Empty;


        [Display(Name = "Distrito")]
        public string idDistrito { get; set; } = string.Empty;

        [Display(Name = "Telefono")]
        public string telefono { get; set; } = string.Empty;

        [Display(Name = "Provincia")]
        public string idProvincia { get; set; } = string.Empty;
        [Display(Name = "Correo")]
        public string correo { get; set; } = string.Empty;

        [Display(Name = "Departamento")]
        public string idDepartamento { get; set; } = string.Empty;
    }
}
