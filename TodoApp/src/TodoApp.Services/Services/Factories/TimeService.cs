using System;
using TodoApp.Contracts.Services.Factories;

namespace TodoApp.Services.Services.Factories
{
    internal class TimeService : ITimeService
    {
        public DateTime GetCurrentDateTime() => DateTime.Now;
    }
}
