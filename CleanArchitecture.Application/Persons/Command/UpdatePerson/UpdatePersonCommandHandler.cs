using CleanArchitecture.Application.Configurations.Database;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Persons.Command.UpdatePerson
{
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Response<Person>>
    {
        private readonly ISampleDbContext _context;

        public UpdatePersonCommandHandler(ISampleDbContext context)
        {
            this._context = context;
        }

        public async Task<Response<Person>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = _context.Person
                                .FirstOrDefault(x => x.Id == request.Id);

            if (person == null)
                return await Task.FromResult(Response.Fail(person, "Person does not exist."));

            person.Firstname = request.Firstname;
            person.Lastname = request.Lastname;
            person.Gender = request.Gender;
            person.BirthDate = request.BirthDate;

            await _context.SaveChangesAsync();

            return await Task.FromResult(Response.Success(person));
        }
    }
}
