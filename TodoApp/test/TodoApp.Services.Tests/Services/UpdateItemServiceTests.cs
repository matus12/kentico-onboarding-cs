using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using TodoApp.Contracts;
using TodoApp.Contracts.Models;
using TodoApp.Contracts.Services;
using TodoApp.Contracts.Services.Factories;
using TodoApp.Services.Services;
using TodoApp.Services.Tests.Comparers;

namespace TodoApp.Services.Tests.Services
{
    [TestFixture]
    internal class UpdateItemServiceTests
    {
        private IUpdateItemService _updateItemService;
        private ITimeService _timeService;
        private IItemRepository _repository;
        private IGetItemByIdService _getItemByIdService;

        [SetUp]
        public void SetUp()
        {
            _repository = Substitute.For<IItemRepository>();
            _timeService = Substitute.For<ITimeService>();
            _getItemByIdService = Substitute.For<IGetItemByIdService>();

            _updateItemService = new UpdateItemService(_repository, _timeService, _getItemByIdService);
        }

        [Test]
        public async Task UpdateItem_ItemToUpdate_ReturnsUpdatedItem()
        {
            const string originalText = "originalText";
            const string updatedText = "updatedText";
            var creationTime = new DateTime(2012, 2, 5, 13, 0, 14);
            var currentTime = new DateTime(2018, 2, 5, 13, 0, 14);
            _timeService.GetCurrentDateTime().Returns(currentTime);
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
            _repository.UpdateAsync(Arg.Any<Guid>(), Arg.Is<Item>(value
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

            var testItem = await _updateItemService.UpdateItemAsync(guid, itemToUpdate);

            Assert.That(testItem.Item, Is.EqualTo(expectedItem.Item).UsingItemEqualityComparer());
            Assert.That(testItem.WasFound, Is.EqualTo(expectedItem.WasFound));
        }
    }
}