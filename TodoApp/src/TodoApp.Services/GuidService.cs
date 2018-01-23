using System;
using TodoApp.Contracts.Services;

namespace TodoApp.Services
{
    internal class GuidService : IGuidService
    {
        public Guid GenerateGuid() => Guid.NewGuid();
    }
}
