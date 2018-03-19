using System;

namespace TodoApp.Contracts.Models
{
    public class RetrievedEntity<TEntity>
    {
        private readonly TEntity _entity;

        public TEntity Entity => WasFound
            ? _entity
            : throw new InvalidOperationException(
                $"Cannot retrieve entity that was not found. Check {nameof(WasFound)} property to ensure that entity was found");

        public static readonly RetrievedEntity<TEntity> NotFound =
            new RetrievedEntity<TEntity>(default(TEntity));

        public RetrievedEntity(TEntity entity)
            => _entity = entity;

        public bool WasFound => _entity != null;
    }
}