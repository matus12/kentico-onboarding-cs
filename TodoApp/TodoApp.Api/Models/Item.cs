using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoApp.Api.Models
{
    public class Item
    {
        public Item(string text)
        {
            this.text = text;
        }
        public string text;
        public Boolean isEdited;
    }
}