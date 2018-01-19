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

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
            => await _repository.GetAllAsync();

        public async Task<Item> GetItemByIdAsync(Guid id)
            => await _repository.GetByIdAsync(id);

        public async Task<Item> AddItemAsync(Item item)
        {
            item.CreateTime = new DateTime(DateTime.Now.Ticks);
            return await _repository.AddAsync(item);
        }

        public async Task<Item> UpdateItemAsync(Item item)
        {
            item.UpdateTime = new DateTime(DateTime.Now.Ticks);
            return await _repository.UpdateAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}