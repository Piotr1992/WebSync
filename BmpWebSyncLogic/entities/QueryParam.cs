using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Helpers
{
    public class QueryParam
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public QueryParam(string Name, object Value)
        {
            this.Name = Name;
            this.Value = Value;
        }
    }
}