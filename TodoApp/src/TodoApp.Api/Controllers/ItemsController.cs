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

        private readonly IAddItemService _addItemService;
        private readonly IGetItemByIdService _getItemByIdService;
        private readonly IItemRepository _repository;
        private readonly ILocationHelper _locationHelper;

        public ItemsController(
            IAddItemService addItemService,
            IGetItemByIdService getItemByIdService,
            IItemRepository repository,
            ILocationHelper helper)
        {
            _addItemService = addItemService;
            _repository = repository;
            _locationHelper = helper;
            _getItemByIdService = getItemByIdService;
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

            if (retrievedItem.WasFound)
            {
                return Ok(retrievedItem.Entity);
            }

            return NotFound();
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
            => Ok(await _repository.UpdateAsync(item));

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);

            return StatusCode(HttpStatusCode.NoContent);
        }

        private void ValidateGuid(Guid id)
        {
            if (id == Guid.Empty)
            {
                ModelState.AddModelError(nameof(Item.Id), EmptyId);
            }
        }

        private void ValidatePostArguments(Item item)
        {
            if (item == null)
            {
                ModelState.AddModelError("NullItem", InvalidRequestBody);
                return;
            }

            ValidateItem(item);
        }

        private void ValidateItem(Item item)
        {
            if (item.Id != Guid.Empty)
            {
                ModelState.AddModelError(nameof(Item.Id), NonEmptyId);
            }

            if (!IsTextValid(item.Text))
            {
                ModelState.AddModelError(nameof(Item.Text), InvalidText);
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