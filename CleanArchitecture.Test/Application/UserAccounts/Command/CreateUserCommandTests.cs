using CleanArchitecture.Application.Configurations.Database;
using CleanArchitecture.Application.Configurations.Services;
using CleanArchitecture.Application.UserAccounts.Command.CreateUser;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Test.Configurations.Database;
using CleanArchitecture.Test.Configurations.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Test.Application.UserAccounts.Command
{
    public class CreateUserCommandTests
    {
        private readonly CreateUserCommandHandler _sut;
        private readonly SampleDbContext _context;
        private readonly IJwtServices _jwtServices;

        public CreateUserCommandTests()
        {
            _jwtServices = new JwtServiceTest();
            _context = new SampleDbContext(new DbContextOptionsBuilder<SampleDbContext>()
                                                .UseInMemoryDatabase(databaseName: "SampleDb")
                                                .Options);

            _sut = new CreateUserCommandHandler(_context, _jwtServices);
        }

        [Fact]
        public async void CreateUser_ShouldCreateUser_WhenAllParametersAreValid()
        {
            // Arrange
            var command = new CreateUserCommand("user6", "password");

            // Act
            var response = await _sut.Handle(command, CancellationToken.None);

            // Assert
            _context.UserAccount
                    .ToList()
                    .Should().HaveCount(1);

            _context.UserAccount
                    .FirstOrDefault(u => u.Username == "user6"
                                      && u.Password == "password")
                    .Should().NotBeNull();
        }


        
    }
}
