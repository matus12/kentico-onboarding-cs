using System;
using TodoApp.Contracts.Services.Factories;

namespace TodoApp.Services.Factories
{
    internal class DateTimeService : IDateTimeService
    {
        public DateTime GetCurrentDateTime() => DateTime.Now;
    }
}
