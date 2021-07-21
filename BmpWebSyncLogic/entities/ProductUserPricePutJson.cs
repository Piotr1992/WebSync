using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BmpWebSyncLogic.entities
{
    public class ProductUserPricePutJson
    {
        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("prices")]
        public List<Price> Prices { get; set; }

        public static List<ProductUserPricePutJson> FromJson(string json) => JsonConvert.DeserializeObject<List<ProductUserPricePutJson>>(json, BmpWebSyncLogic.entities.Converter.Settings);
    }


    public class Price
    {
        [JsonProperty("product_id")]
        public long ProductId { get; set; }

        [JsonProperty("price")]
        public long PricePrice { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }

}
