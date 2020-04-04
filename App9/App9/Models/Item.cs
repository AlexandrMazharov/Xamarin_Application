using App9.Views;
using System;

namespace App9.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

        public static implicit operator Item(Prompt v)
        {
            throw new NotImplementedException();
        }
    }
}