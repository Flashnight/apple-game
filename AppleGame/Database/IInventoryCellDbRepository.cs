using AppleGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleGame
{
    /// <summary>
    /// Hides DB operations for the inventory cells.
    /// </summary>
    public interface IInventoryCellDbRepository
    {
        /// <summary>
        /// Updates inventory cell's data in db.
        /// </summary>
        /// <param name="cell">Inventory cell's data model.</param>
        void UpdateCell(InventoryCell cell);
    }
}
