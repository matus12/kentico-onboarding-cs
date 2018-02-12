using System;
using TodoApp.Contracts.Services.Factories;

namespace TodoApp.Services.Services.Helpers
{
    internal class TimeService : ITimeService
    {
        public DateTime GetCurrentDateTime() => DateTime.Now;
    }
}
