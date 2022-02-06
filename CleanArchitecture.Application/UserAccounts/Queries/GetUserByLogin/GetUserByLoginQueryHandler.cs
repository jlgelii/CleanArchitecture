using CleanArchitecture.Application.Configurations.Database;
using CleanArchitecture.Application.Configurations.Services;
using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Tokens;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UserAccounts.Queries.GetUserByLogin
{
    public class GetUserByLoginQueryHandler : IRequestHandler<GetUserByLoginQuery, Response<GetUserByLoginDto>>
    {
        private readonly ISampleDbContext _context;
        private readonly IJwtServices _jwtServices;

        public GetUserByLoginQueryHandler(ISampleDbContext context, IJwtServices jwtServices)
        {
            this._context = context;
            this._jwtServices = jwtServices;
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

            user.Token = _jwtServices.CreateToken(new TokenData
            {
                UserId = user.Id,
            });

            return Response.Success(user);
        }
    }
}
