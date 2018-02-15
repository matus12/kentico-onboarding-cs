using System;
using System.Threading.Tasks;
using NSubstitute;
using TodoApp.Contracts.Services;
using NUnit.Framework;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Services.Services;

namespace TodoApp.Services.Tests.Services
{
    [TestFixture]
    internal class GetItemByIdServiceTests
    {
        private IGetItemByIdService _getItemByIdService;
        private IItemRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IItemRepository>();

            _getItemByIdService = new GetItemByIdService(_repository);
        }

        [Test]
        public async Task GetItemByIdAsync_ExistingId_ReturnsCorrectItem()
        {
            var time =
                new DateTime(year: 2018, month: 5, day: 4, hour: 3, minute: 2, second: 1);
            var id = new Guid("44e506f7-c42b-42e5-823c-a5f1cf2e16f9");
            var item = new Item
            {
                Id = id,
                Text = "testText",
                CreatedAt = time,
                ModifiedAt = time
            };
            var expectedItem = new RetrievedEntity<Item>
            {
                Entity = item
            };
            _repository.GetByIdAsync(id).Returns(item);

            var testItem = await _getItemByIdService.GetItemByIdAsync(id);

            Assert.That(testItem.Entity, Is.EqualTo(expectedItem.Entity));
        }

        [Test]
        public async Task GetItemByIdAsync_ExistingId_ReturnsRetrievedItemWithSuccess()
        {
            var time =
                new DateTime(year: 2018, month: 5, day: 4, hour: 3, minute: 2, second: 1);
            var id = new Guid("44e506f7-c42b-42e5-823c-a5f1cf2e16f9");
            var item = new Item
            {
                Id = id,
                Text = "testText",
                CreatedAt = time,
                ModifiedAt = time
            };
            _repository.GetByIdAsync(id).Returns(item);

            var testItem = await _getItemByIdService.GetItemByIdAsync(id);

            Assert.That(testItem.WasFound, Is.True);
        }

        [Test]
        public async Task GetItemByIdAsync_NonExistingId_ReturnsItemWithFalseStatus()
        {
            var id = new Guid("44e506f7-c42b-42e5-823c-a5f1cf2e16f9");
            _repository.GetByIdAsync(id).Returns(null as Item);

            var testItem = await _getItemByIdService.GetItemByIdAsync(id);

            Assert.That(testItem.WasFound, Is.False);
        }
    }
}