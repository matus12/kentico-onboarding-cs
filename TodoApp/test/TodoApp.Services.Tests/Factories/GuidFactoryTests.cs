using System;
using System.Linq;
using NUnit.Framework;
using TodoApp.Contracts.Factories;
using TodoApp.Services.Factories;

namespace TodoApp.Services.Tests.Factories
{
    internal class GuidFactoryTests
    {
        private IGuidFactory _guidFactory;

        [SetUp]
        public void SetUp()
        {
            _guidFactory = new GuidFactory();
        }

        [Test]
        public void GenerateGuid_ReturnsNonEmptyGuid()
        {
            var generatedGuid = _guidFactory.GenerateGuid();
            
            Assert.That(generatedGuid, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void GenerateGuid_ReturnsUniqueGuid()
        {
            const int numberOfGeneratedGuids = 150;

            var generatedGuids = Enumerable
                .Repeat<Func<Guid>>(() => _guidFactory.GenerateGuid(), numberOfGeneratedGuids)
                .Select(oneGuidFactory => oneGuidFactory());
            var nonUniqueGuids = generatedGuids
                .GroupBy(guid => guid)
                .Where(group => group.Skip(1).Any())
                .Select(group => group.Key)
                .ToArray();

            Assert.That(nonUniqueGuids, Is.Empty, () => "Each of following GUIDs was present more than once");
        }
    }
}