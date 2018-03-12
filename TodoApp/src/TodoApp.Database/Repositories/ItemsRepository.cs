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
        private readonly Item _updatedItem =
            new Item {Text = "item0", Id = new Guid("e6eb4638-38a4-49ac-8aaf-878684397702")};

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

        public async Task<Item> UpdateAsync(Item item)
            => await Task.FromResult(_updatedItem);

        public async Task DeleteAsync(Guid id)
            => await Task.CompletedTask;
    }
}