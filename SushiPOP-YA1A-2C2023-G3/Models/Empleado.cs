using SushiPOP_YA1A_2C2023_G3.Models;
using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Empleado : Usuario
    {
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int Legajo { get; set; }
    }
}
