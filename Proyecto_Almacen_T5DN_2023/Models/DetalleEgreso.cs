namespace Proyecto_Almacen_T5DN_2023.Models
{
    public class DetalleEgreso
    {
        public int idEgresoDetalle { get; set; }

        public int idProducto { get; set; }
        public int cantidad { get; set; }

        public decimal precioUnitario { get; set; }
        public decimal subtotal { get; set; }

        public Egreso egreso { get; set; }
        public Producto producto { get; set; }
    }
}
