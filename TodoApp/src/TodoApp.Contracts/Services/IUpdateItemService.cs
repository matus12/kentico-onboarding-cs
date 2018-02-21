using System;
using System.Threading.Tasks;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts.Services
{
    public interface IUpdateItemService
    {
        Task<RetrievedItem> UpdateItemAsync(Guid id, Item item);
    }
}
