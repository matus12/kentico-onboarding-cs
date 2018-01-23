using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoApp.Contracts;
using TodoApp.Contracts.Models;

namespace TodoApp.Database
{
    internal class ItemsRepository : IItemRepository
    {
        private const string CollectionName = "items";
        private readonly Item _updatedItem =
            new Item {Text = "item0", Id = new Guid("e6eb4638-38a4-49ac-8aaf-878684397702")};

        private readonly IMongoCollection<Item> _collection;

        public ItemsRepository(IDbConnection connection)
        {
            var client = new MongoClient(connection.DbConnectionString);
            var databaseName = MongoUrl.Create(connection.DbConnectionString).DatabaseName;
            var database = client.GetDatabase(databaseName);

            _collection = database.GetCollection<Item>(CollectionName);
        }

        public async Task<IEnumerable<Item>> GetAllAsync()
            => await _collection.Find(FilterDefinition<Item>.Empty).ToListAsync();

        public async Task<Item> GetByIdAsync(Guid id)
            => await _collection.Find(item => item.Id == id).FirstAsync();

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