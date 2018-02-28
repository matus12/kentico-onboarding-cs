using System;

namespace TodoApp.Contracts.Factories
{
    public interface IGuidService
    {
        Guid GenerateGuid();
    }
}
