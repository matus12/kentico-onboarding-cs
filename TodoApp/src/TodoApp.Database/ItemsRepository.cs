using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Contracts;
using TodoApp.Contracts.Models;

namespace TodoApp.Database
{
    public class ItemsRepository : IDisposable, IItemRepository
    {
        private static IEnumerable<Item> IteratedItems
        {
            get
            {
                yield return new Item {Text = "item0", Id = new Guid("e6eb4638-38a4-49ac-8aaf-878684397702")};
                yield return new Item {Text = "item1", Id = new Guid("a5d4b549-bdd3-4ec2-8210-ff42926aa141")};
                yield return new Item {Text = "item2", Id = new Guid("45c4fb8b-1cdf-42ca-8a61-67fd7f781057")};
            }
        }

        private static readonly Item[] Items = IteratedItems.ToArray();

        private static readonly Item AddedItem =
            new Item {Text = "itemToPost", Id = new Guid("e6eb4638-38a4-49ac-8aaf-878684397707")};

        public async Task<IEnumerable<Item>> GetAll()
        {
            return await Task.FromResult(Items);
        }

        public async Task<Item> GetById(Guid id)
        {
            return await Task.FromResult(Items[0]);
        }

        public async Task<Item> Add(Item item)
        {
            return await Task.FromResult(AddedItem);
        }

        public async Task<Item> Update(Item item)
        {
            return await Task.FromResult(Items[1]);
        }

        public async Task Delete(Guid id)
        {
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}