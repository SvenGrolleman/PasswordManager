using System;
using System.Diagnostics.CodeAnalysis;

namespace PasswordManager.Database.Entities
{
    public class PasswordEntry : CommonBase, IEquatable<PasswordEntry>
    {
        public int PasswordEntryId { get; set; }
        public string Website { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


        public string IV { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                return Equals((PasswordEntry)obj);
            }
        }

        public bool Equals([AllowNull] PasswordEntry other)
        {
            return Password == other.Password && Username == other.Username && Website == other.Website && IV == other.IV;
        }

        public override int GetHashCode()
        {
            return Website.GetHashCode() ^ Username.GetHashCode() ^ Password.GetHashCode() ^ IV.GetHashCode();
        }
    }
}
