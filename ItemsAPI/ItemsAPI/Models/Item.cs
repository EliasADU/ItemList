using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ItemsAPI.Models
{
    [JsonConverter(typeof(ProductJsonConverter))]
    public class Item
    {
        public string name { get; set; }
        public string description { get; set; }
        public string priority { get; set; }
        public int id { get; set; }
    }
}
