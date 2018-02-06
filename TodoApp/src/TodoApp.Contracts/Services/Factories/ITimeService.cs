using System;

namespace TodoApp.Contracts.Services.Factories
{
    public interface ITimeService
    {
        DateTime GetCurrentDateTime();
    }
}
