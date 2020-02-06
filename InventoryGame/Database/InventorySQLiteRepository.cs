using InventoryGame.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryGame.Database
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
        /// <returns>Inventory's data from the db.</returns>
        public Inventory CreateNewInventory()
        {
            using (SQLiteConnection connection = (SQLiteConnection)_sqliteFactory.CreateConnection())
            {
                int inventoryHeight = 3;
                int inventoryWidth = 3;

                connection.ConnectionString = _connectionString.ConnectionString;
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    command.CommandText = $@"INSERT INTO Inventory (Height, Widht)
                    VALUES ({inventoryHeight}, {inventoryWidth});";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT MAX(Id) FROM Inventory;";
                    int id = Convert.ToInt32(command.ExecuteScalar());

                    Inventory inventory = new Inventory
                    {
                        Id = id,
                        Height = inventoryHeight,
                        Width = inventoryWidth
                    };

                    List<InventoryCell> cells = new List<InventoryCell>();

                    for (int i = 0; i < inventoryHeight; i++)
                        for (int j = 0; j < inventoryWidth; j++)
                        {
                            command.CommandText = $@"INSERT INTO InventoryCell (Row, Column, Amount, InventoryId)
                            VALUES ({i}, {j}, 0, {id});";
                            command.CommandType = CommandType.Text;
                            command.ExecuteNonQuery();
                        }

                    

                    command.CommandText = $@"SELECT Id, Row, Column FROM InventoryCell
                    WHERE InventoryId = {id}
                    ORDER BY ID;";
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        for (int i = 0; i < inventoryHeight; i++)
                            for (int j = 0; j < inventoryWidth; j++)
                            {
                                reader.Read();

                                InventoryCell inventoryCell = new InventoryCell
                                {
                                    Id = reader.GetInt32(0),
                                    Row = reader.GetInt32(1),
                                    Column = reader.GetInt32(2),
                                    Amount = 0,
                                    InventoryId = id
                                };

                                cells.Add(inventoryCell);
                            }
                    }

                    inventory.Cells = cells;

                    transaction.Commit();

                    return inventory;
                }
            }
        }
    }
}
