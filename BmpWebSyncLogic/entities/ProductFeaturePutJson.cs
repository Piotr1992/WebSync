using System.Collections.Generic;
using Newtonsoft.Json;


namespace BmpWebSyncLogic.entities
{
    public class ProductFeaturePutJson
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public LangValue Name { get; set; }

        [JsonProperty("important")]
        public long Important { get; set; }

        [JsonProperty("filters")]
        public long Filters { get; set; }

        [JsonProperty("view_order")]
        public long ViewOrder { get; set; }


        public static List<ProductFeaturePutJson> FromJson(string json) => JsonConvert.DeserializeObject<List<ProductFeaturePutJson>>(json, BmpWebSyncLogic.entities.Converter.Settings);
    }
}
