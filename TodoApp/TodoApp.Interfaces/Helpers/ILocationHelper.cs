using System;

namespace TodoApp.Interfaces.Helpers
{
    public interface ILocationHelper
    {
        Uri GetUriLocation(Guid id);
    }
}
