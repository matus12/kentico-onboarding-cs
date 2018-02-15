using System;
using System.Threading.Tasks;
using TodoApp.Contracts;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Services;
using TodoApp.Contracts.Services.Factories;

namespace TodoApp.Services.Services
{
    internal class UpdateItemService : IUpdateItemService
    {
        private readonly IItemRepository _repository;
        private readonly ITimeService _timeService;

        public UpdateItemService(IItemRepository repository, ITimeService timeService)
        {
            _repository = repository;
            _timeService = timeService;
        }

        public Task<Item> UpdateItemAsync(Guid id, Item item)
        {
            var itemToUpdate = new Item
            {
                Id = id,
                Text = item.Text,
                CreatedAt = item.CreatedAt,
                ModifiedAt = _timeService.GetCurrentDateTime()
            };
            var updatedItem = _repository.UpdateAsync(id, itemToUpdate);

            return updatedItem;
        }
    }
}
