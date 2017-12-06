using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using NUnit.Framework;
using TodoApp.Api.Controllers;
using TodoApp.Api.Models;
using TodoApp.Api.Tests.Comparer;

namespace TodoApp.Api.Tests.Controllers
{
    [TestFixture]
    internal class ItemsControllerTests
    {
        private Item[] _items;
        private ItemsController _controller;
        private readonly Guid _guid1 = new Guid("e6eb4638-38a4-49ac-8aaf-878684397702");
        private readonly Guid _guid2 = new Guid("a5d4b549-bdd3-4ec2-8210-ff42926aa141");
        private readonly Guid _guid3 = new Guid("45c4fb8b-1cdf-42ca-8a61-67fd7f781057");

        [SetUp]
        public void SetUp()
        {
            HttpConfiguration _config = new HttpConfiguration();
            _config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional});
            _controller = new ItemsController
            {
                Request = new HttpRequestMessage(),
                Configuration = _config
            };

            _items = new[]
            {
                new Item("item0", _guid1),
                new Item("item1", _guid2),
                new Item("item2", _guid3)
            };
        }

        [TearDown]
        public void TearDown()
        {
            _controller = null;
        }

        [Test]
        public async Task GetReturnsAllItems()
        {
            var actionResult = await _controller.GetAsync();

            var contentResult = await actionResult.ExecuteAsync(CancellationToken.None);
            contentResult.TryGetContentValue(out Item[] value);

            Assert.That(contentResult.Content, Is.Not.Null);
            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(value, Is.EqualTo(_items).AsCollection.UsingItemEqualityComparer());
        }

        [Test]
        public async Task GetReturnsItemWithSameId()
        {
            var actionResult = await _controller.GetAsync(_guid1);

            var contentResult = await actionResult.ExecuteAsync(CancellationToken.None);
            contentResult.TryGetContentValue(out Item item);

            Assert.That(contentResult.Content, Is.Not.Null);
            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(item, Is.EqualTo(_items[0]).UsingItemEqualityComparer());
        }

        [Test]
        public async Task PostSetsLocationHeader()
        {
            var actionResult =
                await _controller.PostAsync(new Item("updatedText", new Guid("e6eb4638-38a4-49ac-8aaf-878684397705")));

            var createdResult = await actionResult.ExecuteAsync(CancellationToken.None);

            Assert.That(createdResult.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            //Assert.That(createdResult.Content.RouteName, Is.EqualTo("DefaultApi"));
            //Assert.That(createdResult.RouteValues["id"], Is.EqualTo(10));
        }

        [Test]
        public async Task PutReturnsContentResult()
        {
            var actionResult = await _controller.PutAsync(_items[1].Id, _items[1]);

            var contentResult = await actionResult.ExecuteAsync(CancellationToken.None);
            contentResult.TryGetContentValue(out Item item);

            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            Assert.That(item, Is.EqualTo(_items[1]).UsingItemEqualityComparer());
        }

        [Test]
        public async Task DeleteReturnsOk()
        {
            var actionResult = await _controller.DeleteAsync(new Guid("e6eb4638-38a4-49ac-8aaf-878684397702"));
            var result = await actionResult.ExecuteAsync(CancellationToken.None);

            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}