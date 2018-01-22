using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using TodoApp.Contracts;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Services;

namespace TodoApp.Api.Controllers
{
    public class ItemsController : ApiController
    {
        private readonly Guid _guidOfPostItem = new Guid("e6eb4638-38a4-49ac-8aaf-878684397707");

        private readonly IItemService _service;
        private readonly IItemRepository _repository;
        private readonly ILocationHelper _locationHelper;

        public ItemsController(IItemService service, IItemRepository repository, ILocationHelper helper)
        {
            _service = service;
            _repository = repository;
            _locationHelper = helper;
        }

        public async Task<IHttpActionResult> GetAsync()
            => Ok(await _repository.GetAllAsync());

        public async Task<IHttpActionResult> GetAsync(Guid id)
            => Ok(await _repository.GetByIdAsync(id));

        public async Task<IHttpActionResult> PostAsync([FromBody] Item item)
        {
            var itemValidation = ValidateItem(item);
            if (itemValidation != null)
            {
                return itemValidation;
            }
            var location = _locationHelper.GetUriLocation(_guidOfPostItem);
            var addedItem = await _service.AddItemAsync(item);

            return Created(location, addedItem);
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] Item item)
            => Ok(await _repository.UpdateAsync(item));

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        private BadRequestErrorMessageResult ValidateItem(Item item)
        {
            if (item == null)
            {
                return BadRequest("Invalid request message body");
            }
            return !ValidateText(item.Text) ? BadRequest("Invalid item text") : null;
        }

        private static bool ValidateText(string inputText)
        {
            var trimmedText = inputText.Trim();
            return !string.IsNullOrEmpty(trimmedText)
                && trimmedText.Length.Equals(inputText.Length);
        }
    }
}