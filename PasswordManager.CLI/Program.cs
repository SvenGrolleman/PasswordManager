using PasswordManager.Database;
using PasswordManager.Database.Entities;
using System.Collections.Generic;
using System.Configuration;

namespace PasswordManager.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["PasswordManager"].ConnectionString;
            var test = new PwRepository(connectionString);
            var entries = new List<PasswordEntry>();
        }
    }
}
