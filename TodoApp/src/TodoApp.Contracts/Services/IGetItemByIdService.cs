using System;
using System.Threading.Tasks;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts.Services
{
    public interface IGetItemByIdService
    {
        Task<RetrievedItem> GetItemByIdAsync(Guid id);
    }
}
