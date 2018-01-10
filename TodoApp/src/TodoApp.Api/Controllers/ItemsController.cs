using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoApp.Contracts;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;

namespace TodoApp.Api.Controllers
{
    public class ItemsController : ApiController
    {
        private readonly IItemRepository _repository;
        private readonly ILocationHelper _locationHelper;

        public ItemsController(IItemRepository repository, ILocationHelper helper)
        {
            _repository = repository;
            _locationHelper = helper;
        }

        public async Task<IHttpActionResult> GetAsync()
            => Ok(await _repository.GetAllAsync());

        public async Task<IHttpActionResult> GetAsync(Guid id)
            => Ok(await _repository.GetByIdAsync(id));

        public async Task<IHttpActionResult> PostAsync([FromBody] Item item)
        {
            var addedItem = await _repository.AddAsync(item);

            return Created(_locationHelper.GetUriLocation(addedItem.Id),
                addedItem);
        } 

        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] Item item)
            => Ok(await _repository.UpdateAsync(item));

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}