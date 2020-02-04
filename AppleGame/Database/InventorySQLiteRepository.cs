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
    /// Hides DB operations for the inventory.
    /// </summary>
    public class InventorySQLiteRepository : IInventoryDbRepository
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
        /// Hides DB operations for the inventory.
        /// </summary>
        public InventorySQLiteRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            _sqliteFactory = (SQLiteFactory)DbProviderFactories.GetFactory(_connectionString.ProviderName);
        }

        /// <summary>
        /// Saves inventory in the database.
        /// </summary>
        /// <returns>Inventory's id in the db.</returns>
        public int CreateNewInventory()
        {
            using (SQLiteConnection connection = (SQLiteConnection)_sqliteFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString.ConnectionString;
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = $@"INSERT INTO Inventory (Height, Widht)
                    VALUES (3, 3);";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT MAX(Id) FROM Inventory;";
                    int id = Convert.ToInt32(command.ExecuteScalar());

                    return id;
                }
            }
        }
    }
}
