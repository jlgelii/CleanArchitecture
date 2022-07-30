using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;

namespace CleanArchitecture.Application.Persons.Command.CreatePerson
{
    public record CreatePersonCommand(string Firstname, string Lastname, DateTime BirthDate, string Gender, int UserAccountId) : IRequest<Response<Person>>;
}