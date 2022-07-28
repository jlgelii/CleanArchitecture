using CleanArchitecture.Application.Persons.Queries.GetPersonById;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Test.Configurations.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Test.Application.Persons.Queries
{
    public class GetPersonByIdQueryTest : BaseTest
    {
        private readonly GetPersonByIdQueryHandler _sut;

        public GetPersonByIdQueryTest()
        {
            _sut = new GetPersonByIdQueryHandler(_context);
        }


        [Fact]
        public async void GetPersonById_ShouldGetPerson_WhenParameterIsValid()
        {
            // Arrange
            var param = new GetPersonByIdQuery(2);

            // Act
            var result = await _sut.Handle(param, CancellationToken.None);

            // Assert
            var date = new DateTimeServiceTest();
            var user = new JwtServiceTest();

            var expected = new Person() 
            { 
                Id = 2, Firstname = "Firstname 2", 
                Lastname = "Lastname 2", 
                Gender = "Female", 
                BirthDate = Convert.ToDateTime("2010-1-1"),
                CreatedDate = date.Now,
                CreatedBy = user.GetLoggedUser().UserId
            };

            result.Error
                  .Should().BeFalse();

            result.Data
                  .Should().BeEquivalentTo(expected);
        }


        [Fact]
        public async void GetPersonById_ShouldReturnError_WhenPersonNotExist()
        {
            // Arrange
            var param = new GetPersonByIdQuery(6);

            // Act
            var result = await _sut.Handle(param, CancellationToken.None);

            // Assert
            result.Error
                  .Should().BeTrue();
        }
    }
}
