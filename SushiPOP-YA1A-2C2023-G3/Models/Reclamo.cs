using SushiPOP_YA1A_2C2023_G3.Models;
using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Reclamo
    {
        public int Id { get; set; }

        [Display(Name = "Nombre completo")]
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MaxLength(255, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string NombreCompleto { get; set; }

        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public string Email { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(10, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(10, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Telefono { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(50, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        public string DetalleReclamo { get; set; }

        /*
         * Relaciones
         */

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
    }
}
