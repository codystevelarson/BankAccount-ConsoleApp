using SGBank.Models;
using System.Data.Entity;

namespace SGBank.Data
{
    public class AccountDBEntities : DbContext
    {
        public AccountDBEntities() : base("SGBank")
        { }

        public DbSet<Account> Accounts { get; set; }

    }
}
