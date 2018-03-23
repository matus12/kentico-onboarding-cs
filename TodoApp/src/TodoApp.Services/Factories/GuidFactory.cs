using System;
using TodoApp.Contracts.Factories;

namespace TodoApp.Services.Factories
{
    internal class GuidFactory : IGuidFactory
    {
        public Guid GenerateGuid() => Guid.NewGuid();
    }
}
