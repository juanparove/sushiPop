using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Pedido
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int NroPedido { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime FechaCompra { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal Subtotal { get; set; }
        public decimal GastoEnvio { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal Total { get; set; }
        public int Estado { get; set; }

        /*
         * Relaciones
         */
        public virtual Reclamo Reclamo { get; set; }
        public int CarritoId { get; set; }
        public Carrito Carrito { get; set; }
    }
}
