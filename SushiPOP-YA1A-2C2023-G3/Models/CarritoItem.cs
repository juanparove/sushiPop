using SushiPOP_YA1A_2C2023_G3.Models;
using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class CarritoItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public decimal PrecioUnitarioConDescuento { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int Cantidad { get; set; }

        /*
         * Relaciones
         */

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int CarritoId { get; set; }
        public Carrito Carrito { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
