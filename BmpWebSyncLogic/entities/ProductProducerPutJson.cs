using System.Collections.Generic;
using Newtonsoft.Json;


namespace BmpWebSyncLogic.entities
{
    public class ProductProducerPutJson
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public LangValue Name { get; set; }

        [JsonProperty("files")]
        public List<long> Files { get; set; }

        public static List<ProductProducerPutJson> FromJson(string json) => JsonConvert.DeserializeObject<List<ProductProducerPutJson>>(json, BmpWebSyncLogic.entities.Converter.Settings);
    }
}
