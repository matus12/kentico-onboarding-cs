using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Interfaces.Models;

namespace TodoApp.Interfaces
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