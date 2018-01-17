using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using NUnit.Framework;
using TodoApp.Api.Controllers;
using TodoApp.Api.Tests.Comparers;
using NSubstitute;
using TodoApp.Contracts;
using TodoApp.Contracts.Helpers;
using TodoApp.Contracts.Models;

namespace TodoApp.Api.Tests.Controllers
{
    [TestFixture]
    internal class ItemsControllerTests
    {
        private ItemsController _controller;
        private IItemRepository _repository;
        private ILocationHelper _helper;
        private static readonly Guid Guid0 = new Guid("e6eb4638-38a4-49ac-8aaf-878684397702");
        private static readonly Guid Guid1 = new Guid("a5d4b549-bdd3-4ec2-8210-ff42926aa141");
        private static readonly Guid Guid2 = new Guid("45c4fb8b-1cdf-42ca-8a61-67fd7f781057");

        private static readonly Item ItemToPost =
            new Item {Text = "itemToPost", Id = new Guid("e6eb4638-38a4-49ac-8aaf-878684397707")};

        private const string UriString = "/e6eb4638-38a4-49ac-8aaf-878684397707/test-route/15";

        private static readonly Uri Uri = new Uri(UriString, UriKind.Relative);

        private readonly Item[] _items = IteratedItems.ToArray();

        private static IEnumerable<Item> IteratedItems
        {
            get
            {
                yield return new Item {Text = "item0", Id = Guid0};
                yield return new Item {Text = "item1", Id = Guid1};
                yield return new Item {Text = "item2", Id = Guid2};
            }
        }

        [SetUp]
        public void SetUp()
        {
            var config = new HttpConfiguration();

            _repository = Substitute.For<IItemRepository>();

            _helper = Substitute.For<ILocationHelper>();

            _controller = new ItemsController(_repository, _helper)
            {
                Request = new HttpRequestMessage(),
                Configuration = config
            };
        }

        [Test]
        public async Task GetAsync_ReturnsAllItems()
        {
            _repository.GetAllAsync().Returns(_items);

            var actionResult = await _controller.GetAsync();
            var contentResult = await actionResult.ExecuteAsync(CancellationToken.None);
            contentResult.TryGetContentValue(out Item[] value);

            Assert.That(contentResult.Content, Is.Not.Null);
            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(value, Is.EqualTo(_items).AsCollection.UsingItemEqualityComparer());
        }

        [Test]
        public async Task GetAsync_ExistingId_ReturnsItemWithSameId()
        {
            _repository.GetByIdAsync(Arg.Any<Guid>()).Returns(_items[1]);
            _repository.GetByIdAsync(Guid0).Returns(_items[0]);  

            var (contentResult, item) = await GetResultFromAction(await _controller.GetAsync(Guid0));

            Assert.That(contentResult.Content, Is.Not.Null);
            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item, Is.EqualTo(_items[0]).UsingItemEqualityComparer());
        }

        [Test]
        public async Task PostAsync_NewItem_SetsLocationHeaderReturnsItemToPost()
        {
            _repository.AddAsync(ItemToPost).ReturnsForAnyArgs(ItemToPost);
            _helper.GetUriLocation(Arg.Any<Guid>()).Returns(Uri);

            var (createdResult, item) = await GetResultFromAction(await _controller.PostAsync(ItemToPost));
            var location = createdResult.Headers.Location.ToString();

            Assert.That(createdResult.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(item, Is.EqualTo(ItemToPost).UsingItemEqualityComparer());
            Assert.That(location, Is.EqualTo(UriString));
        }

        [Test]
        public async Task PutAsync_ItemToUpdateWithExistingId_ReturnsUpdatedItem()
        {
            _repository.UpdateAsync(_items[0]).ReturnsForAnyArgs(_items[1]);
            var (contentResult, item) = await GetResultFromAction(await _controller.PutAsync(_items[1].Id, _items[1]));

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item, Is.EqualTo(_items[1]).UsingItemEqualityComparer());
        }

        [Test]
        public async Task DeleteAsync_ExistingId_ReturnsNoContent()
        {
            var actionResult = await _controller.DeleteAsync(Guid0);
            var result = await actionResult.ExecuteAsync(CancellationToken.None);

            await _repository.Received(1).DeleteAsync(Arg.Any<Guid>());
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        private static async Task<(HttpResponseMessage message, Item item)> GetResultFromAction(IHttpActionResult actionResult)
        {
            var responseMessage = await actionResult.ExecuteAsync(CancellationToken.None);
            Console.WriteLine(responseMessage.TryGetContentValue(out Item item));
            return (responseMessage, item);
        }
    }
}