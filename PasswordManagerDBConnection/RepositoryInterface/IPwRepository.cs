using PasswordManager.Database.Entities;
using System.Collections.Generic;

namespace PasswordManager.Database
{
    public interface IPwRepository
    {
        int DeletePasswordEntry(PasswordEntry entry);
        IEnumerable<PasswordEntry> GetPasswordEntries();
        PasswordEntry GetPasswordEntry(int id);
        int InsertPasswordEntries(IEnumerable<PasswordEntry> entries);
        int InsertPasswordEntry(PasswordEntry entry);
        int UpdatePasswordEntries(IEnumerable<PasswordEntry> entries);
        int UpdatePasswordEntry(PasswordEntry entry);
    }
}