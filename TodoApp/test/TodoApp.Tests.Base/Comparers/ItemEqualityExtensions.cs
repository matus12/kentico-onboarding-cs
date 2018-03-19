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

                return AreItemsEqual(x, y);
            }

            public int GetHashCode(Item obj)
            {
                return obj.Text.GetHashCode() ^ obj.Id.GetHashCode();
            }

            private static bool AreItemsEqual(Item item1, Item item2)
                => item1.Text == item2.Text &&
                   item1.Id == item2.Id &&
                   item1.CreatedAt == item2.CreatedAt &&
                   item1.ModifiedAt == item2.ModifiedAt;
        }

        public static EqualConstraint UsingItemEqualityComparer(this EqualConstraint constraint)
            => constraint.Using(LazyItemEqualityComparer.Value);

        public static bool CompareWith(this Item item1, Item item2)
            => LazyItemEqualityComparer.Value.Equals(item1, item2);
    }
}