using System;

namespace TodoApp.DAL.Models
{
    public class Item
    {
        public Guid Id;
        public string Text;

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Text)}: {Text}";
        }
    }
}
