using CleanArchitecture.Application.UserAccounts.Command.CreateUser;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitecture.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            this._mediator = mediator;
        }


        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<UserAccount>> CreateUserAccount(CreateUserCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.Error)
                return BadRequest(response.Error);

            return Ok(response.Data);
        }
    }
}
