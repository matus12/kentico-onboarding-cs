using System;

namespace TodoApp.Contracts.Models
{
    public class RetrievedEntity<TEntity> where TEntity : new()
    {
        private const string NullItem =
            "Cannot retrieve entity that was not found. " +
            "Check WasFound property to ensure that entity was found";
        private readonly TEntity _entity;

        public TEntity Entity => WasFound
            ? _entity
            : throw new InvalidOperationException(NullItem);

        public static readonly RetrievedEntity<TEntity> NotFound =
            new RetrievedEntity<TEntity>(new TEntity());

        public RetrievedEntity(TEntity entity)
        {
            _entity = entity;
        }

        public bool WasFound => _entity != null;
    }
}