using CleanArchitecture.Application.UserAccounts.Queries.GetUsers;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Test.Application.UserAccounts.Queries
{
    public class GetUsersTests : BaseTest
    {
        private readonly GetUsersQueryHandler _sutHandler;

        public GetUsersTests()
        {
            _sutHandler = new GetUsersQueryHandler(_context);
        }

        [Fact]
        public async void GetUsers_ShouldGetAllUsers()
        {
            // Arrange
            var query = new GetUsersQuery();

            // Act
            var response = await _sutHandler.Handle(query, CancellationToken.None);

            // Assert
            response.Data.Count()
                .Should().Be(5);
        }
    }
}
