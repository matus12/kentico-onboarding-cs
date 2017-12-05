using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TodoApp.Api.Models;

namespace TodoApp.Api.Controllers
{
    public class ItemsController : ApiController
    {
        private Item[] items = new Item[3]
        {
            new Item(1, "item0"),
            new Item(2, "item1"),
            new Item(3, "item2")
        };

        // GET api/<controller>
        public async Task<IHttpActionResult> Get()
        {
            return await Task.Run(() => Ok(items));
        }

        // GET api/<controller>/5
        public async Task<IHttpActionResult> Get(int id)
        {
            return await Task.Run(() => Ok(new Item(4, "New Item")));
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> Post([FromBody]Item item)
        {
            //await Task.Run(() => items.Add(item));
            return await Task.Run(() => CreatedAtRoute("DefaultApi", new { Id = 5 }, item));
            //return await Task.Run(() => Ok());
        }

        // PUT api/<controller>/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]Item item)
        {
            return await Task.Run(() => Content(HttpStatusCode.Accepted, item));
        }

        // DELETE api/<controller>/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            return await Task.Run(() => Ok());
        }
    }
}