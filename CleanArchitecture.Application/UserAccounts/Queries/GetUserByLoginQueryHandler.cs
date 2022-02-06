using CleanArchitecture.Application.Configurations.Database;
using CleanArchitecture.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UserAccounts.Queries
{
    public class GetUserByLoginQueryHandler : IRequestHandler<GetUserByLoginQuery, Response<GetUserByLoginDto>>
    {
        private readonly ISampleDbContext _context;

        public GetUserByLoginQueryHandler(ISampleDbContext context)
        {
            this._context = context;
        }

        public async Task<Response<GetUserByLoginDto>> Handle(GetUserByLoginQuery request, CancellationToken cancellationToken)
        {
            var user = _context.UserAccount
                               .Select(u => new GetUserByLoginDto
                               {
                                   Id = u.Id,
                                   Username = u.Username,
                                   Password = u.Password,
                               })
                               .FirstOrDefault(u => u.Username == request.Username
                                                 && u.Password == request.Password);

            if (user == null)
                return await Task.FromResult(Response.Fail(user, "User does not exist."));

            return Response.Success(user);
        }
    }
}
