using SushiPOP_YA1A_2C2023_G3.Models;
using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(5, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(30, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(5, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(30, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Apellido { get; set; }

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MaxLength(100, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Direccion { get; set; }

        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(10, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(10, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Telefono { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Fecha de alta")]
        public DateTime FechaAlta { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public bool Activo { get; set; }

        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MaxLength(100, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Email { get; set; }
    }
}
