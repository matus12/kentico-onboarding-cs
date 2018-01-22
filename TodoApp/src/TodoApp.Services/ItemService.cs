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
        private readonly IDateTimeService _dateTimeService;

        public ItemService(IItemRepository repository, IDateTimeService dateTimeService)
        {
            _repository = repository;
            _dateTimeService = dateTimeService;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
            => await _repository.GetAllAsync();

        public async Task<Item> GetItemByIdAsync(Guid id)
            => await _repository.GetByIdAsync(id);

        public async Task<Item> AddItemAsync(Item item)
        {
            item.CreateTime = _dateTimeService.GetCurrentDateTime();
            return await _repository.AddAsync(item);
        }

        public async Task<Item> UpdateItemAsync(Item item)
        {
            item.UpdateTime = _dateTimeService.GetCurrentDateTime();
            return await _repository.UpdateAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
            => await _repository.DeleteAsync(id);
    }
}