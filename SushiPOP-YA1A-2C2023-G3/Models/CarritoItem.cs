using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class CarritoItem
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Id { get; set; }
        public decimal PrecioUnitarioConDescuento { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Cantidad { get; set; }

        /*
         * Relaciones
         */
        public int CarritoId { get; set; }
        public Carrito Carrito { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
