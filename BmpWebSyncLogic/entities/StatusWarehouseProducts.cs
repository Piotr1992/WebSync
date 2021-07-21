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
    public class StatusWarehouseProducts
    {
        [JsonProperty("product_id")]
        public long Id { get; set; }        

        [JsonProperty("warehouse")]
        public long Status { get; set; }        


        public static List<StatusWarehouseProducts> FromJson(string json) => JsonConvert.DeserializeObject<List<StatusWarehouseProducts>>(json, BmpWebSyncLogic.entities.Converter.Settings);


        private static List<StatusWarehouseProducts> FillList(DataTable dt)
        {
            var convertedList = (from rw in dt.AsEnumerable()
                                 select new StatusWarehouseProducts()
                                 {
                                     Id = Convert.ToInt64(rw["product_id"]),                                     
                                     Status = Convert.ToInt64(rw["warehouse"]),                                     
                                 }).ToList();

            return convertedList;
        }


        public static List<StatusWarehouseProducts> GetStatusWarehouse()
        {
            var logWatch = System.Diagnostics.Stopwatch.StartNew();
            DataSet ds = DBHelper.RunSqlProc("bmp.b2b_setStatusWarehousesProducts");
            var t = new List<Tuple<string, long>>();
            logWatch.Stop();
            t.Add(Tuple.Create("baza", logWatch.ElapsedMilliseconds));
            logWatch.Reset();
            logWatch.Start();
            var products = StatusWarehouseProducts.FillList(ds.Tables[0]);                       
            logWatch.Stop();
            t.Add(Tuple.Create("fill", logWatch.ElapsedMilliseconds));
            logWatch.Reset();            
            t.Add(Tuple.Create("paraller each", logWatch.ElapsedMilliseconds));

            return (List<StatusWarehouseProducts>)products;
        }

    }
}
