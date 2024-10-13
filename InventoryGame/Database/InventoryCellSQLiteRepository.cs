using InventoryGame.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

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
        public void UpdateCell(InventoryCell cell)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                string itemId = cell.Item?.Id.ToString() ?? "NULL";

                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = $@"UPDATE InventoryCell
                    SET Amount = {cell.Amount}, ItemId = {itemId}
                    WHERE Id = {cell.Id};";

                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
