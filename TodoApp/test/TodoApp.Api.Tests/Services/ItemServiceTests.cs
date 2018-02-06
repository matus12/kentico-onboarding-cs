using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TodoApp.Contracts;
using TodoApp.Contracts.Services;
using NSubstitute;
using TodoApp.Api.Tests.Comparers;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Services.Factories;
using TodoApp.Services;

namespace TodoApp.Api.Tests.Services
{
    [TestFixture]
    internal class ItemServiceTests
    {
        private IItemService _service;
        private IDateTimeService _dateTimeService;
        private IGuidService _guidService;
        private IItemRepository _repository;
        private DateTime _currentTime;
        private Guid _guid;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IItemRepository>();
            _dateTimeService = Substitute.For<IDateTimeService>();
            _guidService = Substitute.For<IGuidService>();

            _service = new ItemService(_repository, _dateTimeService, _guidService);

            _currentTime = DateTime.Now;
            _guid = new Guid("6548b7f6-d35c-4075-90d5-3a17e101f2c4");
        }

        [Test]
        public async Task AddItemAsync_NewItem_ReturnsSavedItem()
        {
            const string newItemText = "new item";
            _dateTimeService.GetCurrentDateTime().Returns(_currentTime);
            _guidService.GenerateGuid().Returns(_guid);
            var expectedItem = new Item
            {
                Id = _guid,
                Text = newItemText,
                CreateTime = _currentTime
            };
            _repository.AddAsync(expectedItem).Returns(expectedItem);
            var newItem = new Item {Text = newItemText};
            _repository.AddAsync(newItem).Returns(newItem);

            var testItem = await _service.AddItemAsync(newItem);

            Assert.That(testItem, Is.EqualTo(expectedItem).UsingItemEqualityComparer());
        }
    }
}