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
    public interface IItem
    {
        /// <summary>
        /// Type of item.
        /// </summary>
        ItemType ItemType { get; set; }

        /// <summary>
        /// Link to image of item in the file system.
        /// </summary>
        string ImageSource { get; set; }
    }
}
