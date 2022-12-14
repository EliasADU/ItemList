using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemsAPI.Models
{
    public class Appointment : Item
    {
        public DateTimeOffset start { get; set; }
        public DateTimeOffset end { get; set; }
        public List<string> attendees { get; set; }

        //public Appointment()
        //{

        //}

        //public Appointment(string n, string des, DateTimeOffset s, DateTimeOffset e, List<string> att, string prio, Guid myId)
        //{
        //    name = n;
        //    description = des;
        //    start = s;
        //    end = e;
        //    attendees = att;
        //    priority = prio;
        //    id = myId;
        //}
    }
}
