using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BmpWebSyncLogic.entities
{
    public class Delete
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        public static Delete FromJson(string json) => JsonConvert.DeserializeObject<Delete>(json, BmpWebSyncLogic.entities.Converter.Settings);
        public string ToJson() => JsonConvert.SerializeObject(this, BmpWebSyncLogic.entities.Converter.Settings);
    }
}
