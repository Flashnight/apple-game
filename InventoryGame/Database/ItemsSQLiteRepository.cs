using InventoryGame.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace InventoryGame.Database
{
    /// <summary>
    /// Hides DB operations for items.
    /// </summary>
    public class ItemsSQLiteRepository : IItemsDbRepository
    {
        /// <summary>
        /// Contains connections string's data.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Hides DB operations for items.
        /// </summary>
        public ItemsSQLiteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Returns image's data by id.
        /// </summary>
        /// <param name="id">Identifier of item.</param>
        /// <returns>Model's data model.</returns>
        public async Task<Item> GetItemByIdAsync(int id)
        {
            await using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                await using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = $@"SELECT * 
                    FROM Item 
                    WHERE Id = {id};";

                    await using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        await reader.ReadAsync();

                        Item item = new Item
                        {
                            Id = reader.GetInt32(0),
                            ItemName = reader.GetString(1),
                            ImageSource = reader.GetString(2)
                        };

                        return item;
                    }
                }
            }
        }
    }
}
