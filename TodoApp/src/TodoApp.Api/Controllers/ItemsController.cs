using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Services;

namespace TodoApp.Api.Controllers
{
    public class ItemsController : ApiController
    {
        private readonly Guid _guidOfPostItem = new Guid("e6eb4638-38a4-49ac-8aaf-878684397707");

        private readonly IItemService _service;
        private readonly ILocationHelper _locationHelper;

        public ItemsController(IItemService service, ILocationHelper helper)
        {
            _service = service;
            _locationHelper = helper;
        }

        public async Task<IHttpActionResult> GetAsync()
            => Ok(await _service.GetAllItems());

        public async Task<IHttpActionResult> GetAsync(Guid id)
            => Ok(await _service.GetItemById(id));

        public async Task<IHttpActionResult> PostAsync([FromBody] Item item)
        {
            if (item == null)
            {
                return BadRequest("Invalid request message body");
            }
            if (!ValidateText(item.Text))
            {
                return BadRequest("Invalid item text");
            }
            return Created(_locationHelper.GetUriLocation(_guidOfPostItem),
                await _service.InsertItem(item));
        }

        public async Task<IHttpActionResult> PutAsync(Guid id, [FromBody] Item item)
            => Content(HttpStatusCode.Accepted, await _service.UpdateItem(id, item));

        public async Task<IHttpActionResult> DeleteAsync(Guid id)
        {
            await _service.DeleteItem(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        private static bool ValidateText(string inputText)
        {
            var trimmedText = inputText.Trim();
            return !string.IsNullOrEmpty(trimmedText)
                && trimmedText.Length.Equals(inputText.Length);
        }
    }
}