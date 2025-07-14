using mediator_cqrs_project.Commands;
using mediator_cqrs_project.Notifications;
using mediator_cqrs_project.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FastMiddleware.Interfaces;

namespace mediator_cqrs_project.Controllers
{
    [Route("accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IFastMiddleware _fastMiddleware;

        public AccountsController(IFastMiddleware fastMiddleware)
        {
            this._fastMiddleware = fastMiddleware; 
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> RegisterAccount([FromBody] RegisterAccountCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

          return Created(await _fastMiddleware.Send(request),request);
        }

        [HttpPost]
        [Route("findAccountByDocumentNumber")]
        public async Task<ActionResult> FindByAccountDocumentNumber([FromBody] FindAccountByDocumentNumberQuery request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _fastMiddleware.Send(request));
        }

        [HttpPost]
        [Route("findAccountByType")]
        public async Task<ActionResult> FindAccountByType([FromBody] FindAccountByTypeQuery request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _fastMiddleware.Send(request));
        }


        [HttpPut]
        public async Task<ActionResult> UpdateAccount([FromBody] UpdateAccountCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _fastMiddleware.Send(request));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAccount([FromBody] DeleteAccountCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(await _fastMiddleware.Send(request));
        }
    }
}
