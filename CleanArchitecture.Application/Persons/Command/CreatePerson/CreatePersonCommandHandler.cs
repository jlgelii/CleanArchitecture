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

namespace CleanArchitecture.Application.Persons.Command.CreatePerson
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Response<Person>>
    {
        private readonly ISampleDbContext _context;

        public CreatePersonCommandHandler(ISampleDbContext context)
        {
            this._context = context;
        }

        public async Task<Response<Person>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            Person person = null; 
            var useraccount = await _context.UserAccount
                                            .FirstOrDefaultAsync(u => u.Id == request.UserAccountId);

            if (useraccount == null)
                return await Task.FromResult(Response.Fail(person, "Useraccount does not exist."));

            person = new Person
            {
                BirthDate = request.BirthDate,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Gender = request.Gender,
                UserAccountId = request.UserAccountId
            };

            await _context.Person.AddAsync(person);
            await _context.SaveChangesAsync();

            return await Task.FromResult(Response.Success(person));
        }
    }
}
