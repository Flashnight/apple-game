using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleGame.Database
{
    /// <summary>
    /// It initializes new database.
    /// </summary>
    public class SQLiteDatabaseMaker : IDatabaseMaker
    {
        /// <summary>
        /// Initializes new database if it doesnt exist for SQLLite.
        /// </summary>
        public void CreateDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString.ConnectionString);

            var baseName = builder.DataSource;

            if (File.Exists(baseName))
            {
                return;
            }

            SQLiteConnection.CreateFile(baseName);

            try
            {
                SQLiteFactory sqliteFactory = (SQLiteFactory)DbProviderFactories.GetFactory(connectionString.ProviderName);

                using (SQLiteConnection connection = (SQLiteConnection)sqliteFactory.CreateConnection())
                {
                    connection.ConnectionString = connectionString.ConnectionString;
                    connection.Open();

                    using (SQLiteCommand command = new SQLiteCommand(connection))
                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        command.CommandText = @"CREATE TABLE [Item] (
                        [Id]    INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        [Name]  TEXT NOT NULL,
                        [ImageSource]   TEXT NOT NULL
                        );";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();

                        command.CommandText = @"CREATE TABLE [Inventory] (
	                    [Id]	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	                    [Height]	INTEGER NOT NULL,
	                    [Widht]	INTEGER NOT NULL
                        );";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();

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
                        command.ExecuteNonQuery();

                        command.CommandText = @"INSERT INTO Item([Id], [Name], [ImageSource])
                        VALUES (0, 'Apple', '../Resources/Pictures/apple.png')";
                        command.CommandType = CommandType.Text;
                        command.ExecuteNonQuery();

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                File.Delete(baseName);

                throw ex;
            }
        }
    }
}
