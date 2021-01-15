using NUnit.Framework;
using PasswordManager.Database;
using PasswordManager.Database.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PasswordManager.Tests
{
    public class DatabaseTests
    {

        private List<PasswordEntry> _entries = new List<PasswordEntry>();
        private const string _connectionString = "DataSource=InMemorySample;Mode=Memory;Cache=Shared";
        private PwRepository _repository;

        [OneTimeSetUp]
        public void Setup()
        {
            using (var reader = new StreamReader("Passwords.csv"))
            {
                var line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    var values = line.Split(",");
                    _entries.Add(new PasswordEntry()
                    {
                        Website = values[1],
                        Username = values[2],
                        Password = values[3],
                        IV = values[4],
                    });
                }
            }

            _repository = new PwRepository(_connectionString);
            _repository.Connection.Open();
            _repository.CreatePasswordsTable();
            _repository.CreateMainPasswordTable();
        }

        private void ResetPasswordsTable()
        {
            _repository.DeletePasswordEntries();
            _repository.InsertPasswordEntries(_entries);
        }

        [Test]
        public void TestPasswordEntriesInserts()
        {
            ResetPasswordsTable();
            var entries = new List<PasswordEntry>(_repository.GetPasswordEntries());
            Assert.AreEqual(18, entries.Count);
        }

        [Test]
        public void TestPasswordEntryInsert()
        {
            ResetPasswordsTable();
            var entry = new PasswordEntry
            {
                Website = "entryWebsite",
                Username = "entryUsername",
                Password = "entryPassword",
                IV = "entryIV"
            };
            _repository.InsertPasswordEntry(entry);
            var entries = new List<PasswordEntry>(_repository.GetPasswordEntries());
            Assert.AreEqual(19, entries.Count);
        }

        [Test]
        public void TestPasswordEntryDelete()
        {
            ResetPasswordsTable();
            var entry = new PasswordEntry
            {
                PasswordEntryId = 1,
            };

            _repository.DeletePasswordEntry(entry);
            var entries = new List<PasswordEntry>(_repository.GetPasswordEntries());
            Assert.AreEqual(17, entries.Count);
        }

        [Test]
        public void TestPasswordEntryUpdate()
        {
            ResetPasswordsTable();
            var entry = new PasswordEntry
            {
                PasswordEntryId = 1,
                Website = "updated Website",
                Username = "updated Username",
                Password = "updated Password",
                IV = "updated IV",
            };
            _repository.UpdatePasswordEntry(entry);
            var updatedEntry = _repository.GetPasswordEntry(1);
            //Assert.AreEqual(entry.Password, updatedEntry.Password);
            //Assert.AreEqual(entry.Username, updatedEntry.Username);
            //Assert.AreEqual(entry.Website, updatedEntry.Website);
            Assert.AreEqual(entry, updatedEntry);
        }

        [Test]
        public void TestInvalidPasswordEntryUpdate()
        {
            ResetPasswordsTable();
            var entry = new PasswordEntry
            {
                PasswordEntryId = 9999,
                Website = "updated Website",
                Username = "updated Username",
                Password = "updated Password",
                IV = "updated IV",
            };
            var rows = _repository.UpdatePasswordEntry(entry);
            Assert.AreEqual(0, rows);
        }

        [Test]
        public void TestPasswordEntriesUpdate()
        {
            ResetPasswordsTable();
            var entries = new List<PasswordEntry>()
            {
                new PasswordEntry
                {
                    PasswordEntryId = 1,
                    Website = "updated Website",
                    Username = "updated Username",
                    Password = "updated Password",
                    IV = "updated IV",
                },
                new PasswordEntry
                {
                    PasswordEntryId = 2,
                    Website = "updated Website two",
                    Username = "updated Username two",
                    Password = "updated Password two",
                    IV = "updated IV two",
                },
                new PasswordEntry
                {
                    PasswordEntryId = 3,
                    Website = "updated Website three",
                    Username = "updated Username three",
                    Password = "updated Password three",
                    IV = "updated IV three",
                }
            };
            _repository.UpdatePasswordEntries(entries);
            var returnEntries = new List<PasswordEntry>(_repository.GetPasswordEntries().Where(pw => pw.PasswordEntryId < 4));
            Assert.AreEqual(entries[0], returnEntries[0]);
            Assert.AreEqual(entries[1], returnEntries[1]);
            Assert.AreEqual(entries[2], returnEntries[2]);
        }
    }
}
