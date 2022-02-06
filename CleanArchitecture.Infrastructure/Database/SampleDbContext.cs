using CleanArchitecture.Application.Configurations.Database;
using CleanArchitecture.Application.Configurations.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Database
{
    public class SampleDbContext : DbContext, ISampleDbContext
    {
        private readonly string _databaseName;

        public SampleDbContext(IConnectionsConfigurations connections)
        {
            _databaseName = connections.GetConnectionString();
        }

        public SampleDbContext(DbContextOptions options) : base(options)
        {
        }



        public DbSet<UserAccount> UserAccount { get; set; }


        void ISampleDbContext.SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
