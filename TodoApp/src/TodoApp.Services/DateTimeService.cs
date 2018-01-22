using System;
using TodoApp.Contracts.Services;

namespace TodoApp.Services
{
    internal class DateTimeService : IDateTimeService
    {
        public DateTime GetCurrentDateTime() => DateTime.Now;
    }
}
