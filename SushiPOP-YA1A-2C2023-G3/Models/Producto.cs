using SushiPOP_YA1A_2C2023_G3.Models;
using System.ComponentModel.DataAnnotations;

namespace SushiPop.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required( ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MaxLength(100,ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        [MinLength(20, ErrorMessage = ErrorViewModel.CaracteresMinimos)]
        [MaxLength(250, ErrorMessage = ErrorViewModel.CaracteresMaximos)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public decimal Precio { get; set; }
        
        public string Foto { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int Stock { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
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
