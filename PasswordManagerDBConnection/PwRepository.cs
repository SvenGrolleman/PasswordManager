using Microsoft.Data.Sqlite;
using PasswordManager.Database.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace PasswordManager.Database
{
    public class PwRepository : IPwRepository
    {
        private string _connectionString;
        private DatabaseSqlCommands _commands = new DatabaseSqlCommands();
        public SqliteConnection Connection { get; }

        public PwRepository(string dbLocation)
        {
            _connectionString = dbLocation;

            Connection = new SqliteConnection(dbLocation);
            using (var conn = new SqliteConnection(dbLocation))
            {
                if (!databaseExist(conn) && !conn.ConnectionString.Contains("Mode=Memory"))
                {
                    CreatePasswordsTable();
                    CreateMainPasswordTable();
                }
            }
        }

        private bool databaseExist(SqliteConnection conn)
        {
            return File.Exists(conn.DataSource);
        }

        public int CreatePasswordsTable()
        {
            int affectedRows = 0;
            using (var conn = new SqliteConnection(_connectionString))
            {
                var comm = _commands.CreatePasswordsTable(conn);
                conn.Open();
                affectedRows = comm.ExecuteNonQuery();
                conn.Close();
            }
            return affectedRows;
        }

        public int CreateMainPasswordTable()
        {
            int affectedRows = 0;
            using (var conn = new SqliteConnection(_connectionString))
            {
                var comm = _commands.CreateMainPasswordTable(conn);
                conn.Open();
                affectedRows = comm.ExecuteNonQuery();
                conn.Close();
            }
            return affectedRows;
        }

        public int InsertPasswordEntry(PasswordEntry entry)
        {
            int affectedRows = 0;
            using (var conn = new SqliteConnection(_connectionString))
            {
                var comm = _commands.InsertPasswordEntryCommand(entry, conn);
                conn.Open();
                affectedRows = comm.ExecuteNonQuery();
                conn.Close();
            }
            return affectedRows;
        }

        public int InsertPasswordEntries(IEnumerable<PasswordEntry> entries)
        {
            int affectedRows = 0;
            using (var conn = new SqliteConnection(_connectionString))
            {
                var comm = _commands.InsertPasswordEntriesCommand(entries, conn);
                conn.Open();
                affectedRows = comm.ExecuteNonQuery();
                conn.Close();
            }
            return affectedRows;
        }

        public int InsertMainPassword(MainPassword mainPassword)
        {
            int affectedRows = 0;
            using (var conn = new SqliteConnection(_connectionString))
            {
                var comm = _commands.InsertMainPassword(mainPassword, conn);
                conn.Open();
                affectedRows = comm.ExecuteNonQuery();
                conn.Close();
            }
            return affectedRows;
        }
        public PasswordEntry GetPasswordEntry(int id)
        {
            var passwordEntry = new PasswordEntry()
            {
                PasswordEntryId = id
            };
            using (var conn = new SqliteConnection(_connectionString))
            {
                var comm = _commands.GetPasswordEntry(id, conn);
                conn.Open();
                using (var sqlReader = comm.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        passwordEntry.Website = sqlReader.GetString(1);
                        passwordEntry.Username = sqlReader.GetString(2);
                        passwordEntry.Password = sqlReader.GetString(3);
                        passwordEntry.IV = sqlReader.GetString(4);
                    }
                }
            }
            return passwordEntry;
        }

        public IEnumerable<PasswordEntry> GetPasswordEntries()
        {
            List<PasswordEntry> passwordEntries = new List<PasswordEntry>();

            using (var conn = new SqliteConnection(_connectionString))
            {
                var comm = _commands.GetPasswordEntries(conn);
                conn.Open();
                using (var sqlReader = comm.ExecuteReader())
                {
                    while (sqlReader.Read())
                    {
                        passwordEntries.Add(new PasswordEntry()
                        {
                            PasswordEntryId = sqlReader.GetInt32(0),
                            Website = sqlReader.GetString(1),
                            Username = sqlReader.GetString(2),
                            Password = sqlReader.GetString(3),
                            IV = sqlReader.GetString(4),
                        });
                    }
                }
            }
            return passwordEntries;
        }

        public MainPassword GetMainPassword()
        {

            var mainPassword = new MainPassword();
            using (var conn = new SqliteConnection(_connectionString))
            {
                var comm = _commands.GetMainPassword(conn);
                using (var sqlReader = comm.ExecuteReader())
                {
                    mainPassword = new MainPassword
                    {
                        MainId = sqlReader.GetInt32(0),
                        Password = sqlReader.GetString(1),
                        MainIV = sqlReader.GetString(2),
                    };
                }
            }
            return mainPassword;
        }

        public int UpdatePasswordEntry(PasswordEntry entry)
        {
            int affectedRows = 0;
            using (var conn = new SqliteConnection(_connectionString))
            {
                var comm = _commands.UpdatePasswordEntry(entry, conn);
                conn.Open();
                affectedRows = comm.ExecuteNonQuery();
                conn.Close();
            }
            return affectedRows;
        }

        public int UpdatePasswordEntries(IEnumerable<PasswordEntry> entries)
        {
            int affectedRows = 0;
            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();
                using (var trn = conn.BeginTransaction())
                {
                    try
                    {
                        var comm = new SqliteCommand(_commands.UpdatePasswordEntryCommand);
                        comm.Connection = conn;
                        comm.Transaction = trn;
                        foreach (var entry in entries)
                        {
                            comm.Parameters.AddRange(_commands.GetPasswordEntryUpdateParameters(entry));
                            affectedRows += comm.ExecuteNonQuery();
                            comm.Parameters.Clear();
                        }
                        trn.Commit();
                    }
                    catch (Exception)
                    {
                        trn.Rollback();
                        throw;
                    }
                }
            }
            return affectedRows;
        }

        public int DeletePasswordEntry(PasswordEntry entry)
        {
            int affectedRows = 0;
            using (var conn = new SqliteConnection(_connectionString))
            {
                var comm = _commands.DeletePasswordEntry(entry.PasswordEntryId, conn);
                conn.Open();
                affectedRows = comm.ExecuteNonQuery();
                conn.Close();
            }

            return affectedRows;
        }

        public int DeletePasswordEntries()
        {
            int affectedRows = 0;
            using (var conn = new SqliteConnection(_connectionString))
            {
                var comm = _commands.DeletePasswordEnties(conn);
                conn.Open();
                affectedRows = comm.ExecuteNonQuery();
                conn.Close();
            }
            return affectedRows;
        }
    }
}
