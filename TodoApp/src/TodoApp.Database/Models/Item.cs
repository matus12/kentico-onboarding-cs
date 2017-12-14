using System;

namespace TodoApp.Database.Models
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
