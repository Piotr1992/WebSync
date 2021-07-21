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
using System.Windows.Forms;
using BmpWebSyncLogic.helpers;
using Helpers;


namespace BmpWebSyncLogic.entities
{
    public class ProductGroup
    {
        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Id { get; set; }

        [JsonProperty("parent")]
        public long? Parent { get; set; }

        [JsonProperty("name")]
        public LangValue Name { get; set; }

        [JsonProperty("code")]
        public LangValue Code { get; set; }

        [JsonProperty("files")]
        public List<long> Files { get; set; }

        [JsonIgnore]
        public long LastMod { get; set; }

        public List<ProductGroup> FromJson(string json) => JsonConvert.DeserializeObject<List<ProductGroup>>(json, BmpWebSyncLogic.entities.Converter.Settings);
        public string ToJson() => JsonConvert.SerializeObject(this, BmpWebSyncLogic.entities.Converter.Settings);


        private static List<ProductGroup> FillList(DataTable dt)
        {
            var convertedList = (from rw in dt.AsEnumerable()
                                 select new ProductGroup()
                                 {
                                     Id = Convert.ToInt64(rw["id"]),
                                     Parent = rw["parent"] != DBNull.Value ? Convert.ToInt64(rw["parent"]) : (long?)null,
                                     LastMod = Convert.ToInt64(rw["lastmod"])
                                 }).ToList();

            return convertedList;
        }

        
        public static List<ProductGroup> GetGroups()
        {
            DataSet ds = DBHelper.RunSqlProc("bmp.b2b_productGroup");

            var productGroups = ProductGroup.FillList(ds.Tables[0]);
            var names = LangValue.FillList(ds.Tables[1]);
            var codes = LangValue.FillList(ds.Tables[2]);
            var files = LongLongObject.FillList(ds.Tables[3]);

            foreach (ProductGroup pg in productGroups)
            {
                pg.Name = names.Where(x => pg.Id == x.Id).FirstOrDefault();
                pg.Code = codes.Where(x => pg.Id == x.Id).FirstOrDefault();
                pg.Files = files.Where(x => pg.Id == x.Id).Select(x => x.Value).ToList();                                                                                
            }

            return productGroups;
        }


        public static void UpdateArchiveGroups(bool trueOrFalse, List<ProductGroup> _groups)
        {

            var fromHelper = _groups.Select(z => new { id = z.Id, lastmod = z.LastMod });

            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("lastmod");

            foreach (var g in fromHelper)
            {
                dt.Rows.Add(g.id, g.lastmod);
            }

            DBHelper.RunSqlProcSupplementDataProductGroup2("bmp.b2b_divisionDataSynchronizedAndNot", trueOrFalse, dt);

            MessageBox.Show("ABCD");
        }


    }           


    internal class ParseStringConverter : JsonConverter
    {

        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);


        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }


        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());            
            return;
        }


        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

}
