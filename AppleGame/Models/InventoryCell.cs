using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleGame.Models
{
    /// <summary>
    /// Model of a cell in the inventory.
    /// </summary>
    public class InventoryCell
    {
        /// <summary>
        /// Model of item.
        /// </summary>
        public Item Item { get; set; }

        /// <summary>
        /// Amount of the item in the cell.
        /// </summary>
        public int Amount { get; set; }
    }
}
