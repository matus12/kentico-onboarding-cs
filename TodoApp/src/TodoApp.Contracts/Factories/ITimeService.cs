using System;

namespace TodoApp.Contracts.Factories
{
    public interface ITimeService
    {
        DateTime GetCurrentDateTime();
    }
}
