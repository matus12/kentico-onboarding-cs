using System;

namespace TodoApp.Contracts.Models
{
    public class Item
    {
        public Guid Id;
        public string Text;
        public DateTime CreateTime;
        public DateTime UpDateTime;

        public override string ToString()
            => $"{nameof(Id)}: {Id}, {nameof(Text)}: {Text}";
    }
}