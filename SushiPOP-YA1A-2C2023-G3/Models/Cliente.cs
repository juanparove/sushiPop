using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Cliente : Usuario
    {
        [Display(Name = "Numero de cliente")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int? NumeroCliente { get; set; }

        /*
         * Relaciones
         */
        public ICollection<Reserva> Reservas { get; set; }
        public ICollection<Carrito> Carritos { get; set; }
    }
}
 