using System;

namespace TodoApp.Contracts.Models
{
    public class RetrievedItem
    {
        private Item _item;
        public Item Item
        {
            get
            {
                if (!WasFound)
                {
                    throw new InvalidOperationException();
                }

                return _item;
            }
            set => _item = value;
        }

        public bool WasFound { get; set; }
    }
}
