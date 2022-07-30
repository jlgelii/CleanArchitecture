using CleanArchitecture.Application.Configurations.Database;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Persons.Queries.GetPerson
{
    public class GetPersonQueryHandler : IRequestHandler<GetPersonQuery, Response<List<Person>>>
    {
        private readonly ISampleDbContext _context;

        public GetPersonQueryHandler(ISampleDbContext context)
        {
            this._context = context;
        }

        public async Task<Response<List<Person>>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Person
                                 .OrderBy(p => p.Id)
                                 .ToList();

            return await Task.FromResult(Response.Success(result));
        }
    }
}
