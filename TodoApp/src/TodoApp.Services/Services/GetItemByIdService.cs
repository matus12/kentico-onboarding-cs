using System;
using System.Threading.Tasks;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;

namespace TodoApp.Services.Services
{
    internal class GetItemByIdService : IGetItemByIdService
    {
        private readonly IItemRepository _repository;

        public GetItemByIdService(IItemRepository repository) 
            => _repository = repository;

        public async Task<RetrievedEntity<Item>> GetItemByIdAsync(Guid id)
        {
            var retrievedItem = await _repository.GetByIdAsync(id);

            return retrievedItem == null
                ? RetrievedEntity<Item>.NotFound
                : new RetrievedEntity<Item>(retrievedItem);
        }
    }
}