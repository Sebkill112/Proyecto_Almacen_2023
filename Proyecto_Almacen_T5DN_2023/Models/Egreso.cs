namespace Proyecto_Almacen_T5DN_2023.Models
{
    public class Egreso
    {
        public int idEgreso { get; set; }
        public string idCliente { get; set; }
        public DateTime fechaEgreso { get; set; }
        public decimal total { get; set; }
        public String destino { get; set; }

        public List<DetalleEgreso> lstdetalleEgreso { get; set; }
    }
}
