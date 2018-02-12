using System;
using TodoApp.Contracts.Services.Factories;

namespace TodoApp.Services.Services.Helpers
{
    internal class GuidService : IGuidService
    {
        public Guid GenerateGuid() => Guid.NewGuid();
    }
}
