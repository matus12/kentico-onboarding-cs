using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoApp.Api.Models
{
    public class Item : IEquatable<Item>
    {
        public Item(int id, string text)
        {
            Id = id;
            Text = text;
        }

        public int Id;
        public string Text;
        public Boolean IsEdited;
        public bool Equals(Item other)
        {
            if (other == null)
            {
                return false;
            }
            return Id.Equals(other.Id) &&
                   Text.Equals(other.Text) &&
                   IsEdited.Equals(other.IsEdited);
        }
    }
}