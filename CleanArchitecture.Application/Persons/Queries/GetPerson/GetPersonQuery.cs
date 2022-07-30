using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace CleanArchitecture.Application.Persons.Queries.GetPerson
{
    public class GetPersonQuery : IRequest<Response<List<Person>>>
    {
    }
}