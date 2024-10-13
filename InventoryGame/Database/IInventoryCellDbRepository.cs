using InventoryGame.Models;
using System.Threading.Tasks;

namespace InventoryGame
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
        Task UpdateCellAsync(InventoryCell cell);
    }
}
