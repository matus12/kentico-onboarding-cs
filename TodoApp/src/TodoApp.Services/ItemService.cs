using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Contracts;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Services;
using System.Text.RegularExpressions;

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
            var regex = new Regex(@"/\w/");
            var isTextValid = regex.IsMatch(item.Text);
            if (isTextValid)
            {
                return await _repository.Add(item);
            }

            return await Task.FromResult<Item>(null);
        }

        public async Task<Item> UpdateItem(Guid id, Item item)
        {
            item.UpDateTime = new DateTime(DateTime.Now.Ticks);
            return await _repository.Update(item);
        }

        public async Task DeleteItem(Guid id)
        {
            await _repository.Delete(id);
        }
    }
}