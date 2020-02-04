using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleGame.Database
{
    /// <summary>
    /// Hides DB operations for the inventory.
    /// </summary>
    public interface IInventoryDbRepository
    {
        /// <summary>
        /// Saves inventory in the database.
        /// </summary>
        /// <returns>Inventory's id in the db.</returns>
        int CreateNewInventory();
    }
}
