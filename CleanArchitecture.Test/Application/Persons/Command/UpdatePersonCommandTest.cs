using CleanArchitecture.Application.Persons.Command.UpdatePerson;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Test.Application.Persons.Command
{
    public class UpdatePersonCommandTest : BaseTest
    {
        private readonly UpdatePersonCommandHandler _sut;

        public UpdatePersonCommandTest()
        {
            _sut = new UpdatePersonCommandHandler(_context);
        }


        [Fact]
        public async void UpdatePerson_ShouldUpdatePerson_WhenParametersAreValid()
        {
            // Arrange
            var param = new UpdatePersonCommand(Id: 1,
                                                Firstname: "Vhon",
                                                Lastname: "Rey",
                                                BirthDate: _dateTimeService.Now,
                                                Gender: "Male");

            // Act
            var response = await _sut.Handle(param, CancellationToken.None);

            // Assert
            var expected = _context.Person
                                   .FirstOrDefault(p => p.Id == param.Id);

            response.Error
                    .Should().BeFalse();

            response.Data
                    .Firstname.Should().Be(expected.Firstname);
            response.Data
                    .Lastname.Should().Be(expected.Lastname);
            response.Data
                    .BirthDate.Should().Be(expected.BirthDate);
            response.Data
                    .Gender.Should().Be(expected.Gender);
        }


        [Fact]
        public async void UpdatePerson_ShouldReturnError_WhenIdNotExist()
        {
            // Arrange
            var param = new UpdatePersonCommand(Id: 7,
                                                Firstname: "Vhon",
                                                Lastname: "Rey",
                                                BirthDate: _dateTimeService.Now,
                                                Gender: "Male");

            // Act
            var response = await _sut.Handle(param, CancellationToken.None);

            // Assert
            response.Error
                    .Should().BeTrue();
        }


        [Fact]
        public async void UpdatePerson_ShouldReturnError_WhenParametersAreNotValid()
        {
            // Arrange
            var validator = new UpdatePersonCommandValidator();
            var param = new UpdatePersonCommand(Id: 1,
                                                Firstname: "",
                                                Lastname: "Rey",
                                                BirthDate: _dateTimeService.Now,
                                                Gender: "Male");

            var param2 = new UpdatePersonCommand(Id: 1,
                                                Firstname: "Rey",
                                                Lastname: "",
                                                BirthDate: _dateTimeService.Now,
                                                Gender: "Male");

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
