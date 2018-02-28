using System;
using TodoApp.Contracts.Factories;

namespace TodoApp.Services.Factories
{
    internal class GuidService : IGuidService
    {
        public Guid GenerateGuid() => Guid.NewGuid();
    }
}
