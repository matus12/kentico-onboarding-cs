using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using TodoApp.Contracts.Services;
using TodoApp.Services;

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
        public void GenerateGuid_ReturnsValidGuid()
        {
            const string guidPattern = @"^[{(]?[0-9A-F]{8}[-]?([0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$";
            var regex = new Regex(guidPattern, RegexOptions.IgnoreCase);

            var generatedGuid = _guidService.GenerateGuid();
            Console.WriteLine(generatedGuid.ToString());
            
            Assert.That(regex.Match(generatedGuid.ToString()).Success, Is.True);
        }

        [Test]
        public void GenerateGuid_ReturnsUniqueGuid()
        {
            var generatedGuid0 = _guidService.GenerateGuid();
            var generatedGuid1 = _guidService.GenerateGuid();

            Assert.That(generatedGuid0, Is.Not.EqualTo(generatedGuid1));
        }
    }
}
