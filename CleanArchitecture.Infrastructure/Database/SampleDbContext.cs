using CleanArchitecture.Application.Configurations.Database;
using CleanArchitecture.Application.Configurations.Services;
using CleanArchitecture.Domain.Common;
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
        private readonly IJwtServices _jwtServices;
        private readonly IDateTimeService _dateTimeService;

        public SampleDbContext(IConnectionsConfigurations connections,
            IDateTimeService dateTimeService,
            IJwtServices jwtServices)
        {
            _databaseName = connections.GetConnectionString();
            _dateTimeService = dateTimeService;
            _jwtServices = jwtServices;
        }

        public SampleDbContext(DbContextOptions options,
            IDateTimeService dateTimeService,
            IJwtServices jwtServices) : base(options)
        {
            this._dateTimeService = dateTimeService;
            _jwtServices = jwtServices;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_databaseName))
                optionsBuilder.UseSqlServer(_databaseName);
        }


        public DbSet<UserAccount> UserAccount { get; set; }


        void ISampleDbContext.SaveChanges()
        {
            SaveChanges();
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntities>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _jwtServices.GetLoggedUser().UserId;
                        entry.Entity.CreatedDate = _dateTimeService.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _jwtServices.GetLoggedUser().UserId;
                        entry.Entity.UpdatedDate = _dateTimeService.Now;
                        break;
                }
            }

            return base.SaveChanges();
        }
    }
}
