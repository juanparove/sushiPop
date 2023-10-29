using SushiPOP_YA1A_2C2023_G3.Models;
using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Carrito
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public bool Procesado { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public bool Cancelado { get; set; }

        /*
         * Relaciones
         */

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public virtual Pedido Pedido { get; set; }
        public ICollection<CarritoItem>? CarritosItems { get; set; }
    }
}
