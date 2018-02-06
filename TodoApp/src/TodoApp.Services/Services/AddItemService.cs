using System.Threading.Tasks;
using TodoApp.Contracts;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Services;
using TodoApp.Contracts.Services.Factories;

namespace TodoApp.Services.Services
{
    internal class AddItemService : IAddItemService
    {
        private readonly IItemRepository _repository;
        private readonly ITimeService _timeService;
        private readonly IGuidService _guidService;

        public AddItemService(IItemRepository repository, ITimeService timeService, IGuidService guidService)
        {
            _repository = repository;
            _timeService = timeService;
            _guidService = guidService;
        }

        public async Task<Item> AddItemAsync(Item item)
        {
            var currentTime = _timeService.GetCurrentDateTime();
            item.CreatedAt = currentTime;
            item.ModifiedAt = currentTime;
            item.Id = _guidService.GenerateGuid();

            return await _repository.AddAsync(item);
        }
    }
}