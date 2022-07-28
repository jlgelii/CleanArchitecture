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

namespace CleanArchitecture.Application.Persons.Queries.GetPersonById
{
    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, Response<Person>>
    {
        private readonly ISampleDbContext _context;

        public GetPersonByIdQueryHandler(ISampleDbContext context)
        {
            this._context = context;
        }

        public async Task<Response<Person>> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Person
                                       .FirstOrDefaultAsync(p => p.Id == request.Id);

            if (result == null)
                return await Task.FromResult(Response.Fail(result, "Person does not exist."));

            return await Task.FromResult(Response.Success(result));
        }
    }
}
