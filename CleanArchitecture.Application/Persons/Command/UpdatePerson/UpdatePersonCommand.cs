using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;

namespace CleanArchitecture.Application.Persons.Command.UpdatePerson
{
    public record UpdatePersonCommand(int Id, string Firstname, string Lastname, DateTime BirthDate, string Gender) : IRequest<Response<Person>>;
}