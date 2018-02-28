using System.Threading.Tasks;
using TodoApp.Contracts;
using TodoApp.Contracts.Factories;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;

namespace TodoApp.Services.Services
{
    internal class AddItemService : IAddItemService
    {
        private readonly IItemRepository _repository;
        private readonly ITimeFactory _timeService;
        private readonly IGuidFactory _guidService;

        public AddItemService(IItemRepository repository, ITimeFactory timeService, IGuidFactory guidService)
        {
            _repository = repository;
            _timeService = timeService;
            _guidService = guidService;
        }

        public async Task<Item> AddItemAsync(Item item)
        {
            var currentTime = _timeService.GetCurrentDateTime();
            var newItem = new Item
            {
                Id = _guidService.GenerateGuid(),
                Text = item.Text,
                CreatedAt = currentTime,
                ModifiedAt = currentTime
            };
            var addedItem = await _repository.AddAsync(newItem);
            return addedItem;
        }
    }
}