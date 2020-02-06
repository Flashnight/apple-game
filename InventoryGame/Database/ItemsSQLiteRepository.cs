using InventoryGame.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
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
        private ConnectionStringSettings _connectionString;

        /// <summary>
        /// Creates SQLite connections.
        /// </summary>
        private SQLiteFactory _sqliteFactory;

        /// <summary>
        /// Hides DB operations for items.
        /// </summary>
        public ItemsSQLiteRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            _sqliteFactory = (SQLiteFactory)DbProviderFactories.GetFactory(_connectionString.ProviderName);
        }

        /// <summary>
        /// Returns image's data by id.
        /// </summary>
        /// <param name="id">Identifier of item.</param>
        /// <returns>Model's data model.</returns>
        public Item GetItemById(int id)
        {
            using (SQLiteConnection connection = (SQLiteConnection)_sqliteFactory.CreateConnection())
            {
                connection.ConnectionString = _connectionString.ConnectionString;
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = $@"SELECT * 
                    FROM Item 
                    WHERE Id = {id};";

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();

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
