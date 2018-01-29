using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoApp.Contracts;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Services;

namespace TodoApp.Api.Controllers
{
    public class ItemsController : ApiController
    {
        private const string InvalidRequestBody = "Invalid request message body";
        private const string InvalidText = "Invalid item text";

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
        {
            if (!ValidateGuid(id))
            {
                return BadRequest("Empty id");
            }
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
            {
                return BadRequest("Item with given id doesn't exist");
            }

            return Ok(item);
        }

        public async Task<IHttpActionResult> PostAsync([FromBody] Item item)
        {
            var errorMessage = ValidateItem(item);
            if (errorMessage != null)
            {
                return BadRequest(errorMessage);
            }
            var addedItem = await _service.AddItemAsync(item);
            var location = _locationHelper.GetUriLocation(addedItem.Id);

            return Created(location, addedItem);
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] Item item)
            => Ok(await _repository.UpdateAsync(item));

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }

        private static bool ValidateGuid(Guid id) => id != Guid.Empty;

        private static string ValidateItem(Item item)
        {
            if (item == null)
            {
                return InvalidRequestBody;
            }

            return !ValidateText(item.Text) ? InvalidText : null;
        }

        private static bool ValidateText(string inputText)
        {
            var trimmedText = inputText.Trim();

            return !string.IsNullOrEmpty(trimmedText)
                && trimmedText.Length.Equals(inputText.Length);
        }
    }
}