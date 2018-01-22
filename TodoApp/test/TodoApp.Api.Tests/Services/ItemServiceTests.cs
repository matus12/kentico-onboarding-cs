using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TodoApp.Contracts;
using TodoApp.Contracts.Services;
using NSubstitute;
using TodoApp.Api.Tests.Comparers;
using TodoApp.Contracts.Models;
using TodoApp.Services;

namespace TodoApp.Api.Tests.Services
{
    [TestFixture]
    internal class ItemServiceTests
    {
        private IItemService _service;
        private IDateTimeService _dateTimeService;
        private IItemRepository _repository;
        private DateTime _currentTime;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IItemRepository>();
            _dateTimeService = Substitute.For<IDateTimeService>();

            _service = new ItemService(_repository, _dateTimeService);

            _currentTime = DateTime.Now;
        }

        [Test]
        public async Task AddItemAsync_NewItem_ReturnsSavedItem()
        {
            const string newItemText = "new item";
            _dateTimeService.GetCurrentDateTime().Returns(_currentTime);
            var expectedItem = new Item {Text = newItemText, CreateTime = _currentTime};
            _repository.AddAsync(expectedItem).Returns(expectedItem);
            var newItem = new Item {Text = newItemText};
            _repository.AddAsync(newItem).Returns(newItem);

            var testItem = await _service.AddItemAsync(newItem);

            Assert.That(testItem, Is.EqualTo(expectedItem).UsingItemEqualityComparer());
        }

        [Test]
        public async Task UpdateItemAsync_ItemBeingUpdated_ReturnsUpdatedItem()
        {
            const string updatedItemText = "updated text";
            _dateTimeService.GetCurrentDateTime().Returns(_currentTime);
            var expectedItem = new Item {Text = updatedItemText, UpdateTime = _currentTime};
            _repository.UpdateAsync(expectedItem).Returns(expectedItem);
            var itemToUpdate = new Item {Text = updatedItemText };
            _repository.UpdateAsync(itemToUpdate).Returns(itemToUpdate);

            var testItem = await _service.UpdateItemAsync(itemToUpdate);

            Assert.That(testItem, Is.EqualTo(expectedItem).UsingItemEqualityComparer());
        }
    }
}