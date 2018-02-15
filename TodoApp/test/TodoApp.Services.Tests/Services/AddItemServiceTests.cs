﻿using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TodoApp.Contracts.Factories;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;
using TodoApp.Services.Services;
using TodoApp.Tests.Base.Comparers;

namespace TodoApp.Services.Tests.Services
{
    [TestFixture]
    internal class AddItemServiceTests
    {
        private IAddItemService _service;
        private ITimeFactory _timeFactory;
        private IGuidFactory _guidFactory;
        private IItemRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IItemRepository>();
            _timeFactory = Substitute.For<ITimeFactory>();
            _guidFactory = Substitute.For<IGuidFactory>();

            _service = new AddItemService(_repository, _timeFactory, _guidFactory);
        }

        [Test]
        public async Task AddItemAsync_NewItem_ReturnsSavedItem()
        {
            const string newItemText = "new item";
            var currentTime =
                new DateTime(year: 2018, month: 2, day: 5, hour: 15, minute: 0, second: 24);
            var guid = new Guid("6548b7f6-d35c-4075-90d5-3a17e101f2c4");
            _timeFactory.GetCurrentDateTime().Returns(currentTime);
            _guidFactory.GenerateGuid().Returns(guid);
            var expectedItem = new Item
            {
                Id = guid,
                Text = newItemText,
                CreatedAt = currentTime,
                ModifiedAt = currentTime
            };
            var newItem = new Item {Text = newItemText};
            _repository.AddAsync(Arg.Is<Item>(value
                => value.ItemIdentifierEqualityComparer(expectedItem)
            )).Returns(expectedItem);

            var testItem = await _service.AddItemAsync(newItem);

            Assert.That(testItem, Is.EqualTo(expectedItem).UsingItemEqualityComparer());
        }
    }
}