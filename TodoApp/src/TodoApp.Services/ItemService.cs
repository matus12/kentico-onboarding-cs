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
        private readonly IGuidService _guidService;

        public ItemService(IItemRepository repository, IDateTimeService dateTimeService, IGuidService guidService)
        {
            _repository = repository;
            _dateTimeService = dateTimeService;
            _guidService = guidService;
        }

        public async Task<Item> AddItemAsync(Item item)
        {
            item.CreateTime = _dateTimeService.GetCurrentDateTime();
            item.Id = _guidService.GenerateGuid();

            return await _repository.AddAsync(item);
        }
    }
}