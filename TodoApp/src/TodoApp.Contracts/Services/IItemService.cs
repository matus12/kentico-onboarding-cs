using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts.Services
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAllItems();

        Task<Item> GetItemById(Guid id);

        Task<Item> InsertItem(Item item);

        Task<Item> UpdateItem(Guid id, Item item);

        Task DeleteItem(Guid id);
    }
}