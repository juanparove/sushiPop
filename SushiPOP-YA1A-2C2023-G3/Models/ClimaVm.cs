using Newtonsoft.Json;

namespace SushiPOP_YA1A_2C2023_G3.Models
{
    public class ClimaVm
    {

        [JsonProperty("current")]
        public Current current { get; set; }

    }

    public class Current
    {

        [JsonProperty("temp_c")]
        public decimal temperatura { get; set; }

        [JsonProperty("condition")]
        public Condition condition { get; set; }

    }

    public class  Condition
    {
        [JsonProperty("text")]
        public string clima { get; set; }
    }
}
