using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using TodoApp.Contracts.Services.Factories;
using TodoApp.Services.Services.Helpers;

namespace TodoApp.Services.Tests.Services.Helpers
{
    internal class TimeServiceTests
    {
        private ITimeService _timeService;

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
            _timeService = new TimeService();    
        }

        [TestCaseSource(nameof(MaxMinDateTimes))]
        public void GetCurrentDateTime_ReturnsCorrectDateTime(DateTime testTime)
        {
            var time = _timeService.GetCurrentDateTime();

            Assert.That(time.Ticks, Is.Not.EqualTo(testTime).Within(50000));
        }

        [Test]
        public void GetCurrentDateTime_ReturnsDifferentTimeWhenCalledTwice()
        {
            var time0 = _timeService.GetCurrentDateTime();
            Thread.Sleep(50);
            var time1 = _timeService.GetCurrentDateTime();

            Assert.That(time1.Ticks, Is.EqualTo(time0.Ticks).Within(600000));
        }
    }
}
