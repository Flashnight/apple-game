using System.Threading.Tasks;

namespace InventoryGame.Database
{
    /// <summary>
    /// It initializes new database.
    /// </summary>
    public interface IDatabaseMaker
    {
        /// <summary>
        /// Initializes new database if it doesnt exist for SQLLite.
        /// </summary>
        Task CreateDatabaseAsync();
    }
}
