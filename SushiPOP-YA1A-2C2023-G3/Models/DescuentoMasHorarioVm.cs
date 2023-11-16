using Newtonsoft.Json;

namespace SushiPOP_YA1A_2C2023_G3.Models
{
    public class DescuentoMasHorarioVm
    {
  
        public string Horarios { get; set; }


        public string DiaDescuento { get; set; }

        public string textoDescuento { get; set; }
        public decimal PorcentajeDescuento { get; set; }


        public string NombreProducto { get; set; }

    }
}
