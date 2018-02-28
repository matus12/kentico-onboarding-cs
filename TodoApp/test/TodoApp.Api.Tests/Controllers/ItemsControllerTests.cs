using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using NUnit.Framework;
using TodoApp.Api.Controllers;
using NSubstitute;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;
using TodoApp.Tests.Base.Comparers;

namespace TodoApp.Api.Tests.Controllers
{
    [TestFixture]
    internal class ItemsControllerTests
    {
        private const string UriString = "/e6eb4638-38a4-49ac-8aaf-878684397707/test-route/15";

        private static readonly Uri Uri = new Uri(UriString, UriKind.Relative);
        private static readonly Guid Guid0 = new Guid("e6eb4638-38a4-49ac-8aaf-878684397702");
        private static readonly Guid Guid1 = new Guid("a5d4b549-bdd3-4ec2-8210-ff42926aa141");
        private static readonly Guid Guid2 = new Guid("45c4fb8b-1cdf-42ca-8a61-67fd7f781057");

        private static readonly Item ItemToPost =
            new Item {Text = "itemToPost", Id = new Guid("e6eb4638-38a4-49ac-8aaf-878684397707")};

        private static IEnumerable<Item> InvalidItems
        {
            get
            {
                yield return null;
                yield return new Item {Text = " invalid text  "};
                yield return new Item {Text = "validText", Id = new Guid("67c77bb1-1c3c-4776-a9bd-b1707ea304c4")};
            }
        }

        private readonly Item[] _items =
        {
            new Item {Text = "item0", Id = Guid0},
            new Item {Text = "item1", Id = Guid1},
            new Item {Text = "item2", Id = Guid2}
        };

        private ItemsController _controller;
        private IAddItemService _addItemService;
        private IGetItemByIdService _getItemByIdService;
        private IItemRepository _repository;
        private ILocationHelper _helper;

        [SetUp]
        public void SetUp()
        {
            _addItemService = Substitute.For<IAddItemService>();
            _getItemByIdService = Substitute.For<IGetItemByIdService>();
            _repository = Substitute.For<IItemRepository>();
            _helper = Substitute.For<ILocationHelper>();

            _controller = new ItemsController(_addItemService, _getItemByIdService, _repository, _helper)
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [Test]
        public async Task GetAsync_ReturnsAllItems()
        {
            _repository.GetAllAsync().Returns(_items);

            var (contentResult, items) = await GetResultFromAction<Item[]>(controller => controller.GetAsync());

            Assert.That(contentResult.Content, Is.Not.Null);
            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(items, Is.EqualTo(_items).AsCollection.UsingItemEqualityComparer());
        }

        [Test]
        public async Task GetAsync_ExistingId_ReturnsItemWithSameId()
        {
            _getItemByIdService.GetItemByIdAsync(Guid0).Returns(new RetrievedItem {WasFound = true, Item = _items[0]});

            var (contentResult, item) = await GetResultFromAction<Item>(controller => controller.GetAsync(Guid0));

            Assert.That(contentResult.Content, Is.Not.Null);
            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item, Is.EqualTo(_items[0]).UsingItemEqualityComparer());
        }

        [Test]
        public async Task GetAsync_EmptyId_ReturnsBadRequest()
        {
            _controller.ModelState.Clear();

            var response = await GetResultFromAction(controller => controller.GetAsync(Guid.Empty));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task GetAsync_NonExistentId_ReturnsNotFound()
        {
            _getItemByIdService.GetItemByIdAsync(Guid0).Returns(new RetrievedItem {WasFound = false});

            var (contentResult, item) = await GetResultFromAction<Item>(controller => controller.GetAsync(Guid0));

            Assert.That(contentResult, Is.Not.Null);
            Assert.That(item, Is.EqualTo(null));
            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task PostAsync_NewItem_SetsLocationHeaderReturnsItemToPost()
        {
            _addItemService.AddItemAsync(Arg.Is<Item>(value => AreTextsEqual(value))).Returns(ItemToPost);
            _helper.GetUriLocation(ItemToPost.Id).Returns(Uri);

            var (createdResult, item) =
                await GetResultFromAction<Item>(controller => controller.PostAsync(new Item { Text = "itemToPost" }));
            var location = createdResult.Headers.Location.ToString();

            Assert.That(createdResult.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(item, Is.EqualTo(ItemToPost).UsingItemEqualityComparer());
            Assert.That(location, Is.EqualTo(UriString));
        }

        [TestCaseSource(nameof(InvalidItems))]
        public async Task PostAsync_InvalidItem_ReturnsBadRequest(Item item)
        {
            _controller.ModelState.Clear();

            var response = await GetResultFromAction(controller => controller.PostAsync(item));

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task PutAsync_ItemToUpdateWithExistingId_ReturnsUpdatedItem()
        {
            _repository.UpdateAsync(Arg.Is<Item>(value
                    => AreIdsEqual(value)))
                .Returns(_items[1]);

            var (contentResult, item) =
                await GetResultFromAction<Item>(controller => controller.PutAsync(_items[1].Id, _items[1]));

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item, Is.EqualTo(_items[1]).UsingItemEqualityComparer());
        }

        [Test]
        public async Task DeleteAsync_ExistingId_ReturnsNoContent()
        {
            var response = await GetResultFromAction(controller => controller.DeleteAsync(Guid0));

            await _repository.Received(1).DeleteAsync(Guid0);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        private async Task<(HttpResponseMessage message, TPayload item)> GetResultFromAction<TPayload>(
            Func<ItemsController, Task<IHttpActionResult>> actionSelector)
        {
            var responseMessage = await GetResultFromAction(actionSelector);
            responseMessage.TryGetContentValue(out TPayload item);
            return (responseMessage, item);
        }

        private async Task<HttpResponseMessage> GetResultFromAction(
            Func<ItemsController, Task<IHttpActionResult>> actionSelector)
        {
            var actionResult = await actionSelector(_controller);
            var responseMessage = await actionResult.ExecuteAsync(CancellationToken.None);
            return responseMessage;
        }

        private bool AreIdsEqual(Item value)
            => value.Id == _items[1].Id;

        private static bool AreTextsEqual(Item value)
            => value.Text == ItemToPost.Text;
    }
}