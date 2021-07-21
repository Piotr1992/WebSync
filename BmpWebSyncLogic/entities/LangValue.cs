using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace BmpWebSyncLogic.entities
{
    public class LangValue
    {
        public long Id { get; set; }
        [JsonProperty("pl", NullValueHandling = NullValueHandling.Ignore)]
        public string Pl { get; set; }      
        [JsonProperty("en", NullValueHandling = NullValueHandling.Ignore)]
        public string En { get; set; }
        [JsonProperty("de", NullValueHandling = NullValueHandling.Ignore)]
        public string De { get; set; }
        [JsonProperty("es", NullValueHandling = NullValueHandling.Ignore)]
        public string Es { get; set; }

        public static List<LangValue> FillList(DataTable dt)
        {
            var convertedList = (from rw in dt.AsEnumerable()
                                 select new LangValue()
                                 {
                                     Id = Convert.ToInt64(rw["id"]),
                                     Pl = rw.Table.Columns.Contains("pl") ? rw["pl"] != DBNull.Value ? Convert.ToString(rw["pl"]) : null : null,
                                     En = rw.Table.Columns.Contains("en") ? rw["en"] != DBNull.Value ? Convert.ToString(rw["en"]) : null : null,
                                     De = rw.Table.Columns.Contains("de") ? rw["de"] != DBNull.Value ? Convert.ToString(rw["de"]) : null : null,
                                     Es = rw.Table.Columns.Contains("es") ? rw["es"] != DBNull.Value ? Convert.ToString(rw["es"]) : null : null
                                 }).ToList();
            return convertedList;
        }

    }
}
