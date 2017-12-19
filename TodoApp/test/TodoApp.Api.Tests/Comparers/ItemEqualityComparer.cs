﻿using System;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TodoApp.Interfaces.Models;

namespace TodoApp.Api.Tests.Comparers
{
    internal static class ItemEqualityComparerWrapper
    {
        private static Lazy<ItemEqualityComparer> LazyItemEqualityComparer => new Lazy<ItemEqualityComparer>();

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
            => constraint.Using(LazyItemEqualityComparer.Value);
    }
}