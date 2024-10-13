using InventoryGame.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
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
        public async Task<Inventory> CreateNewInventoryAsync()
        {
            await using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                int inventoryHeight = 3;
                int inventoryWidth = 3;

                await connection.OpenAsync();

                await using (SqliteTransaction transaction = connection.BeginTransaction())
                {
                    await using SqliteCommand command = connection.CreateCommand();

                    command.CommandText = $@"INSERT INTO Inventory (Height, Widht)
                    VALUES ({inventoryHeight}, {inventoryWidth});";
                    command.CommandType = CommandType.Text;
                    await command.ExecuteNonQueryAsync();

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
                            await command.ExecuteNonQueryAsync();
                        }

                    

                    command.CommandText = $@"SELECT Id, Row, Column FROM InventoryCell
                    WHERE InventoryId = {id}
                    ORDER BY ID;";
                    await using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        for (int i = 0; i < inventoryHeight; i++)
                            for (int j = 0; j < inventoryWidth; j++)
                            {
                                await reader.ReadAsync();

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

                    await transaction.CommitAsync();

                    return inventory;
                }
            }
        }
    }
}
