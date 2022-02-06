using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Test.Configurations.Database
{
    public class SeedData
    {
        public static void SeedUserAccount(SampleDbContext context)
        {
            var users = new List<UserAccount>()
            {
                new UserAccount() { Id = 1, Username = "User1", Password = "Password1" },
                new UserAccount() { Id = 2, Username = "User2", Password = "Password1" },
                new UserAccount() { Id = 3, Username = "User3", Password = "Password1" },
                new UserAccount() { Id = 4, Username = "User4", Password = "Password1" },
                new UserAccount() { Id = 5, Username = "User5", Password = "Password1" },
            };

            context.AddRange(users);
            context.SaveChanges();
        }
    }
}
