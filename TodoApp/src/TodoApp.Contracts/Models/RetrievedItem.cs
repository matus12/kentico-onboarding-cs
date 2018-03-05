using System;

namespace TodoApp.Contracts.Models
{
    public class RetrievedItem
    {
        private Item _item;

        public Item Item
        {
            get => WasFound
                ? _item
                : throw new InvalidOperationException();

            set => _item = value;
        }

        public bool WasFound { get; set; }
    }
}