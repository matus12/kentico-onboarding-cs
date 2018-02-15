using System.Threading.Tasks;
using TodoApp.Contracts.Models;

namespace TodoApp.Contracts.Services
{
    public interface IUpdateItemService
    {
        Task<RetrievedEntity<Item>> UpdateItemAsync(Item item);
    }
}
