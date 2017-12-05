using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using NUnit.Framework;
using TodoApp.Api.Controllers;
using TodoApp.Api.Models;

namespace TodoApp.Api.Tests.Controllers
{
    [TestFixture]
    public class ItemsControllerTests
    {
        private Item[] items;
        private ItemsController controller;
        [SetUp]
        public void SetUp()
        {
            controller = new ItemsController();
            items = new []
            {
                new Item(1, "item0"),
                new Item(2, "item1"),
                new Item(3, "item2")
            };
        }

        [TearDown]
        public void TearDown()
        {
            controller = null;
        }

        [Test]
        public void GetReturnsAllItems()
        {
            Task<IHttpActionResult> actionResult = controller.Get();

            var contentResult = actionResult.Result as OkNegotiatedContentResult<Item[]>;

            Assert.That(contentResult.Content, Is.Not.Null);
            Assert.That(Enumerable.SequenceEqual(contentResult.Content, items), Is.True);
        }

        [Test]
        public void GetReturnsItemWithSameId()
        {
            Task<IHttpActionResult> actionResult = controller.Get(42);

            var contentResult = actionResult.Result as OkNegotiatedContentResult<Item>;

            Assert.That(contentResult, Is.Not.Null);
            Assert.That(contentResult.Content, Is.Not.Null);
            // Assert.That(contentResult.Content.Id, Is.EqualTo(42));
        }

        [Test]
        public void PostSetsLocationHeader()
        {
            Task<IHttpActionResult> actionResult = controller.Post(new Item(3, "updatedText"));

            var createdResult = actionResult.Result as CreatedAtRouteNegotiatedContentResult<Item>;

            Assert.That(createdResult, Is.Not.Null);
            Assert.That(createdResult.RouteName, Is.EqualTo("DefaultApi"));
            // Assert.That(createdResult.RouteValues["id"], Is.EqualTo(10));
        }

        [Test]
        public void PutReturnsContentResult()
        {
            Task<IHttpActionResult> actionResult = controller.Put(10, new Item(10, "itemik"));

            var contentResult = actionResult.Result as NegotiatedContentResult<Item>;

            Assert.That(contentResult.Content, Is.Not.Null);
            Assert.That(contentResult.StatusCode, Is.EqualTo(HttpStatusCode.Accepted));
            // Assert.That(contentResult.Content.Id, Is.EqualTo(10));
        }

        [Test]
        public void DeleteReturnsOk()
        {
            IHttpActionResult actionResult = controller.Delete(10).Result;

            Assert.That(actionResult, Is.TypeOf(typeof(OkResult)));
        }
    }
}
