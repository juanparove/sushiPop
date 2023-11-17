using Newtonsoft.Json;

namespace SushiPOP_YA1A_2C2023_G3.Models
{
    public class ClimaVm
    {

        [JsonProperty("temp_c")]
        public decimal temperatura { get; set; }

        [JsonProperty("condition:text")]
        public string clima { get; set; }
    }
}
