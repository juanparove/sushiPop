using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Reserva
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Local { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime FechaHora { get; set; }
        public bool Confirmada { get; set; }
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Apellido { get; set; }

        /*
         * Relaciones
         */
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
