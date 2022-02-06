using CleanArchitecture.Application.Configurations.Services;
using CleanArchitecture.Application.UserAccounts.Queries.GetUserByLogin;
using CleanArchitecture.Domain.Tokens;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Test.Configurations.Database;
using CleanArchitecture.Test.Configurations.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Test.Application.UserAccounts.Queries
{
    public class GetUserByLoginTests : IDisposable
    {
        private readonly SampleDbContext _context;
        private readonly IJwtServices _jwtServices;
        private readonly IDateTimeService _datetimService;
        private readonly GetUserByLoginQueryHandler _sut;

        public GetUserByLoginTests()
        {
            _jwtServices = new JwtServiceTest();
            _datetimService = new DateTimeServiceTest();

            var option = new DbContextOptionsBuilder<SampleDbContext>()
                                                   .UseInMemoryDatabase(databaseName: "SampleDb")
                                                   .UseInternalServiceProvider(new ServiceCollection().AddEntityFrameworkInMemoryDatabase()
                                                                                                      .BuildServiceProvider())
                                                   .Options;

            _context = new SampleDbContext(option, _datetimService, _jwtServices);

            _sut = new GetUserByLoginQueryHandler(_context, _jwtServices);

            SeedTestData.Seed(_context);
        }



        [Fact]
        public async void GetUserByLogin_ShouldGetUser_WhenAllParametersAreValid()
        {
            // Arrange
            var query = new GetUserByLoginQuery("User1", "Password1");
            var user = _context.UserAccount
                               .FirstOrDefault(u => u.Username == query.Username
                                        && u.Password == query.Password); 

            // Act
            var response = await _sut.Handle(query, CancellationToken.None);

            // Assert
            response.Error
                    .Should().BeFalse();

            response.Data.Id
                    .Should().Be(user.Id);

            response.Data.Username
                    .Should().Be(user.Username);

            response.Data.Password
                    .Should().Be(user.Password);
        }

        [Fact]
        public async void GetUserByLogin_ShouldValidateUser_WhenUsernameIsEmpty()
        {
            // Arrange
            var validator = new GetUserLoginQueryValidator();
            var query = new GetUserByLoginQuery(username: "", password: "password");

            // Act
            var response = await validator.Validate(query);

            // Assert
            response.IsSuccessful
                .Should().BeFalse();
        }

        [Fact]
        public async void GetUserByLogin_ShouldValidateUser_WhenPasswordIsEmpty()
        {
            // Arrange
            var validator = new GetUserLoginQueryValidator();
            var query = new GetUserByLoginQuery(username: "user", password: "");

            // Act
            var response = await validator.Validate(query);

            // Assert
            response.IsSuccessful
                .Should().BeFalse();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
