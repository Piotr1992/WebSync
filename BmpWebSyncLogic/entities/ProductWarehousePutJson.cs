using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BmpWebSyncLogic.entities
{
    public  class ProductWarehousePutJson
    {
        [JsonProperty("product_id")]
        public long ProductId { get; set; }

        [JsonProperty("warehouse")]
        public long Warehouse { get; set; }

        public static List<ProductWarehousePutJson> FromJson(string json) => JsonConvert.DeserializeObject<List<ProductWarehousePutJson>>(json, BmpWebSyncLogic.entities.Converter.Settings);
    }

}
