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
    public class Product
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public LangValue Name { get; set; }

        [JsonProperty("description")]
        public LangValue Description { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("symbol", NullValueHandling = NullValueHandling.Ignore)]
        public string Symbol { get; set; }

        [JsonProperty("ean", NullValueHandling = NullValueHandling.Ignore)]
        public string Ean { get; set; }

        [JsonProperty("producer")]
        public long Producer { get; set; }

        [JsonProperty("new")]
        public long New { get; set; }

        [JsonProperty("promotion")]
        public long Promotion { get; set; }

        [JsonProperty("sale")]
        public long Sale { get; set; }

        [JsonProperty("price_catalog")]
        public double PriceCatalog { get; set; }

        [JsonProperty("price_promotion", NullValueHandling = NullValueHandling.Ignore)]
        public double? PricePromotion { get; set; }

        [JsonProperty("price_sale", NullValueHandling = NullValueHandling.Ignore)]
        public double? PriceSale { get; set; }

        [JsonProperty("vat")]
        public long Vat { get; set; }

        [JsonProperty("warehouse")]
        public long Warehouse { get; set; }

        [JsonProperty("files", NullValueHandling = NullValueHandling.Ignore)]
        public List<long> Files { get; set; }

        [JsonProperty("groups")]
        public List<long> Groups { get; set; }

        [JsonProperty("features", NullValueHandling = NullValueHandling.Ignore)]
        public List<Feature> Features { get; set; }

        [JsonProperty("connections", NullValueHandling = NullValueHandling.Ignore)]
        public List<Connection> Connections { get; set; }


        public static List<Product> FromJson(string json) => JsonConvert.DeserializeObject<List<Product>>(json, BmpWebSyncLogic.entities.Converter.Settings);


        private static List<Product> FillList(DataTable dt)
        {
            var convertedList = (from rw in dt.AsEnumerable()
                                 select new Product()
                                 {
                                     Id = Convert.ToInt64(rw["id"]),
                                     Code = rw.Table.Columns.Contains("code") ? rw["code"] != DBNull.Value ? Convert.ToString(rw["code"]) : null : null,
                                     Symbol = rw.Table.Columns.Contains("symbol") ? rw["symbol"] != DBNull.Value ? Convert.ToString(rw["symbol"]) : null : null,
                                     Ean = rw.Table.Columns.Contains("ean") ? rw["ean"] != DBNull.Value ? Convert.ToString(rw["ean"]) : null : null,
                                     Producer = Convert.ToInt64(rw["producer"]),
                                     New = Convert.ToInt64(rw["new"]),
                                     Promotion = Convert.ToInt64(rw["promotion"]),
                                     Sale = Convert.ToInt64(rw["sale"]),
                                     PriceCatalog = Convert.ToDouble(rw["price_catalog"]),
                                     PricePromotion = rw["price_promotion"] != DBNull.Value ? Convert.ToDouble(rw["price_promotion"]) : (double?)null,
                                     PriceSale = rw["price_sale"] != DBNull.Value ? Convert.ToDouble(rw["price_sale"]) : (double?)null,
                                     Vat = Convert.ToInt64(rw["vat"]),
                                     Warehouse = Convert.ToInt64(rw["warehouse"]),
                                 }).ToList();

            return convertedList;
        }


        public static List<Product> GetProducts()
        {

            var logWatch = System.Diagnostics.Stopwatch.StartNew();
            DataSet ds = DBHelper.RunSqlProc("bmp.b2b_products");
            var t = new List<Tuple<string, long>>();
            logWatch.Stop();
            t.Add(Tuple.Create("baza", logWatch.ElapsedMilliseconds));
            logWatch.Reset();
            logWatch.Start();
            var products = Product.FillList(ds.Tables[0]);
            var names = LangValue.FillList(ds.Tables[1]);
            var descriptions = LangValue.FillList(ds.Tables[2]);
            var files = LongListObject.FillList(ds.Tables[3]);
            var groups = LongListObject.FillList(ds.Tables[4]);
            var features = IdFeature.FillList(ds.Tables[5]);            
            var connections = IdConnection.FillList(ds.Tables[6]);          

            logWatch.Stop();
            t.Add(Tuple.Create("fill", logWatch.ElapsedMilliseconds));
            logWatch.Reset();
            logWatch.Start();

            Parallel.ForEach(products, (pg) =>
            {
                pg.Name = names.Where(x => pg.Id == x.Id).FirstOrDefault();
                pg.Description = descriptions.Where(x => pg.Id == x.Id).FirstOrDefault();
                pg.Files = files.Where(x => pg.Id == x.Id).Select(p => p.Value).FirstOrDefault();
                pg.Groups = groups.Where(x => pg.Id == x.Id).Select(p => p.Value).FirstOrDefault();
                pg.Features = features.Where(x => pg.Id == x.Id).Select(p => p.Value).FirstOrDefault(); 
                pg.Connections = connections.Where(x => pg.Id == x.Id).Select(p => p.Value).FirstOrDefault();
            }
            );
            logWatch.Stop();
            t.Add(Tuple.Create("paraller each", logWatch.ElapsedMilliseconds));         
            return (List<Product>)products;
        }
    }


    public class IdConnection
    {
        [JsonIgnore]
        public long Id { get; set; }

        public List<Connection> Value { get; set; }

        public static List<IdConnection> FillList(DataTable dt)
        {
            var convertedList =
                dt.Rows.OfType<DataRow>().GroupBy(z => Convert.ToInt64(z["id"])).Select(z => new IdConnection()
                {
                    Id = z.Key,
                    Value = z.Select(x => new Connection()
                    {
                        ProductId = Convert.ToInt64(x["product_id"]),
                        ConnectionTypeId = Convert.ToInt64(x["connection_type_id"]),
                    }).ToList()
                }).ToList();
            return convertedList;
        }
    }


    public class Connection
    {
        [JsonProperty("product_id")]
        public long ProductId { get; set; }

        [JsonProperty("connection_type_id")]
        public long ConnectionTypeId { get; set; }
    }


    public class IdFeature
    {
        [JsonIgnore]
        public long Id { get; set; }

        public List<Feature> Value { get; set; }

        public static List<IdFeature> FillList(DataTable dt)
        {
            var convertedList =
            dt.Rows.OfType<DataRow>().GroupBy(z => Convert.ToInt64(z["id"])).Select(z => new IdFeature()
            {
                Id = z.Key,
                Value = z.Select(x => new Feature() {
                    FeatureId = Convert.ToInt64(x["feature_id"]),
                    Value = x.Table.Columns.Contains("value") ? x["value"] != DBNull.Value ? Convert.ToString(x["value"]) : null : null,
                    ViewOrder = Convert.ToInt64(x["view_order"]),
                } ).ToList()
            }).ToList();
            return convertedList;
        }
    }


    public class Feature
    {
        [JsonProperty("feature_id")]
        public long FeatureId { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("view_order")]
        public long ViewOrder { get; set; }
    }
    

}
