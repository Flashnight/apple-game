using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryGame.Models
{
    /// <summary>
    /// Model of the inventory.
    /// </summary>
    public class Inventory : IModel
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Height of the inventory.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Width of the inventory.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Cells in the inventory
        /// </summary>
        public List<InventoryCell> Cells { get; set; }
    }
}
