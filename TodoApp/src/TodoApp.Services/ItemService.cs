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

        public async Task<Item> AddItemAsync(Item item)
        {
            item.CreateTime = _dateTimeService.GetCurrentDateTime();
            return await _repository.AddAsync(item);
        }
    }
}