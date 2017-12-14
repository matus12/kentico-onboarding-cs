using System;

namespace TodoApp.Api.Models
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