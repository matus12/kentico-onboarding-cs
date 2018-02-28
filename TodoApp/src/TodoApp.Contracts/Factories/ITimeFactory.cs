using System;

namespace TodoApp.Contracts.Factories
{
    public interface ITimeFactory
    {
        DateTime GetCurrentDateTime();
    }
}
