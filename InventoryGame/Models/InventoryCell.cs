using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryGame.Models
{
    /// <summary>
    /// Model of a cell in the inventory.
    /// </summary>
    public class InventoryCell : IModel
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public int Id { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        /// <summary>
        /// Model of item.
        /// </summary>
        public Item Item { get; set; }

        /// <summary>
        /// Amount of the item in the cell.
        /// </summary>
        public int Amount { get; set; }

        public int InventoryId { get; set; }
    }
}
