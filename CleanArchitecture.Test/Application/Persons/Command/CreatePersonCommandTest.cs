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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="gender"></param>
        /// <param name="birthdate"></param>
        /// <param name="useraccountid"></param>
        [Theory]
        [InlineData("Jepoy", "Leroy", "Male", "1-1-2022", 1)]
        public async void CreatePerson_ShouldAddPerson_WhenParametersAreValid(string firstname, string lastname, string gender, DateTime birthdate, int useraccountid)
        {
            // Arrange
            var param = new CreatePersonCommand(firstname, lastname, birthdate, gender, useraccountid);

            // Act
            var response = await _sut.Handle(param, CancellationToken.None);

            // Assert
            var expectedExist = _context.Person
                                   .FirstOrDefault(p => p.Id == response.Data.Id);

            var expectedCount = _context.Person
                                        .Count();

            var expected = new Person()
            {
                Id = 6,
                Firstname = firstname,
                Lastname = lastname,
                Gender = gender,
                BirthDate = birthdate,
                CreatedDate = _dateTimeService.Now,
                CreatedBy = _jwtServices.GetLoggedUser().UserId,
                UserAccountId = useraccountid
            };

            response.Error
                    .Should().BeFalse();

            response.Data
                    .Should().BeEquivalentTo(expected);

            expectedCount.Should().Be(6);
            expectedExist.Should().NotBeNull();

            response.Data
                    .Should().BeEquivalentTo(expected);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="gender"></param>
        /// <param name="birthdate"></param>
        /// <param name="useraccountid"></param>
        [Theory]
        [InlineData("", "Leroy", "Male", "1-1-2022", 1)]
        [InlineData("Jepoy", "", "Male", "1-1-2022", 1)]
        public async void CreatePerson_ShouldReturnError_WhenRequiredFieldsDoesNotExist(string firstname, string lastname, string gender, DateTime birthdate, int useraccountid)
        {
            // Arrange
            var validator = new CreatePersonCommandValidator();
            var param = new CreatePersonCommand(firstname, lastname, birthdate, gender, useraccountid);

            // Act
            var response = await validator.Validate(param);

            // Assert
            response.IsSuccessful
                    .Should().BeFalse();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="gender"></param>
        /// <param name="birthdate"></param>
        /// <param name="useraccountid"></param>
        [Theory]
        [InlineData("Jepoy", "Leroy", "Male", "1-1-2022", 15)]
        public async void CreatePerson_ShouldReturnError_WhenUserAccountDoesNotExist(string firstname, string lastname, string gender, DateTime birthdate, int useraccountid)
        {
            // Arrange
            var param = new CreatePersonCommand(firstname, lastname, birthdate, gender, useraccountid);

            // Act
            var response = await _sut.Handle(param, CancellationToken.None);

            // Assert
            response.Error
                    .Should().BeTrue();
        }
    }
}
