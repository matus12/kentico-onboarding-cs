using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Contracts;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Services;

namespace TodoApp.Services
{
    internal class ItemService : IItemService
    {
        private readonly IItemRepository _repository;

        public ItemService(IItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Item>> GetAllItems()
            => await _repository.GetAll();

        public async Task<Item> GetItemById(Guid id)
            => await _repository.GetById(id);

        public async Task<Item> InsertItem(Item item)
        {
            item.CreateTime = new DateTime(DateTime.Now.Ticks);
            return await _repository.Add(item);
        }

        public async Task<Item> UpdateItem(Guid id, Item item)
        {
            item.UpdateTime = new DateTime(DateTime.Now.Ticks);
            return await _repository.Update(item);
        }

        public async Task DeleteItem(Guid id)
            => await _repository.Delete(id);
    }
}