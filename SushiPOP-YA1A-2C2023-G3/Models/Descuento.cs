using SushiPOP_YA1A_2C2023_G3.Models;
using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Descuento
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int Dia { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int Porcentaje { get; set; }

        public decimal DescuentoMaximo { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public bool Activo { get; set; }

        /*
         * Relaciones 
         */

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
