using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Producto
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal Precio { get; set; }
        public string Foto { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal Costo { get; set; }
        
        /*
         * Relaciones
         */
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public ICollection<Descuento> Descuentos { get; set; }
        public ICollection<CarritoItem> CarritosItems { get; set; }
    }
}
