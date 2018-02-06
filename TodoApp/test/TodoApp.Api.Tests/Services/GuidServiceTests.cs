﻿using System;
using System.Linq;
using NUnit.Framework;
using TodoApp.Contracts.Services.Factories;
using TodoApp.Services.Services.Factories;

namespace TodoApp.Api.Tests.Services
{
    internal class GuidServiceTests
    {
        private IGuidService _guidService;

        [SetUp]
        public void SetUp()
        {
            _guidService = new GuidService();
        }
        [Test]
        public void GenerateGuid_ReturnsNonEmptyGuid()
        {
            var generatedGuid = _guidService.GenerateGuid();
            Console.WriteLine(generatedGuid.ToString());
            
            Assert.That(generatedGuid, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void GenerateGuid_ReturnsUniqueGuid()
        {
            const int numberOfGeneratedGuids = 150;

            var generatedGuids = Enumerable
                .Repeat<Func<Guid>>(() => _guidService.GenerateGuid(), numberOfGeneratedGuids)
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
