using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BmpWebSyncLogic.entities
{
    public class LongLongObject
    {
        [JsonIgnore]
        public long Id { get; set; }
        public long Value { get; set; }

        public static List<LongLongObject> FillList(DataTable dt)
        {
            var convertedList = (from rw in dt.AsEnumerable()
                                 select new LongLongObject()
                                 {
                                     Id = Convert.ToInt64(rw["id"]),
                                     Value = Convert.ToInt64(rw["value"])
                                 }).ToList();
            return convertedList;
        }
    }
}
