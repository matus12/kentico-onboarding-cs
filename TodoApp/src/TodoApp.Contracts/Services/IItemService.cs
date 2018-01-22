using System.Threading.Tasks;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts.Services
{
    public interface IItemService
    {
        Task<Item> AddItemAsync(Item item);
    }
}