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
            var creationTime = new DateTime(2012, 2, 5, 13, 0, 14);
            var currentTime = new DateTime(2018, 2, 5, 13, 0, 14);
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
            var expectedItem = new RetrievedItem
            {
                WasFound = true,
                Item = new Item
                {
                    Id = guid,
                    Text = updatedText,
                    CreatedAt = creationTime,
                    ModifiedAt = currentTime
                }
            };
            _getItemByIdService.GetItemByIdAsync(Arg.Any<Guid>()).Returns(new RetrievedItem { WasFound = true, Item = itemBeforeUpdate });
            _repository.UpdateAsync(Arg.Is<Item>(value
                => value.Id == itemToUpdate.Id
                   && value.Text == itemToUpdate.Text
                   && value.CreatedAt == itemToUpdate.CreatedAt
                   && value.ModifiedAt == currentTime
            )).Returns(new Item
            {
                Id = guid,
                Text = updatedText,
                CreatedAt = creationTime,
                ModifiedAt = currentTime
            });

            var testItem = await _updateItemService.UpdateItemAsync(itemToUpdate);

            Assert.That(testItem.Item, Is.EqualTo(expectedItem.Item).UsingItemEqualityComparer());
            Assert.That(testItem.WasFound, Is.EqualTo(expectedItem.WasFound));
        }
    }
}