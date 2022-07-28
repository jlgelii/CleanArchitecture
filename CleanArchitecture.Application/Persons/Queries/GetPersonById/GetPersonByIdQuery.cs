using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Persons.Queries.GetPersonById
{
    public record GetPersonByIdQuery(int Id) : IRequest<Response<Person>>;
    
}