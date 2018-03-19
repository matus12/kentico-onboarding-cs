using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TodoApp.Contracts.Factories;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Repositories;
using TodoApp.Contracts.Services;
using TodoApp.Services.Services;
using TodoApp.Tests.Base.Comparers;
// ReSharper disable ArgumentsStyleLiteral

namespace TodoApp.Services.Tests.Services
{
    [TestFixture]
    internal class UpdateItemServiceTests
    {
        private IUpdateItemService _updateItemService;
        private ITimeFactory _timeFactory;
        private IItemRepository _repository;
        private IGetItemByIdService _getItemByIdService;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IItemRepository>();
            _timeFactory = Substitute.For<ITimeFactory>();
            _getItemByIdService = Substitute.For<IGetItemByIdService>();

            _updateItemService = new UpdateItemService(_repository, _timeFactory, _getItemByIdService);
        }

        [Test]
        public async Task UpdateItem_ItemToUpdate_ReturnsUpdatedItem()
        {
            const string originalText = "originalText";
            const string updatedText = "updatedText";
            var creationTime =
                new DateTime(year: 2012, month: 2, day: 5, hour: 13, minute: 0, second: 14);
            var currentTime =
                new DateTime(year: 2018, month: 2, day: 5, hour: 13, minute: 0, second: 14);
            _timeFactory.GetCurrentDateTime().Returns(currentTime);
            var guid = new Guid("6548b7f6-d35c-4075-90d5-3a17e101f2c4");
            var itemBeforeUpdate = new Item
            {
                Id = guid,
                Text = originalText,
                CreatedAt = creationTime,
                ModifiedAt = creationTime
            };
            var itemToUpdate = new Item
            {
                Id = guid,
                Text = updatedText,
                CreatedAt = creationTime
            };
            var expectedItem = new RetrievedEntity<Item>
            {
                Entity = new Item
                {
                    Id = guid,
                    Text = updatedText,
                    CreatedAt = creationTime,
                    ModifiedAt = currentTime
                }
            };
            _getItemByIdService.GetItemByIdAsync(guid)
                .Returns(new RetrievedEntity<Item> {Entity = itemBeforeUpdate});
            _repository.UpdateAsync(Arg.Is<Item>(value
                => value.ItemIdentifierEqualityComparer(itemToUpdate)
            )).Returns(new Item
            {
                Id = guid,
                Text = updatedText,
                CreatedAt = creationTime
            });
            
            var testItem = await _updateItemService.UpdateItemAsync(itemToUpdate);

            Assert.That(testItem.Entity, Is.EqualTo(expectedItem.Entity).UsingItemEqualityComparer());
        }
    }
}