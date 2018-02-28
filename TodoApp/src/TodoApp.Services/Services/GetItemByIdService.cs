using System;
using System.Threading.Tasks;
using TodoApp.Contracts;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;

namespace TodoApp.Services.Services
{
    internal class GetItemByIdService : IGetItemByIdService
    {
        private readonly IItemRepository _repository;

        public GetItemByIdService(IItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<RetrievedItem> GetItemByIdAsync(Guid id)
        {
            var retrievedItem = await _repository.GetByIdAsync(id);
            return retrievedItem == null
                ? new RetrievedItem {WasFound = false}
                : new RetrievedItem {WasFound = true, Item = retrievedItem};
        }
    }
}