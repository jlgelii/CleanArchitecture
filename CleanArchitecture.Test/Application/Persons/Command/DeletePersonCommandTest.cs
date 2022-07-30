using CleanArchitecture.Application.Persons.Command.DeletePerson;
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
    public class DeletePersonCommandTest : BaseTest
    {
        private readonly DeletePersonCommandHandler _sut;

        public DeletePersonCommandTest()
        {
            _sut = new DeletePersonCommandHandler(_context, _dateTimeService, _jwtServices);
        }


        [Fact]
        public async void DeletePerson_ShouldDeletePerson_WhenParametersAreValid()
        {
            // Arrange
            var param = new DeletePersonCommand(id: 1);

            // Act
            var response = await _sut.Handle(param, CancellationToken.None);

            // Assert
            response.Error
                    .Should().BeFalse();

            response.Data
                    .Deleted.Should().BeTrue();
        }


        [Fact]
        public async void DeletePerson_ShouldReturnError_WhenPersonNotExist()
        {
            // Arrange
            var param = new DeletePersonCommand(id: 15);

            // Act
            var response = await _sut.Handle(param, CancellationToken.None);

            // Assert
            response.Error
                    .Should().BeTrue();
        }
    }
}
