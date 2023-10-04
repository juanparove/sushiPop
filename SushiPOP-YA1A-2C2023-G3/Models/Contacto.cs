using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Contacto
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string NombreCompleto { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Telefono { get; set; }
        public string Mensaje { get; set; }
        public bool Leido { get; set; }
    }
}
