using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment3ItemListUWP.Models;
using Newtonsoft.Json.Linq;

namespace Assignment3ItemListUWP
{
    public class ProductJsonConverter : JsonCreationConverter<Item>
    {
        protected override Item Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");

            if (jObject["deadline"] != null || jObject["Deadline"] != null)
            {
                return new Assignment3ItemListUWP.Models.Task();
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
