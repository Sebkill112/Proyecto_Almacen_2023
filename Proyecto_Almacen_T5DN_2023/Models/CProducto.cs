using System.ComponentModel.DataAnnotations;

namespace Proyecto_Almacen_T5DN_2023.Models
{
    public class CProducto
    {
        [Display(Name = "Código")]
        public int idProducto { get; set; }
        [Display(Name = "Producto")]
        public string nombreProducto { get; set; }
        [Display(Name = "Proveedor")]
        public string nombreProveedor { get; set; }
        [Display(Name = "Stock")]
        public int stock { get; set; }
        [Display(Name = "Precio")]
        public decimal precio { get; set; }
        [Display(Name = "Categoria")]
        public string nomCat { get; set; }
    }
}
