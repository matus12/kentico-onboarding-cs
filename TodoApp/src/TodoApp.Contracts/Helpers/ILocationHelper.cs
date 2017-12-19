using System;

namespace TodoApp.Contracts.Helpers
{
    public interface ILocationHelper
    {
        Uri GetUriLocation(Guid id);
    }
}
