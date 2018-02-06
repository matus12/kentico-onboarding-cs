using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using TodoApp.Contracts.Services;
using TodoApp.Contracts.Services.Factories;
using TodoApp.Services;
using TodoApp.Services.Services.Factories;

namespace TodoApp.Api.Tests.Services
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
            var pause = new ManualResetEvent(false);

            var time0 = _timeService.GetCurrentDateTime();
            pause.WaitOne(1000);
            var time1 = _timeService.GetCurrentDateTime();

            Assert.That(time1, Is.GreaterThan(time0));
        }
    }
}
