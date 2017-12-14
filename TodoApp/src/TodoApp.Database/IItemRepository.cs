using System;
using System.Collections.Generic;
using TodoApp.Database.Models;

namespace TodoApp.Database
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAll();
        Item GetById(Guid id);
        Item Add(Item item);
        Item Update(Item item);
        void Delete(Guid id);
    }
}
