using SushiPOP_YA1A_2C2023_G3.Models;
using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int NroPedido { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public DateTime FechaCompra { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public decimal Subtotal { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public decimal GastoEnvio { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public decimal Total { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int Estado { get; set; }

        /*
         * Relaciones
         */
        public virtual Reclamo Reclamo { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int CarritoId { get; set; }
        public Carrito Carrito { get; set; }
    }
}
