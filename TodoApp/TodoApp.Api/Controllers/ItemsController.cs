using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace TodoApp.Api.Controllers
{
    public class Item
    {
        public Item(string text)
        {
            this.text = text;
        }
        public string text;
        public Boolean isEdited;
    }

    public class ItemsController : ApiController
    {
        private static List<Item> items = new List<Item>()
        {
            new Item("item0"), new Item("item1"), new Item("item2")
        };

        /*public List<Item> GetList()
        {
            return items;
        }
        */
        private static IEnumerable<Item> GetItemsAsync()
        {
            return items;
        }

        // GET api/<controller>
        public async Task<IHttpActionResult> Get()
        {
            return await Task.Run(() => Ok(GetItemsAsync()));
        }

        private static Item GetItemById(int id)
        {
            return new Item("New Item");
        }

        // GET api/<controller>/5
        public async Task<IHttpActionResult> Get(int id)
        {
            return await Task.Run(() => Ok(GetItemById(id)));
        }

        private static void AddItem(Item item)
        {
            items.Add(item);
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> Post([FromBody]Item item)
        {
            await Task.Run(() => AddItem(item));
            return Ok();
        }

        private void UpdateItem(int id, Item item)
        {
            items[id] = item;
        }

        // PUT api/<controller>/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]Item item)
        {
            await Task.Run(() => UpdateItem(id, item));
            return Ok();
        }

        private static void DeleteItem(int id)
        {
            items.RemoveAt(id);
        }

        // DELETE api/<controller>/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            await Task.Run(() => DeleteItem(id));
            return Ok();
        }
    }
}