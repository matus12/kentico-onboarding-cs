using System.Threading.Tasks;
using TodoApp.Contracts.Factories;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;

namespace TodoApp.Services.Services
{
    internal class AddItemService : IAddItemService
    {
        private readonly IItemRepository _repository;
        private readonly ITimeFactory _timeFactory;
        private readonly IGuidFactory _guidFactory;

        public AddItemService(IItemRepository repository, ITimeFactory timeFactory, IGuidFactory guidFactory)
        {
            _repository = repository;
            _timeFactory = timeFactory;
            _guidFactory = guidFactory;
        }

        public async Task<Item> AddItemAsync(Item item)
        {
            var currentTime = _timeFactory.GetCurrentDateTime();
            var newItem = new Item
            {
                Id = _guidFactory.GenerateGuid(),
                Text = item.Text,
                CreatedAt = currentTime,
                ModifiedAt = currentTime
            };
            var addedItem = await _repository.AddAsync(newItem);

            return addedItem;
        }
    }
}