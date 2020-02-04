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

namespace AppleGame.Misc
{
    /// <summary>
    /// It initializes new database.
    /// </summary>
    public static class DatabaseMaker
    {
        /// <summary>
        /// Initializes new database if it doesnt exist for SQLLite.
        /// </summary>
        public static void CreateSQLiteDatabase()
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
                    connection.ConnectionString = "Data Source = " + baseName;
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
	                    [Id]	INTEGER NOT NULL,
	                    [Row]	INTEGER NOT NULL,
	                    [Column]	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	                    [Amount]	INTEGER NOT NULL,
	                    [ItemId]	INTEGER NOT NULL,
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
