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
                if (!_wasFound)
                {
                    throw new InvalidOperationException();
                }

                return _item;
            }
            set => _item = value;
        }

        private bool _wasFound;

        public bool WasFound
        {
            set => _wasFound = value;
        }
    }
}
