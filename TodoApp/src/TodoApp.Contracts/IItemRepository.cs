using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAll();
        Task<Item> GetById(Guid id);
        Task<Item> Add(Item item);
        Task<Item> Update(Item item);
        Task Delete(Guid id);
    }
}