using System;
using TodoApp.Contracts.Services.Factories;

namespace TodoApp.Services.Factories
{
    internal class TimeService : ITimeService
    {
        public DateTime GetCurrentDateTime() => DateTime.Now;
    }
}
