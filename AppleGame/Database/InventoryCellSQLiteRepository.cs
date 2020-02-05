using AppleGame.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleGame.Database
{
    /// <summary>
    /// Hides DB operations for the inventory cells.
    /// </summary>
    public class InventoryCellSQLiteRepository : IInventoryCellDbRepository
    {
        /// <summary>
        /// Contains connections string's data.
        /// </summary>
        private ConnectionStringSettings _connectionString;

        /// <summary>
        /// Creates SQLite connections.
        /// </summary>
        private SQLiteFactory _sqliteFactory;

        /// <summary>
        /// Hides DB operations for the inventory cells.
        /// </summary>
        public InventoryCellSQLiteRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            _sqliteFactory = (SQLiteFactory)DbProviderFactories.GetFactory(_connectionString.ProviderName);
        }

        /// <summary>
        /// Updates inventory cell's data in db.
        /// </summary>
        /// <param name="cell">Inventory cell's data model.</param>
        public void UpdateCell(InventoryCell cell)
        {
            using (SQLiteConnection connection = (SQLiteConnection)_sqliteFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString.ConnectionString;
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = $@"UPDATE InventoryCell
                    SET Amount = {cell.Amount}, ItemId = {cell.Item.Id}
                    WHERE Id = {cell.Id}";

                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
