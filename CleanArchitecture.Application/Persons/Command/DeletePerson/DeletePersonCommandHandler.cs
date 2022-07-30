using CleanArchitecture.Application.Configurations.Database;
using CleanArchitecture.Application.Configurations.Services;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Persons.Command.DeletePerson
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Response<Person>>
    {
        private readonly ISampleDbContext _context;
        private readonly IDateTimeService _dateTime;
        private readonly IJwtServices _jwtServices;

        public DeletePersonCommandHandler(ISampleDbContext context, 
            IDateTimeService dateTime,
            IJwtServices jwtServices)
        {
            this._context = context;
            this._dateTime = dateTime;
            this._jwtServices = jwtServices;
        }

        public async Task<Response<Person>> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = _context.Person
                                 .FirstOrDefault(p => p.Id == request.Id);

            if (person == null)
                return await Task.FromResult(Response.Fail(person, "Person does not exist."));

            person.Deleted = true;
            person.DeletedDate = _dateTime.Now;
            person.DeletedBy = _jwtServices.GetLoggedUser().UserId;

            await _context.SaveChangesAsync();

            return await Task.FromResult(Response.Success(person));
        }
    }
}
