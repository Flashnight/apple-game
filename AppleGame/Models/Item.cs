using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleGame.Models
{
    /// <summary>
    /// Model of the item in the inventory.
    /// </summary>
    public class Item : ICloneable, IItem
    {
        /// <summary>
        /// Type of item.
        /// </summary>
        public ItemType ItemType { get; set; }

        /// <summary>
        /// Link to image of item in the file system.
        /// </summary>
        public string ImageSource { get; set; }

        public object Clone()
        {
            return new Item
            {
                ItemType = ItemType,
                ImageSource = ImageSource
            };
        }
    }
}
