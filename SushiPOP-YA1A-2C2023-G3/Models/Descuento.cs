using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Descuento
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Dia { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Porcentaje { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal DescuentoMaximo { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public bool Activo { get; set; }

        /*
         * Relaciones 
         */
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
