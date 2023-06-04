namespace Proyecto_Almacen_T5DN_2023.Models
{
    public class RProducto
    {
        public string idProducto { get; set; }
        public string nombreProducto { get; set; }
        public string descripcionP { get; set; }
        public int idProveedor { get; set; }
        public int stock { get; set; }
        public decimal precio { get; set; }
    }
}
