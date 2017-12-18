using System;

namespace TodoApp.Interfaces
{
    public interface ILocationHelper
    {
        Uri GetUriLocation(Guid id);
    }
}
