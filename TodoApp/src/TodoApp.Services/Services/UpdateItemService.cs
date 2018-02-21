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
        private readonly IGetItemByIdService _getItemByIdService;

        public UpdateItemService(
            IItemRepository repository,
            ITimeService timeService,
            IGetItemByIdService getItemByIdService)
        {
            _repository = repository;
            _timeService = timeService;
            _getItemByIdService = getItemByIdService;
        }

        public async Task<RetrievedItem> UpdateItemAsync(Guid id, Item item)
        {
            var retrievedItem = await _getItemByIdService.GetItemByIdAsync(id);
            if (!retrievedItem.WasFound)
            {
                return retrievedItem;
            }
            var itemToUpdate = new Item
            {
                Id = id,
                Text = item.Text,
                CreatedAt = item.CreatedAt,
                ModifiedAt = _timeService.GetCurrentDateTime()
            };
            retrievedItem.Item = await _repository.UpdateAsync(id, itemToUpdate);

            return retrievedItem;
        }
    }
}
