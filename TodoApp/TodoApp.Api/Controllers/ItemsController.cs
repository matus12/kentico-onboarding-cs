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

    public class Items : IEnumerable
    {
        private Item[] items;

        public Items(Item[] pArray)
        {
            items = new Item[pArray.Length];

            for (int i = 0; i < pArray.Length; i++)
            {
                items[i] = pArray[i];
            }
        }
        public IEnumerator GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }

        public ItemEnum GetEmumerator()
        {
            return new ItemEnum(items);
        }
    }

    public class ItemEnum : IEnumerator
    {
        public Item[] Items;
        private int Position = -1;

        public ItemEnum(Item[] list)
        {
            Items = list;
        }
        public bool MoveNext()
        {
            Position++;
            return (Position < Items.Length);
        }

        public void Reset()
        {
            Position = -1;
        }

        public object Current
        {
            get
            {
                try
                {
                    return Items[Position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }

    public class ItemsController : ApiController
    {
        private static List<Item> items = new List<Item>()
        {
            new Item("item0"), new Item("item1"), new Item("item2")
        };

        private static IEnumerable<Item> GetItemsAsync()
        {
            return items;
        }

        // GET api/<controller>
        public async Task<IEnumerable<Item>> Get()
        {
            return await Task.Run(() => GetItemsAsync());
        }

        private static Item GetItemById(int id)
        {
            return items[id];
        }

        // GET api/<controller>/5
        public async Task<Item> Get(int id)
        {
            return await Task.Run(() => GetItemById(id));
        }

        private static void AddItem(Item item)
        {
            items.Add(item);
        }

        // POST api/<controller>
        public async Task Post([FromBody]Item item)
        {
            await Task.Run(() => AddItem(item));
        }

        private void UpdateItem(int id, Item item)
        {
            items[id] = item;
        }

        // PUT api/<controller>/5
        public async Task Put(int id, [FromBody]Item item)
        {
            await Task.Run(() => UpdateItem(id, item));
        }

        private static void DeleteItem(int id)
        {
            items.RemoveAt(id);
        }

        // DELETE api/<controller>/5
        public async Task Delete(int id)
        {
            await Task.Run(() => DeleteItem(id));
        }
    }
}