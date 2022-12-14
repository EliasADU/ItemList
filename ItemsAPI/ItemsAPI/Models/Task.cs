using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ItemsAPI.Models
{
    public class Task : Item 
    {
        public string deadline { get; set; }
        public bool isCompleted { get; set; }

        //public Task()
        //{

        //}

        //public Task(string n, string des, string dea, bool iscomp, string prio, Guid myId)
        //{
        //    name = n;
        //    description = des;
        //    deadline = dea;
        //    isCompleted = iscomp;
        //    priority = prio;
        //    id = myId;
        //}
    }
}
