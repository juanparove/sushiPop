using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Usuario
    {
        [Required(ErrorMessage="Este campo es obligatorio")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime FechaAlta { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public bool Activo { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Email { get; set; }
    }
}
