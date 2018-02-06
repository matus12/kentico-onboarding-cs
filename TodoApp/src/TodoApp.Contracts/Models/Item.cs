using System;

namespace TodoApp.Contracts.Models
{
    public class Item
    {
        public Guid Id;
        public string Text;
        public DateTime CreatedAt;
        public DateTime ModifiedAt;

        public override string ToString()
            => $"{nameof(Id)}: {Id}," +
               $" {nameof(Text)}: {Text}," +
               $" {nameof(CreatedAt)}: {CreatedAt}," +
               $" {nameof(ModifiedAt)}: {ModifiedAt}";
    }
}