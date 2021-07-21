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
    public class Files
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonIgnore]
        byte[] DabDane { get; set; }
        [JsonIgnore]
        long DabRozmiar { get; set; }
        [JsonIgnore]
        public bool Uploaded { get; set; }

        public byte[] GetFile()
        {
            if (DabRozmiar != 0)
            {
                return helpers.ZipCompress.DeCompress(DabDane);
            }
            else
                return DabDane;
            
        }
        

        public static List<Files> FillFiles()
        {

            var logWatch = System.Diagnostics.Stopwatch.StartNew();
            DataSet ds = DBHelper.RunSqlProc("bmp.b2b_files");
            var t = new List<Tuple<string, long>>();
            logWatch.Stop();
            t.Add(Tuple.Create("baza", logWatch.ElapsedMilliseconds));
            logWatch.Reset();
            logWatch.Start();
            var files = Files.FillList(ds.Tables[0]);
            logWatch.Stop();
            t.Add(Tuple.Create("fill", logWatch.ElapsedMilliseconds));
            logWatch.Reset();


            return (List<Files>)files;
        }
        private static List<Files> FillList(DataTable dt)
        {
            var convertedList = (from rw in dt.AsEnumerable()
                                 select new Files()
                                 {
                                     Id = Convert.ToInt64(rw["id"]),
                                     Path = rw.Table.Columns.Contains("path") ? rw["path"] != DBNull.Value ? Convert.ToString(rw["path"]) : null : null,
                                     Date = rw.Table.Columns.Contains("date") ? rw["date"] != DBNull.Value ? Convert.ToString(rw["date"]) : null : null,
                                     Name = rw.Table.Columns.Contains("name") ? rw["name"] != DBNull.Value ? Convert.ToString(rw["name"]) : null : null,
                                     Type = rw.Table.Columns.Contains("type") ? rw["type"] != DBNull.Value ? Convert.ToString(rw["type"]) : null : null,
                                     DabDane = rw.Table.Columns.Contains("dabdane") ?  (byte[])rw["dabdane"] : new byte[0],
                                     DabRozmiar = Convert.ToInt64(rw["dabrozmiar"])
                                 }).ToList();
            return convertedList;
        }
        public static List<Files> FromJson(string json) => JsonConvert.DeserializeObject<List<Files>>(json, BmpWebSyncLogic.entities.Converter.Settings);
    }

}
