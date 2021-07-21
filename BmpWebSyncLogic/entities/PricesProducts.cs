using Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BmpWebSyncLogic.entities
{
    public class PricesProducts
    {
        [JsonProperty("product_id")]
        public long Id { get; set; }

        [JsonProperty("price_catalog")]
        public double PriceCatalog { get; set; }

        [JsonProperty("price_promotion", NullValueHandling = NullValueHandling.Ignore)]
        public double? PricePromotion { get; set; }

        [JsonProperty("price_sale", NullValueHandling = NullValueHandling.Ignore)]
        public double? PriceSale { get; set; }

        [JsonProperty("vat")]
        public long Vat { get; set; }        


        public static List<PricesProducts> FromJson(string json) => JsonConvert.DeserializeObject<List<PricesProducts>>(json, BmpWebSyncLogic.entities.Converter.Settings);


        private static List<PricesProducts> FillList(DataTable dt)
        {
            var convertedList = (from rw in dt.AsEnumerable()
                                 select new PricesProducts()
                                 {
                                     Id = Convert.ToInt64(rw["product_id"]),
                                     PriceCatalog = Convert.ToDouble(rw["price_catalog"]),
                                     PricePromotion = rw["price_promotion"] != DBNull.Value ? Convert.ToDouble(rw["price_promotion"]) : (double?)null,
                                     PriceSale = rw["price_sale"] != DBNull.Value ? Convert.ToDouble(rw["price_sale"]) : (double?)null,
                                     Vat = Convert.ToInt64(rw["vat"]),                                     
                                 }).ToList();

            return convertedList;
        }


        public static List<PricesProducts> GetPrices()
        {
            var logWatch = System.Diagnostics.Stopwatch.StartNew();
            DataSet ds = DBHelper.RunSqlProc("bmp.b2b_setPricesProducts");
            var t = new List<Tuple<string, long>>();
            logWatch.Stop();
            t.Add(Tuple.Create("baza", logWatch.ElapsedMilliseconds));
            logWatch.Reset();
            logWatch.Start();
            var products = PricesProducts.FillList(ds.Tables[0]);            
            
            logWatch.Stop();
            t.Add(Tuple.Create("fill", logWatch.ElapsedMilliseconds));
            logWatch.Reset();            
            t.Add(Tuple.Create("paraller each", logWatch.ElapsedMilliseconds));

            return (List<PricesProducts>)products;
        }
    }
}
