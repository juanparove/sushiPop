using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Reclamo
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string NombreCompleto { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Email { get; set; }
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string DetalleReclamo { get; set; }

        /*
         * Relaciones
         */
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
    }
}
