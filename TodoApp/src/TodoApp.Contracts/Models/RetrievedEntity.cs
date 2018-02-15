using System;

namespace TodoApp.Contracts.Models
{
    public class RetrievedEntity<TEntity>
    {
        private const string NullItem =
            "Cannot retrieve entity that was not found. " +
            "Check WasFound property to ensure that entity was found";
        private TEntity _entity;

        public TEntity Entity
        {
            get => WasFound
                ? _entity
                : throw new InvalidOperationException(NullItem);

            set => _entity = value;
        }

        public static readonly RetrievedEntity<TEntity> NotFound = new RetrievedEntity<TEntity>();
        
        public bool WasFound => _entity != null;
    }
}