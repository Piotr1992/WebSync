using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BmpWebSyncLogic.entities
{
    public class ProductPricePutJson
    {
        [JsonProperty("product_id")]
        public long ProductId { get; set; }

        [JsonProperty("price_catalog")]
        public double PriceCatalog { get; set; }

        [JsonProperty("price_promotion")]
        public long PricePromotion { get; set; }

        [JsonProperty("price_sale")]
        public long PriceSale { get; set; }

        [JsonProperty("vat")]
        public long Vat { get; set; }


        public static List<ProductPricePutJson> FromJson(string json) => JsonConvert.DeserializeObject<List<ProductPricePutJson>>(json, BmpWebSyncLogic.entities.Converter.Settings);
    }
}
