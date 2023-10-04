using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Empleado : Usuario
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Legajo { get; set; }
    }
}
