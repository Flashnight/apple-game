using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleGame.Models
{
    /// <summary>
    /// Model of the inventory
    /// </summary>
    public class Inventory
    {
        /// <summary>
        /// Height of the inventory.
        /// </summary>
        public int Height => 3;

        /// <summary>
        /// Width of the inventory.
        /// </summary>
        public int Width => 3;

        /// <summary>
        /// Items that storaged in the inventory.
        /// </summary>
        public InventoryCell [][] Cells { get; set;}
    }
}
