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

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="gender"></param>
        /// <param name="birthdate"></param>
        [Theory]
        [InlineData(1, "Jepoy", "Leroy", "Male", "1-1-2022")]
        public async void UpdatePerson_ShouldUpdatePerson_WhenParametersAreValid(int id, string firstname, string lastname, string gender, DateTime birthdate)
        {
            // Arrange
            var param = new UpdatePersonCommand(id, firstname, lastname, birthdate, gender);

            // Act
            var response = await _sut.Handle(param, CancellationToken.None);

            // Assert
            var expected = _context.Person
                                   .FirstOrDefault(p => p.Id == param.Id);

            response.Error
                    .Should().BeFalse();

            response.Data
                    .Should().BeEquivalentTo(expected);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="gender"></param>
        /// <param name="birthdate"></param>
        [Theory]
        [InlineData(17, "Jepoy", "Leroy", "Male", "1-1-2022")]
        public async void UpdatePerson_ShouldReturnError_WhenIdNotExist(int id, string firstname, string lastname, string gender, DateTime birthdate)
        {
            // Arrange
            var param = new UpdatePersonCommand(id, firstname, lastname, birthdate, gender);

            // Act
            var response = await _sut.Handle(param, CancellationToken.None);

            // Assert
            response.Error
                    .Should().BeTrue();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="gender"></param>
        /// <param name="birthdate"></param>
        [Theory]
        [InlineData(1, "", "Leroy", "Male", "1-1-2022")]
        [InlineData(1, "Jepoy", "", "Male", "1-1-2022")]
        public async void UpdatePerson_ShouldReturnError_WhenParametersAreNotValid(int id, string firstname, string lastname, string gender, DateTime birthdate)
        {
            // Arrange
            var validator = new UpdatePersonCommandValidator();
            var param = new UpdatePersonCommand(id, firstname, lastname, birthdate, gender);

            // Act
            var response = await validator.Validate(param);

            // Assert
            response.IsSuccessful
                    .Should().BeFalse();
        }
    }
}
