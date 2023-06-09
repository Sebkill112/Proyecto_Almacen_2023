﻿using System.ComponentModel.DataAnnotations;

namespace Proyecto_Almacen_T5DN_2023.Models
{
    public class Proveedor
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
        public int idDistrito { get; set; }

        [Display(Name = "Telefono")]
        public string telefono { get; set; } = string.Empty;

        [Display(Name = "Provincia")]
        public int idProvincia { get; set; }

        [Display(Name = "Departamento")]
        public int idDepartamento { get; set; }
        [Display(Name = "Correo")]
        public string correo { get; set; }
        [Display(Name = "Estado")]
        public string estado { get; set; }

    }
}
