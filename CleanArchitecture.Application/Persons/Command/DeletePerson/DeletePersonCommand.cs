using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Persons.Command.DeletePerson
{
    public class DeletePersonCommand : IRequest<Response<Person>>
    {
        public DeletePersonCommand(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}