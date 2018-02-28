using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TodoApp.Contracts.Models;

namespace TodoApp.Tests.Base.Comparers
{
    internal static class ItemEqualityExtensions
    {
        private static Lazy<ItemEqualityComparer> LazyItemEqualityComparer => new Lazy<ItemEqualityComparer>();

        // ReSharper disable once ClassNeverInstantiated.Local
        private sealed class ItemEqualityComparer : IEqualityComparer<Item>
        {
            public bool Equals(Item x, Item y)
            {
                if (x == null && y == null)
                {
                    return true;
                }
                if (x == null | y == null)
                {
                    return false;
                }
                return x.Text == y.Text &&
                       x.Id == y.Id &&
                       x.CreatedAt == y.CreatedAt &&
                       x.ModifiedAt == y.ModifiedAt;
            }

            public int GetHashCode(Item obj)
            {
                return obj.Text.GetHashCode() ^ obj.Id.GetHashCode();
            }
        }

        public static EqualConstraint UsingItemEqualityComparer(this EqualConstraint constraint)
            => constraint.Using(LazyItemEqualityComparer.Value);
    }
}