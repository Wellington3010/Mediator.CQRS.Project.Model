using mediator_cqrs_project.Commands;
using mediator_cqrs_project.Notifications;
using mediator_cqrs_project.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mediator_cqrs_project.Controllers
{
    [Route("accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            this._mediator = mediator; 
        }

        [HttpPost]
        public async Task<ActionResult> RegisterAccount([FromBody] RegisterAccountCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

          return Created(await _mediator.Send(request),request);
        }

        [HttpGet]
        public async Task<ActionResult> FindByAccountDocumentNumber([FromBody] FindAccountByDocumentNumberQuery request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _mediator.Send(request));
        }

        [HttpGet]
        [Route("type")]
        public async Task<ActionResult> FindAccountByType([FromBody] FindAccountByTypeQuery request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _mediator.Send(request));
        }


        [HttpPut]
        public async Task<ActionResult> UpdateAccount([FromBody] UpdateAccountCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _mediator.Send(request));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAccount([FromBody] DeleteAccountCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _mediator.Send(request));
        }
    }
}
