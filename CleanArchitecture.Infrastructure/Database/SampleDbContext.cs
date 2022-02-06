using CleanArchitecture.Application.Configurations.Services;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_databaseName);
        }


        void ISampleDbContext.SaveChanges()
        {
            base.SaveChanges();
        }
    }
}
