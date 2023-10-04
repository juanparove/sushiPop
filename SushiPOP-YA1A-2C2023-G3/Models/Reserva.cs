using SushiPOP_YA1A_2C2023_G3.Models;
using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public string Local { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public bool Confirmada { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public string Apellido { get; set; }

        /*
         * Relaciones
         */

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
