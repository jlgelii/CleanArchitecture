﻿using CleanArchitecture.Application.UserAccounts.Command.CreateUser;
using CleanArchitecture.Application.UserAccounts.Command.UpdateUser;
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
                return BadRequest(response.ModelStateError);

            return Ok(response.Data);
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<UserAccount>> UpdateUserAccount(int id, UpdateUserCommand command)
        {
            command.Id = id;

            var response = await _mediator.Send(command);
            if (response.Error)
                return BadRequest(response.ModelStateError);

            return Ok(response.Data);

        }
    }
}
