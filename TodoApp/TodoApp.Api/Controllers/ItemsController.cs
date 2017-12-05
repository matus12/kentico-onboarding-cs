using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TodoApp.Api.Models;

namespace TodoApp.Api.Controllers
{
    public class ItemsController : ApiController
    {
        public List<Item> items = new List<Item>()
        {
            new Item("item0"), new Item("item1"), new Item("item2")
        };

        /*public List<Item> GetList()
        {
            return items;
        }
        */

        // GET api/<controller>
        public async Task<IHttpActionResult> Get()
        {
            return await Task.Run(() => Ok(items));
        }

        // GET api/<controller>/5
        public async Task<IHttpActionResult> Get(int id)
        {
            return await Task.Run(() => Ok(new Item("New Item")));
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> Post([FromBody]Item item)
        {
            await Task.Run(() => items.Add(item));
            return Ok();
        }

        // PUT api/<controller>/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]Item item)
        {
            await Task.Run(() => items[id] = item);
            return Ok();
        }

        // DELETE api/<controller>/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            await Task.Run(() => items.RemoveAt(id));
            return Ok();
        }
    }
}