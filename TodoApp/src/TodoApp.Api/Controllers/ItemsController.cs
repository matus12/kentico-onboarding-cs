using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;

namespace TodoApp.Api.Controllers
{
    public class ItemsController : ApiController
    {
        private const string InvalidRequestBody = "Message body can't be empty";
        private const string InvalidText = "Invalid item text";
        private const string NonEmptyId = "Id of item can't be set";
        private const string EmptyId = "Id can't be empty";
        private const string InvalidIds = "Id in url and body does not match";

        private readonly IAddItemService _addItemService;
        private readonly IGetItemByIdService _getItemByIdService;
        private readonly IUpdateItemService _updateItemService;
        private readonly IItemRepository _repository;
        private readonly ILocationHelper _locationHelper;

        public ItemsController(
            IAddItemService addItemService,
            IGetItemByIdService getItemByIdService,
            IUpdateItemService updateItemService,
            IItemRepository repository,
            ILocationHelper helper)
        {
            _addItemService = addItemService;
            _getItemByIdService = getItemByIdService;
            _updateItemService = updateItemService;
            _repository = repository;
            _locationHelper = helper;
        }

        public async Task<IHttpActionResult> GetAsync()
            => Ok(await _repository.GetAllAsync());

        public async Task<IHttpActionResult> GetAsync(Guid id)
        {
            ValidateGuid(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var retrievedItem = await _getItemByIdService.GetItemByIdAsync(id);

            try
            {
                return Ok(retrievedItem.Item);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        public async Task<IHttpActionResult> PostAsync([FromBody] Item item)
        {
            ValidatePostArguments(item);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedItem = await _addItemService.AddItemAsync(item);
            var location = _locationHelper.GetUriLocation(addedItem.Id);

            return Created(location, addedItem);
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] Item item)
        {
            ValidatePutArguments(id, item);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedItem = await _updateItemService.UpdateItemAsync(id, item);

            if (updatedItem.WasFound)
            {
                return Ok(updatedItem.Item);
            }

            return NotFound();
        }

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }

        private void ValidatePutArguments(Guid id, Item item)
        {
            if (item == null)
            {
                ModelState.AddModelError("NullItem", InvalidRequestBody);
                return;
            }

            if (item.Id != id)
            {
                ModelState.AddModelError("NonMatchingIds", InvalidIds);
                return;
            }

            if (!IsTextValid(item.Text))
            {
                ModelState.AddModelError("InvalidText", InvalidText);
            }
        }

        private void ValidatePostArguments(Item item)
        {
            if (item == null)
            {
                ModelState.AddModelError("NullItem", InvalidRequestBody);
                return;
            }

            ValidatePostItem(item);
        }

        private void ValidatePostItem(Item item)
        {
            if (item.Id != Guid.Empty)
            {
                ModelState.AddModelError("NonEmptyGuid", NonEmptyId);
            }

            if (!IsTextValid(item.Text))
            {
                ModelState.AddModelError("InvalidText", InvalidText);
            }
        }

        private void ValidateGuid(Guid id)
        {
            if (id == Guid.Empty)
            {
                ModelState.AddModelError("EmptyGuid", EmptyId);
            }
        }

        private static bool IsTextValid(string inputText)
        {
            var trimmedText = inputText.Trim();

            return !string.IsNullOrEmpty(trimmedText)
                && trimmedText.Length.Equals(inputText.Length);
        }
    }
}
