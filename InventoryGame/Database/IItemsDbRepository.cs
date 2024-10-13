using InventoryGame.Models;
using System.Threading.Tasks;

namespace InventoryGame.Database
{
    /// <summary>
    /// Hides DB operations for items.
    /// </summary>
    public interface IItemsDbRepository
    {
        /// <summary>
        /// Returns image's data by id.
        /// </summary>
        /// <param name="id">Identifier of item.</param>
        /// <returns>Model's data model.</returns>
        Task<Item> GetItemByIdAsync(int id);
    }
}
