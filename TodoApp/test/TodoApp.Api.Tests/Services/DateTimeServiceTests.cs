using System;
using NUnit.Framework;
using TodoApp.Contracts.Services;
using TodoApp.Services;

namespace TodoApp.Api.Tests.Services
{
    internal class DateTimeServiceTests
    {
        private IDateTimeService _dateTimeService;

        [SetUp]
        public void SetUp()
        {
            _dateTimeService = new DateTimeService();    
        }

        [Test]
        public void GetCurrentDateTime_ReturnsCurrentDateAndTime()
        {
            var expectedDateTime = DateTime.Now;

            var time = _dateTimeService.GetCurrentDateTime();
            Console.WriteLine(time.Ticks - expectedDateTime.Ticks);

            Assert.That(time.Ticks - expectedDateTime.Ticks, Is.LessThan(50000));
        }
    }
}
