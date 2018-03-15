using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using TodoApp.Contracts.Factories;
using TodoApp.Services.Factories;

namespace TodoApp.Services.Tests.Factories
{
    internal class TimeFactoryTests
    {
        private ITimeFactory _timeFactory;

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
            _timeFactory = new TimeFactory();    
        }

        [TestCaseSource(nameof(MaxMinDateTimes))]
        public void GetCurrentDateTime_ReturnsCorrectDateTime(DateTime testTime)
        {
            var time = _timeFactory.GetCurrentDateTime();

            Assert.That(time.Ticks, Is.Not.EqualTo(testTime).Within(50000));
        }

        [Test]
        public void GetCurrentDateTime_ReturnsDifferentTimeWhenCalledTwice()
        {
            var time0 = _timeFactory.GetCurrentDateTime();
            Thread.Sleep(50);
            var time1 = _timeFactory.GetCurrentDateTime();

            Assert.That(time1.Ticks, Is.EqualTo(time0.Ticks).Within(600000));
        }
    }
}
