using System;
using TodoApp.Contracts.Services.Factories;

namespace TodoApp.Services.Services.Factories
{
    internal class GuidService : IGuidService
    {
        public Guid GenerateGuid() => Guid.NewGuid();
    }
}
