using Newtonsoft.Json;

namespace SushiPOP_YA1A_2C2023_G3.Models
{
    public class DescuentoMasHorarioVm
    {
        [JsonProperty("apertura")]
        public int HorarioApertura { get; set; }

        [JsonProperty("cierre")]
        public int HorarioCierre { get; set; }

        [JsonProperty("dia")]
        public int DiaDescuento { get; set; }

        [JsonProperty("porcentaje")]
        public decimal PorcentajeDescuento { get; set; }

        [JsonProperty("producto")]
        public string NombreProducto { get; set; }

    }
}
