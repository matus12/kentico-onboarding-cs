using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Interfaces
{
    public interface ILocationHelper
    {
        Uri GetUriLocation(Guid id);
    }
}
