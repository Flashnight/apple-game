using InventoryGame.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

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
        private readonly string _connectionString;

        /// <summary>
        /// Hides DB operations for the inventory.
        /// </summary>
        public InventorySQLiteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Saves inventory in the database.
        /// </summary>
        /// <returns>Inventory's data from the db.</returns>
        public Inventory CreateNewInventory()
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                int inventoryHeight = 3;
                int inventoryWidth = 3;

                connection.Open();

                using (SqliteTransaction transaction = connection.BeginTransaction())
                {
                    using SqliteCommand command = connection.CreateCommand();

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
                    using (SqliteDataReader reader = command.ExecuteReader())
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
