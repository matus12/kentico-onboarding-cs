using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllAsync();
        Task<Item> GetByIdAsync(Guid id);
        Task<Item> AddAsync(Item item);
        Task<Item> UpdateAsync(Guid id, Item item);
        Task DeleteAsync(Guid id);
    }
}