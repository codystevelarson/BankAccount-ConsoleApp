namespace SGBank.Data.Migrations
{
    using SGBank.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SGBank.Data.AccountDBEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SGBank.Data.AccountDBEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Accounts.AddOrUpdate(
                a => a.AccountNumber,
                new Account
                {
                    AccountNumber = "12345",
                    Name = "Timmy Testy",
                    Balance = 350.66m,
                    Type = AccountType.Premium
                },
                new Account
                {
                    AccountNumber = "11111",
                    Name = "Chad Testy",
                    Balance = 150.42m,
                    Type = AccountType.Basic
                },
                new Account
                {
                    AccountNumber = "00000",
                    Name = "Greg Testy",
                    Balance = 3.50m,
                    Type = AccountType.Free
                },
                new Account
                {
                    AccountNumber = "33333",
                    Name = "Bob Testy",
                    Balance = 4000.40m,
                    Type = AccountType.Premium
                },
                new Account
                {
                    AccountNumber = "44444",
                    Name = "Timmy Testy",
                    Balance = -150.66m,
                    Type = AccountType.Premium
                });
        }
    }
}
