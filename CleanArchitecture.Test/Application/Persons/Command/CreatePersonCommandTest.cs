using CleanArchitecture.Application.Persons.Command.CreatePerson;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Test.Application.Persons.Command
{
    public class CreatePersonCommandTest : BaseTest
    {
        private readonly CreatePersonCommandHandler _sut;

        public CreatePersonCommandTest()
        {
            _sut = new CreatePersonCommandHandler(_context);
        }


        [Fact]
        public async void CreatePerson_ShouldAddPerson_WhenParametersAreValid()
        {
            // Arrange
            var param = new CreatePersonCommand(Firstname: "Jepoy",
                                                Lastname: "Leroy",
                                                Gender: "Male",
                                                BirthDate: _dateTimeService.Now);

            // Act
            var response = await _sut.Handle(param, CancellationToken.None);

            // Assert
            var expectedExist = _context.Person
                                   .FirstOrDefault(p => p.Id == response.Data.Id);

            var expectedCount = _context.Person
                                        .Count();

            response.Error
                    .Should().BeFalse();

            expectedCount.Should().Be(6);
            expectedExist.Should().NotBeNull();

            response.Data.Id
                         .Should().Be(6);

            response.Data.Firstname
                         .Should().Be("Jepoy");

            response.Data.Lastname
                         .Should().Be("Leroy");

            response.Data.Gender
                         .Should().Be("Male");

            response.Data.BirthDate
                         .Should().Be(_dateTimeService.Now);

            response.Data.CreatedBy
                         .Should().Be(_jwtServices.GetLoggedUser().UserId);

            response.Data.CreatedDate
                         .Should().Be(_dateTimeService.Now);
        }


        [Fact]
        public async void CreatePerson_ShouldReturnError_WhenRequiredFieldsDoesNotExist()
        {
            // Arrange
            var validator = new CreatePersonCommandValidator();
            var param = new CreatePersonCommand(Firstname: "",
                                                Lastname: "Leroy",
                                                Gender: "Male",
                                                BirthDate: _dateTimeService.Now);

            var param2 = new CreatePersonCommand(Firstname: "Eloy",
                                                Lastname: "",
                                                Gender: "Male",
                                                BirthDate: _dateTimeService.Now);

            // Act
            var response = await validator.Validate(param);
            var response2 = await validator.Validate(param2);

            // Assert
            response.IsSuccessful
                    .Should().BeFalse();

            response2.IsSuccessful
                    .Should().BeFalse();

        }
    }
}
