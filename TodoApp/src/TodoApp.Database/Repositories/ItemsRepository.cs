using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;

namespace TodoApp.Database.Repositories
{
    internal class ItemsRepository : IItemRepository
    {
        private const string CollectionName = "items";

        private readonly IMongoCollection<Item> _collection;

        public ItemsRepository(IDbConnection connection)
        {
            var mongoUrl = MongoUrl.Create(connection.DbConnectionString);
            var client = new MongoClient(mongoUrl);
            var database = client.GetDatabase(mongoUrl.DatabaseName);

            _collection = database.GetCollection<Item>(CollectionName);
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
            => await _collection.Find(FilterDefinition<Item>.Empty).ToListAsync();

        public async Task<Item> GetByIdAsync(Guid id)
            => await _collection.Find(item => item.Id == id).FirstOrDefaultAsync();

        public async Task<Item> AddAsync(Item item)
        {
            await _collection.InsertOneAsync(item);

            return item;
        }

        public async Task<Item> UpdateAsync(Item updatedItem)
            => await _collection.FindOneAndReplaceAsync(item => item.Id == updatedItem.Id, updatedItem);

        public async Task DeleteAsync(Guid id)
            => await Task.CompletedTask;
    }
}