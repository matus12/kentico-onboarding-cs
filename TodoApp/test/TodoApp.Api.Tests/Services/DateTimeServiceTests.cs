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

            Assert.That(time.Ticks - expectedDateTime.Ticks, Is.LessThan(50000));
        }

        [Test]
        public void GetCurrentDateTime_ReturnsDifferentTimeWhenCalledTwice()
        {
            var time0 = _dateTimeService.GetCurrentDateTime();
            var time1 = _dateTimeService.GetCurrentDateTime();

            Assert.That(time1, Is.GreaterThan(time0));
        }
    }
}
