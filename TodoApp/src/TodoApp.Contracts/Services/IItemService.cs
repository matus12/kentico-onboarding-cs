using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts.Services
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();

        Task<Item> GetItemByIdAsync(Guid id);

        Task<Item> AddItemAsync(Item item);

        Task<Item> UpdateItemAsync(Item item);

        Task DeleteItemAsync(Guid id);
    }
}