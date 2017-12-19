using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoApp.Interfaces;
using TodoApp.Interfaces.Helpers;
using TodoApp.Interfaces.Models;

namespace TodoApp.Api.Controllers
{
    public class ItemsController : ApiController
    {
        private readonly Guid _guidOfPostItem = new Guid("e6eb4638-38a4-49ac-8aaf-878684397707");

        private readonly IItemRepository _repository;
        private readonly ILocationHelper _locationHelper;

        public ItemsController(IItemRepository repository, ILocationHelper helper)
        {
            _repository = repository;
            _locationHelper = helper;
        }

        public async Task<IHttpActionResult> GetAsync()
            => Ok(await _repository.GetAll());

        public async Task<IHttpActionResult> GetAsync(Guid id)
            => Ok(await _repository.GetById(id));

        public async Task<IHttpActionResult> PostAsync([FromBody] Item item)
            => Created(_locationHelper.GetUriLocation(_guidOfPostItem),
                await _repository.Add(item));

        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] Item item)
            => Content(HttpStatusCode.Accepted, await _repository.Update(item));

        public IHttpActionResult DeleteAsync(Guid id)
        {
            await _repository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}