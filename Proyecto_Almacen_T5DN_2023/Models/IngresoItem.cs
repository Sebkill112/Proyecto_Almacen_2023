namespace Proyecto_Almacen_T5DN_2023.Models
{
	public class IngresoItem
	{
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
        public decimal monto { get { return precio * cantidad; } }


    }
}
