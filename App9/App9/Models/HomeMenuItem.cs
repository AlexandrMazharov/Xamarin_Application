using System;
using System.Collections.Generic;
using System.Text;

namespace App9.Models
{
    public enum MenuItemType
    {
        Browse,
        About,        
        Map,
            Exit
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
