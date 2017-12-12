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
using TodoApp.Api.Models;
using TodoApp.Api.Tests.Comparers;

namespace TodoApp.Api.Tests.Controllers
{
    [TestFixture]
    internal class ItemsControllerTests
    {
        private ItemsController _controller;
        private static readonly Guid Guid1 = new Guid("e6eb4638-38a4-49ac-8aaf-878684397702");
        private static readonly Guid Guid2 = new Guid("a5d4b549-bdd3-4ec2-8210-ff42926aa141");
        private static readonly Guid Guid3 = new Guid("45c4fb8b-1cdf-42ca-8a61-67fd7f781057");

        private static readonly Item ItemToPost =
            new Item {Text = "itemToPost", Id = new Guid("e6eb4638-38a4-49ac-8aaf-878684397707")};

        private readonly Item[] _items = IteratedItems.ToArray();

        private static IEnumerable<Item> IteratedItems
        {
            get
            {
                yield return new Item {Text = "item0", Id = Guid1};
                yield return new Item {Text = "item1", Id = Guid2};
                yield return new Item {Text = "item2", Id = Guid3};
            }
        }

        [SetUp]
        public void SetUp()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                RoutesConfig.ApiV1Route,
                "{id}/test-route/15"
            );
            _controller = new ItemsController
            {
                Request = new HttpRequestMessage(),
                Configuration = config
            };
        }

        [Test]
        public async Task GetAsync_ReturnsAllItems()
        {
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
            var actionResult = await _controller.GetAsync(Guid1);
            var contentResult = await actionResult.ExecuteAsync(CancellationToken.None);
            contentResult.TryGetContentValue(out Item item);

            Assert.That(contentResult.Content, Is.Not.Null);
            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item, Is.EqualTo(_items[0]).UsingItemEqualityComparer());
        }

        [Test]
        public async Task PostAsync_NewItem_SetsLocationHeader()
        {
            const string expectedUri = "/45c4fb8b-1cdf-42ca-8a61-67fd7f781057/test-route/15";

            var actionResult = await _controller.PostAsync(ItemToPost);
            var createdResult = await actionResult.ExecuteAsync(CancellationToken.None);
            createdResult.TryGetContentValue(out Item item);
            var location = createdResult.Headers.Location.ToString();

            Assert.That(createdResult.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(item, Is.EqualTo(ItemToPost).UsingItemEqualityComparer());
            Assert.That(location, Is.EqualTo(expectedUri));
        }

        [Test]
        public async Task PutAsync_ExistingId_UpdatedItem_ReturnsContentResult()
        {
            var actionResult = await _controller.PutAsync(_items[1].Id, _items[1]);
            var contentResult = await actionResult.ExecuteAsync(CancellationToken.None);
            contentResult.TryGetContentValue(out Item item);

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(item, Is.EqualTo(_items[1]).UsingItemEqualityComparer());
        }

        [Test]
        public async Task DeleteAsync_ExistingId_ReturnsNoContent()
        {
            var actionResult = await _controller.DeleteAsync(Guid1);
            var result = await actionResult.ExecuteAsync(CancellationToken.None);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }
    }
}