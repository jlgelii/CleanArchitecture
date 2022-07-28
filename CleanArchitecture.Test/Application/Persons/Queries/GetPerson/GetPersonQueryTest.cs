using CleanArchitecture.Application.Persons.Queries.GetPerson;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Test.Configurations.Database;
using CleanArchitecture.Test.Configurations.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Test.Application.Persons.Queries.GetPerson
{
    public class GetPersonQueryTest : BaseTest
    {
        private readonly GetPersonQueryHandler _sut;

        public GetPersonQueryTest()
        {
            _sut = new GetPersonQueryHandler(_context);
        }


        [Fact]
        public async void GetPerson_ShouldReturnAllData_WhenCalled()
        {
            // Arrange
            var param = new GetPersonQuery();

            // Act
            var result = await _sut.Handle(param, CancellationToken.None);

            // Assert
            var date = new DateTimeServiceTest();
            var user = new JwtServiceTest();

            var expected = new List<Person>()
            {
                new Person() { Id = 1, Firstname = "Firstname 1", Lastname = "Lastname 1", Gender = "Male", BirthDate = Convert.ToDateTime("2010-1-1"), CreatedDate =  date.Now, CreatedBy = user.GetLoggedUser().UserId },
                new Person() { Id = 2, Firstname = "Firstname 2", Lastname = "Lastname 2", Gender = "Female", BirthDate = Convert.ToDateTime("2010-1-1"), CreatedDate =  date.Now, CreatedBy = user.GetLoggedUser().UserId },
                new Person() { Id = 3, Firstname = "Firstname 3", Lastname = "Lastname 3", Gender = "Male", BirthDate = Convert.ToDateTime("2010-1-1"), CreatedDate =  date.Now, CreatedBy = user.GetLoggedUser().UserId },
                new Person() { Id = 4, Firstname = "Firstname 4", Lastname = "Lastname 4", Gender = "Male", BirthDate = Convert.ToDateTime("2010-1-1"), CreatedDate =  date.Now, CreatedBy = user.GetLoggedUser().UserId },
                new Person() { Id = 5, Firstname = "Firstname 5", Lastname = "Lastname 5", Gender = "Female", BirthDate = Convert.ToDateTime("2010-1-1"), CreatedDate =  date.Now, CreatedBy = user.GetLoggedUser().UserId },
            };

            result.Error
                  .Should().BeFalse();

            result.Data
                  .Should().BeEquivalentTo(expected);
        }
    }
}
