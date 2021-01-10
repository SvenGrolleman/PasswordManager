﻿using Microsoft.Data.Sqlite;
using PasswordManager.Database.Entities;
using System.Collections.Generic;

namespace PasswordManager.Database
{
    internal class DatabaseSqlCommands
    {
        private static readonly string _createTable =
            "CREATE TABLE Passwords (PasswordEntryId INTEGER PRIMARY KEY, Website TEXT NOT NULL, Username TEXT NOT NULL, Password TEXT NOT NULL, IV TEXT NOT NULL) ";

        private static readonly string _insertPasswordEntry =
            $"INSERT INTO Passwords({_website}, {_username}, {_password}, {_IV})" +
            $"VALUES(@{_website}, @{_username}, @{_password}, @{_IV})";
        private static readonly string _insertPasswordEntries =
            $"INSERT INTO Passwords({_website}, {_username}, {_password}, {_IV})" +
            "VALUES";

        private static readonly string _getPasswordEntry =
            "SELECT * " +
            "FROM Passwords " +
            $"WHERE PasswordEntryId = @{_passwordId}";
        private static readonly string _getPasswordEntries =
            "SELECT * " +
            "FROM Passwords";
        public readonly string UpdatePasswordEntryCommand =
            $"UPDATE Passwords SET {_website} = @{_website}, {_username} = @{_username}, {_password} = @{_password}, {_IV} = @{_IV} WHERE {_passwordId} = @{_passwordId}";

        private static string _deletePasswordEntry =
            $"DELETE FROM Passwords WHERE {_passwordId} = @{_passwordId}";
        private static string _deletePasswordEntries =
            "DELETE FROM Passwords";

        private const string _passwordId = "PasswordEntryId";
        private const string _website = "Website";
        private const string _username = "Username";
        private const string _password = "Password";
        private const string _IV = "IV";

        public SqliteCommand CreateTable(SqliteConnection conn)
        {
            return new SqliteCommand
            {
                CommandText = _createTable,
                Connection = conn,
            };
        }

        public SqliteCommand InsertPasswordEntryCommand(PasswordEntry entry, SqliteConnection conn)
        {
            var comm = new SqliteCommand
            {
                CommandText = _insertPasswordEntry,
                Connection = conn
            };
            comm.Parameters.AddWithValue($"@{_website}", entry.Website);
            comm.Parameters.AddWithValue($"@{_username}", entry.Username);
            comm.Parameters.AddWithValue($"@{_password}", entry.Password);
            comm.Parameters.AddWithValue($"@{_IV}", entry.IV);
            return comm;
        }

        public SqliteCommand InsertPasswordEntriesCommand(IEnumerable<PasswordEntry> entries, SqliteConnection conn)
        {
            var comm = new SqliteCommand
            {
                CommandText = _insertPasswordEntries,
                Connection = conn
            };
            int i = 0;
            foreach (var entry in entries)
            {
                comm.CommandText += $" (@{_website}{i}, @{_username}{i}, @{_password}{i}, @{_IV}{i}),";
                comm.Parameters.AddWithValue($"@{_website}{i}", entry.Website);
                comm.Parameters.AddWithValue($"@{_username}{i}", entry.Username);
                comm.Parameters.AddWithValue($"@{_password}{i}", entry.Password);
                comm.Parameters.AddWithValue($"@{_IV}{i}", entry.IV);
                ++i;
            }
            comm.CommandText = comm.CommandText.Remove(comm.CommandText.Length - 1);
            return comm;
        }

        public SqliteCommand GetPasswordEntry(int id, SqliteConnection conn)
        {
            var comm = new SqliteCommand
            {
                CommandText = _getPasswordEntry,
                Connection = conn,
            };
            comm.Parameters.AddWithValue($"@{_passwordId}", id);
            return comm;
        }

        public SqliteCommand GetPasswordEntries(SqliteConnection conn)
        {
            return new SqliteCommand
            {
                CommandText = _getPasswordEntries,
                Connection = conn,
            };
        }

        public SqliteCommand UpdatePasswordEntry(PasswordEntry entry, SqliteConnection conn)
        {
            var comm = new SqliteCommand
            {
                CommandText = UpdatePasswordEntryCommand,
                Connection = conn,
            };
            comm.Parameters.AddWithValue($"@{_passwordId}", entry.PasswordEntryId);
            comm.Parameters.AddWithValue($"@{_website}", entry.Website);
            comm.Parameters.AddWithValue($"@{_username}", entry.Username);
            comm.Parameters.AddWithValue($"@{_password}", entry.Password);
            comm.Parameters.AddWithValue($"@{_IV}", entry.IV);
            return comm;
        }

        public IEnumerable<SqliteParameter> GetPasswordEntryUpdateParameters(PasswordEntry entry)
        {
            return new List<SqliteParameter>()
            {
                new SqliteParameter($"@{_passwordId}", entry.PasswordEntryId),
                new SqliteParameter($"@{_website}", entry.Website),
                new SqliteParameter($"@{_username}", entry.Username),
                new SqliteParameter($"@{_password}", entry.Password),
                new SqliteParameter($"@{_IV}", entry.IV),
            };
        }

        public SqliteCommand DeletePasswordEntry(int id, SqliteConnection conn)
        {
            var comm = new SqliteCommand()
            {
                CommandText = _deletePasswordEntry,
                Connection = conn,
            };
            comm.Parameters.AddWithValue($"@{_passwordId}", id);
            return comm;
        }

        public SqliteCommand DeletePasswordEnties(SqliteConnection conn)
        {
            return new SqliteCommand()
            {
                CommandText = _deletePasswordEntries,
                Connection = conn,
            };
        }
    }
}
