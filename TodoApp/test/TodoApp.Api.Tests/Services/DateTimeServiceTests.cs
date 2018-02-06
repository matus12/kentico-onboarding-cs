using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using TodoApp.Contracts.Services;
using TodoApp.Contracts.Services.Factories;
using TodoApp.Services;
using TodoApp.Services.Factories;

namespace TodoApp.Api.Tests.Services
{
    internal class DateTimeServiceTests
    {
        private IDateTimeService _dateTimeService;

        private static IEnumerable<DateTime> MaxMinDateTimes
        {
            get
            {
                yield return DateTime.MaxValue;
                yield return DateTime.MinValue;
            }
        }

        [SetUp]
        public void SetUp()
        {
            _dateTimeService = new DateTimeService();    
        }

        [TestCaseSource(nameof(MaxMinDateTimes))]
        public void GetCurrentDateTime_ReturnsCorrectDateTime(DateTime testTime)
        {
            var time = _dateTimeService.GetCurrentDateTime();

            Assert.That(time.Ticks, Is.Not.EqualTo(testTime).Within(50000));
        }

        [Test]
        public void GetCurrentDateTime_ReturnsDifferentTimeWhenCalledTwice()
        {
            var pause = new ManualResetEvent(false);

            var time0 = _dateTimeService.GetCurrentDateTime();
            pause.WaitOne(1000);
            var time1 = _dateTimeService.GetCurrentDateTime();

            Assert.That(time1, Is.GreaterThan(time0));
        }
    }
}
