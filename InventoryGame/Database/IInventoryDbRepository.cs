using InventoryGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryGame.Database
{
    /// <summary>
    /// Hides DB operations for the inventory.
    /// </summary>
    public interface IInventoryDbRepository
    {
        /// <summary>
        /// Saves inventory in the database.
        /// </summary>
        /// <returns>Inventory's data from db.</returns>
        Inventory CreateNewInventory();
    }
}
