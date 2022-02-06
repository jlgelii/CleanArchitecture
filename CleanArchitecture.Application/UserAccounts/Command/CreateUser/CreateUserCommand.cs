using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UserAccounts.Command.CreateUser
{
    public class CreateUserCommand : IRequest<Response<UserAccount>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
