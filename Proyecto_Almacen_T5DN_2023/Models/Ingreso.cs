namespace Proyecto_Almacen_T5DN_2023.Models
{
    public class Ingreso
    {
        public int idIngreso { get; set; }
        public string idProveedor { get; set; }
        public DateTime fechaIngreso { get; set; }
        public decimal total { get; set; }

        public List<DetalleIngreso> lstdetalleIngreso { get; set; }

    }
}
