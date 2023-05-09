using System.ComponentModel.DataAnnotations;

namespace Proyecto_Almacen_T5DN_2023.Models
{
    public class RUsuario
    {
        [Key]
        public int idUsuario { get; set; }

        [Required (ErrorMessage ="campo requerido")]
        public string? nombre { get; set; }

        [Required(ErrorMessage = "campo requerido")]
        public string? correo { get; set; }

        [Required(ErrorMessage = "campo requerido")]
        public string? clave { get; set; }

        
        public int idRol { get; set; }
    }
}
