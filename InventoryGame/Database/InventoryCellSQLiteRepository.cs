using InventoryGame.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace InventoryGame.Database
{
    /// <summary>
    /// Hides DB operations for the inventory cells.
    /// </summary>
    public class InventoryCellSQLiteRepository : IInventoryCellDbRepository
    {
        /// <summary>
        /// Contains connections string's data.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Hides DB operations for the inventory cells.
        /// </summary>
        public InventoryCellSQLiteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Updates inventory cell's data in db.
        /// </summary>
        /// <param name="cell">Inventory cell's data model.</param>
        public async Task UpdateCellAsync(InventoryCell cell)
        {
            await using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                string itemId = cell.Item?.Id.ToString() ?? "NULL";

                await using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = $@"UPDATE InventoryCell
                    SET Amount = {cell.Amount}, ItemId = {itemId}
                    WHERE Id = {cell.Id};";

                    command.CommandType = CommandType.Text;
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
