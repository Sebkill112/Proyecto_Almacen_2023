namespace Proyecto_Almacen_T5DN_2023.Models
{
    public class DetalleIngreso
    {
        public int idIngresoDetalle {get;set;}

        public int idProducto { get; set; }
        public int cantidad { get; set; }   

        public decimal precioUnitario { get; set; }
        public decimal subtotal { get; set; }

        public Ingreso ingreso { get; set; }
        public Producto producto { get; set; }
    }
}
