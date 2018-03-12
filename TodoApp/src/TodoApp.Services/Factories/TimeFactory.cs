using System;
using TodoApp.Contracts.Factories;

namespace TodoApp.Services.Factories
{
    internal class TimeFactory : ITimeFactory
    {
        public DateTime GetCurrentDateTime() => DateTime.Now;
    }
}
