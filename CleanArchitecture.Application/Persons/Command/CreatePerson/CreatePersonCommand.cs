using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;

namespace CleanArchitecture.Application.Persons.Command.CreatePerson
{
    public class CreatePersonCommand : IRequest<Response<Person>>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
    }
}