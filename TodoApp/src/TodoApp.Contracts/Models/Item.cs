using System;

namespace TodoApp.Contracts.Models
{
    public class Item
    {
        public Guid Id;
        public string Text;

        public override string ToString()
            => $"{nameof(Id)}: {Id}, {nameof(Text)}: {Text}";
    }
}