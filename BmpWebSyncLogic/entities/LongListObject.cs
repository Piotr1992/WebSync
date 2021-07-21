using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BmpWebSyncLogic.entities
{
    public class LongListObject
    {
        [JsonIgnore]
        public long Id { get; set; }
        public List<long> Value { get; set; }

        public static List<LongListObject> FillList(DataTable dt)
        {
            var convertedList = 
            dt.Rows.OfType<DataRow>().GroupBy(z => Convert.ToInt64(z["id"])).Select(z => new LongListObject(){
                Id = z.Key,
                Value = z.Select(x => Convert.ToInt64(x["value"])).ToList()
            }).ToList();
            return convertedList;
        }
    }
}
