using System;
using System.Collections.Generic;
using TodoApp.DAL.Models;

namespace TodoApp.DAL
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
