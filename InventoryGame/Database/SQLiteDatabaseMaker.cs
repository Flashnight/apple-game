using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace InventoryGame.Database
{
    /// <summary>
    /// It initializes new database.
    /// </summary>
    public class SQLiteDatabaseMaker : IDatabaseMaker
    {
        private readonly string _connectionString;

        public SQLiteDatabaseMaker(IConfiguration configuraion)
        {
            _connectionString = configuraion.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Initializes new database if it doesnt exist for SQLLite.
        /// </summary>
        public async Task CreateDatabaseAsync()
        {
            string dataSource = string.Empty;
            try
            {
                await using (SqliteConnection connection = new SqliteConnection(_connectionString))
                {
                    if (File.Exists(connection.DataSource))
                    {
                        return;
                    }

                    dataSource = connection.DataSource;
                    await connection.OpenAsync();

                    
                    await using (SqliteTransaction transaction = connection.BeginTransaction())
                    {
                        await using SqliteCommand command = connection.CreateCommand();

                        command.CommandText = @"CREATE TABLE [Item] (
                        [Id]    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        [Name]  TEXT NOT NULL,
                        [ImageSource]   TEXT NOT NULL
                        );";
                        command.CommandType = CommandType.Text;
                        await command.ExecuteNonQueryAsync();

                        command.CommandText = @"CREATE TABLE [Inventory] (
	                    [Id]	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	                    [Height]	INTEGER NOT NULL,
	                    [Widht]	INTEGER NOT NULL
                        );";
                        command.CommandType = CommandType.Text;
                        await command.ExecuteNonQueryAsync();

                        command.CommandText = @"CREATE TABLE [InventoryCell] (
	                    [Id]	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	                    [Row]	INTEGER NOT NULL,
	                    [Column]	INTEGER NOT NULL ,
	                    [Amount]	INTEGER NOT NULL,
	                    [ItemId]	INTEGER,
	                    [InventoryId]	INTEGER NOT NULL,
	                    FOREIGN KEY([InventoryId]) REFERENCES [InventoryCell]([Id]),
                        FOREIGN KEY([ItemId]) REFERENCES[Item]([Id])
                        );";
                        command.CommandType = CommandType.Text;
                        await command.ExecuteNonQueryAsync();

                        command.CommandText = @"INSERT INTO Item([Id], [Name], [ImageSource])
                        VALUES (0, 'Apple', '../Resources/Pictures/apple.png')";
                        command.CommandType = CommandType.Text;
                        await command.ExecuteNonQueryAsync();

                        await transaction.CommitAsync();
                    }
                }
            }
            catch (Exception)
            {
                if (!string.IsNullOrEmpty(dataSource) && File.Exists(dataSource))
                    File.Delete(dataSource);

                throw;
            }
        }
    }
}
