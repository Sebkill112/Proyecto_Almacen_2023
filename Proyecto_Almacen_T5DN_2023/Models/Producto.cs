namespace Proyecto_Almacen_T5DN_2023.Models
{
    public class Producto
    {
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public string idProveedor { get; set; }
        public int stock { get; set; }
        public decimal precio { get; set; }
        public int idCat { get; set; }


    }
}
