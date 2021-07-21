using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BmpWebSyncLogic.helpers
{
    public static class JsonHelper
    {
        public static string ToJson(object o)
        {
            return JsonConvert.SerializeObject(o, BmpWebSyncLogic.entities.Converter.Settings);
        }
    }
}
