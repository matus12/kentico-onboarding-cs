using System;
using System.Threading.Tasks;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts.Services
{
    public interface IUpdateItemService
    {
        Task<Item> UpdateItemAsync(Guid id, Item item);
    }
}
