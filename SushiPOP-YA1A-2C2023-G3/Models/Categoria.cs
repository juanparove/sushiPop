using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Categoria
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        
        /*
         * Relaciones
         */        
        public ICollection<Producto> Productos { get; set; }
    }
}
