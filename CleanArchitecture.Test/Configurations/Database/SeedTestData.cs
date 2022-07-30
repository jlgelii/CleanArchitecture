using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Test.Configurations.Database
{
    public class SeedTestData
    {
        public static void Seed(SampleDbContext context)
        {
            SeedUserAccount(context);
            SeedPerson(context);
        }

        public static void UnSeedData(SampleDbContext context)
        {
            if (context.UserAccount.Count() != 0)
                context.UserAccount.RemoveRange(context.UserAccount.ToList());

            if (context.Person.Count() != 0)
                context.Person.RemoveRange(context.Person.ToList());

            context.SaveChanges();
        }




        public static async void SeedUserAccount(SampleDbContext context)
        {
            var users = new List<UserAccount>()
            {
                new UserAccount() { Id = 1, Username = "User1", Password = "Password1" },
                new UserAccount() { Id = 2, Username = "User2", Password = "Password1" },
                new UserAccount() { Id = 3, Username = "User3", Password = "Password1" },
                new UserAccount() { Id = 4, Username = "User4", Password = "Password1" },
                new UserAccount() { Id = 5, Username = "User5", Password = "Password1" },
            };

            if (context.UserAccount.Count() == 0)
                await context.AddRangeAsync(users);

            context.SaveChanges();
        }


        public static async void SeedPerson(SampleDbContext context)
        {
            var person = new List<Person>()
            {
                new Person() { Id = 1, Firstname = "Firstname 1", Lastname = "Lastname 1", Gender = "Male", BirthDate = Convert.ToDateTime("2010-1-1"), UserAccountId = 1 },
                new Person() { Id = 2, Firstname = "Firstname 2", Lastname = "Lastname 2", Gender = "Female", BirthDate = Convert.ToDateTime("2010-1-1"), UserAccountId = 2 },
                new Person() { Id = 3, Firstname = "Firstname 3", Lastname = "Lastname 3", Gender = "Male", BirthDate = Convert.ToDateTime("2010-1-1"), UserAccountId = 3 },
                new Person() { Id = 4, Firstname = "Firstname 4", Lastname = "Lastname 4", Gender = "Male", BirthDate = Convert.ToDateTime("2010-1-1"), UserAccountId = 4 },
                new Person() { Id = 5, Firstname = "Firstname 5", Lastname = "Lastname 5", Gender = "Female", BirthDate = Convert.ToDateTime("2010-1-1"), UserAccountId = 5 },
            };

            if (context.Person.Count() == 0)
                await context.Person.AddRangeAsync(person);

            context.SaveChanges();
        }
    }
}
