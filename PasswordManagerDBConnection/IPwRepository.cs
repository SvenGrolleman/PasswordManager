using PasswordManager.Database.Entities;
using System.Collections.Generic;

namespace PasswordManager.Database
{
    public interface IPwRepository
    {
        int DeletePasswordEntries();
        int DeletePasswordEntry(PasswordEntry entry);
        int DeletePasswordEntry(int passwordEntryId);
        MainPassword GetMainPassword();
        IEnumerable<PasswordEntry> GetPasswordEntries();
        PasswordEntry GetPasswordEntry(int id);
        int InsertMainPassword(MainPassword mainPassword);
        int InsertPasswordEntries(IEnumerable<PasswordEntry> entries);
        int InsertPasswordEntry(PasswordEntry entry);
        int UpdatePasswordEntries(IEnumerable<PasswordEntry> entries);
        int UpdatePasswordEntry(PasswordEntry entry);
        int UpdateMainPassword(MainPassword mainPassword);
    }
}