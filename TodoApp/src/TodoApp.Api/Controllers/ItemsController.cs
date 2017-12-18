using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoApp.Database;
using TodoApp.Database.Models;
using TodoApp.Interfaces;

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
            => await Task.FromResult(Ok(_repository.GetAll()));

        public async Task<IHttpActionResult> GetAsync(Guid id)
            => await Task.FromResult(Ok(_repository.GetById(id)));

        public async Task<IHttpActionResult> PostAsync([FromBody] Item item)
            => await Task.FromResult(Created(
                _locationHelper.GetUriLocation(_guidOfPostItem),
                _repository.Add(item)));

        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] Item item)
            => await Task.FromResult(Content(HttpStatusCode.Accepted, _repository.Update(item)));

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
            => await Task.FromResult(StatusCode(HttpStatusCode.NoContent));
    }
}