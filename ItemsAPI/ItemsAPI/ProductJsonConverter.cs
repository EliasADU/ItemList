using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemsAPI.Models;
using Newtonsoft.Json.Linq;

namespace ItemsAPI
{
    public class ProductJsonConverter : JsonCreationConverter<Item>
    {
        protected override Item Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");

            if (jObject["deadline"] != null || jObject["Deadline"] != null)
            {
                return new ItemsAPI.Models.Task();
            }
            else if (jObject["start"] != null || jObject["Start"] != null)
            {
                return new Appointment();
            }
            else
            {
                return new Item();
            }
        }
    }
}
