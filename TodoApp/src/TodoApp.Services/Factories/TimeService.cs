using System;
using TodoApp.Contracts.Factories;

namespace TodoApp.Services.Factories
{
    internal class TimeService : ITimeService
    {
        public DateTime GetCurrentDateTime() => DateTime.Now;
    }
}
