using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Proyecto_Almacen_T5DN_2023.Models
{
    public class ClienteVista
    {

        [Display(Name = "Codigo")]
        public string idCliente { get; set; } = string.Empty;

        [Display(Name = "RUC")]
        public string RUC { get; set; } = string.Empty;


        [Display(Name = "Compañia")]
        public string nombreCompañia { get; set; } = string.Empty;


        [Display(Name = "Dirección")]
        public string direccion { get; set; } = string.Empty;


        [Display(Name = "Distrito")]
        public string idDistrito { get; set; } = string.Empty;

        [Display(Name = "Telefono")]
        public string telefono { get; set; } = string.Empty;

        [Display(Name = "Provincia")]
        public string idProvincia { get; set; } = string.Empty;

        [Display(Name = "Departamento")]
        public string idDepartamento { get; set; } = string.Empty;
    }
}
