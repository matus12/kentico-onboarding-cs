using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TodoApp.Api.Models
{
    [Serializable]
    public class Item
    {
        public Item(string text, Guid id)
        {
            Text = text;
            Id = id;
        }

        public Guid Id;
        public string Text;
    }
}