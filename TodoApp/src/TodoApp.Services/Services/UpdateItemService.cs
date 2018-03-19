using System.Threading.Tasks;
using TodoApp.Contracts.Factories;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;

namespace TodoApp.Services.Services
{
    internal class UpdateItemService : IUpdateItemService
    {
        private readonly IItemRepository _repository;
        private readonly ITimeFactory _timeFactory;
        private readonly IGetItemByIdService _getItemByIdService;

        public UpdateItemService(
            IItemRepository repository,
            ITimeFactory timeFactory,
            IGetItemByIdService getItemByIdService
        )
        {
            _repository = repository;
            _timeFactory = timeFactory;
            _getItemByIdService = getItemByIdService;
        }

        public async Task<RetrievedEntity<Item>> UpdateItemAsync(Item item)
        {
            var retrievedItem = await _getItemByIdService.GetItemByIdAsync(item.Id);
            if (!retrievedItem.WasFound)
            {
                return retrievedItem;
            }

            var itemToUpdate = new Item
            {
                Id = item.Id,
                Text = item.Text,
                CreatedAt = retrievedItem.Entity.CreatedAt,
                ModifiedAt = _timeFactory.GetCurrentDateTime()
            };
            await _repository.UpdateAsync(itemToUpdate);

            return new RetrievedEntity<Item>(itemToUpdate);
        }
    }
}