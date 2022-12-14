using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemsAPI.Models;
using System.Data.SqlClient;
namespace ItemsAPI.Controllers
{
    [ApiController]
    [Route("Items")]
    public class ItemListController : ControllerBase
    {

        [HttpGet("ping")]
        public string Test()
        {
            return "pong!";
        }

        [HttpGet("GetAll")]
        public ActionResult<List<Item>> Get()
        {
            List<Item> list = DBHelper.GetAllItems();
            return Ok(list);
        }

        [HttpGet("GetAllOutstanding")]
        public ActionResult<List<Item>> GetOutStanding()
        {
            List<Item> list = DBHelper.GetAllOutstanding();

            return Ok(list);
        }

        [HttpGet("GetSorted")]
        public ActionResult<List<Item>> GetSorted()
        {
            List<Item> list = DBHelper.GetAllItems();

            list = list.OrderBy(s => s.priority).ToList();
            return Ok(list);
        }

        [HttpPost("GetFromSearch")]
        public ActionResult<List<Item>> GetFromSearch([FromBody] string searchfor)
        {
            return Ok(DBHelper.GetFromSearch(searchfor));
        }

        [HttpPost("AddOrUpdate")]
        public ActionResult<Item> AddOrUpdate([FromBody] Item sentItem)
        {

            //call InsertTask or UpdateTask using the id

            if (sentItem == null)
            {
                return BadRequest();
            }

            if (sentItem.id == 0)
            {
                DBHelper.InsertItem(sentItem);
            }
            else
            {
                DBHelper.UpdateItem(sentItem);
                //var itemToSync = DataContext.Items.FirstOrDefault(t => t.id.Equals(sentItem.id));
                //var index = DataContext.Items.IndexOf(itemToSync);
                //DataContext.Items.RemoveAt(index);
                //DataContext.Items.Insert(index, sentItem);
            }
            return Ok(sentItem);
        }

        [HttpPost("Delete")]
        public ActionResult<Item> Delete([FromBody] int id)
        {
            DBHelper.DeleteItem(id);

            //var ticketToRemove = DataContext.Items.FirstOrDefault(t => t.id.Equals(id));
            //if (ticketToRemove?.id != Guid.Empty)
            //{
            //    DataContext.Items.Remove(ticketToRemove);
            //}

            return Ok();
        }
    }
}
