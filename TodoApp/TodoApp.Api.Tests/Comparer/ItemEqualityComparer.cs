using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TodoApp.Api.Models;

namespace TodoApp.Api.Tests.Comparer
{
    internal static class ItemEqualityComparerWrapper
    {
        private static Lazy<ItemEqualityComparer> Lazy => new Lazy<ItemEqualityComparer>();

        private static ItemEqualityComparer Comparer => Lazy.Value;

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
                return x.Text == y.Text && x.Id == y.Id;
            }

            public int GetHashCode(Item obj)
            {
                return obj.Text.GetHashCode() ^ obj.Id.GetHashCode();
            }
        }

        public static EqualConstraint UsingItemEqualityComparer(this EqualConstraint constraint)
            => constraint.Using(Comparer);
    }
}