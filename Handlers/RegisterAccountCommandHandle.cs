using AutoMapper;
using mediator_cqrs_project.Commands;
using mediator_cqrs_project.Interfaces;
using mediator_cqrs_project.Models;
using mediator_cqrs_project.Notifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FastMiddleware.Interfaces;
using mediator_cqrs_project.Persistence;
using mediator_cqrs_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace mediator_cqrs_project.Handlers
{
    public class RegisterAccountCommandHandle : IRequestHandler<RegisterAccountCommand, string>
    {
        private readonly IMapper _mapper;

        private readonly IFastMiddleware _fastMiddleware;

        private readonly IRepository<Account> _accountRepository;


        public RegisterAccountCommandHandle(IMapper mapper, IFastMiddleware fastMiddleware, IRepository<Account> accountRepository)
        {
            this._mapper = mapper;
            this._fastMiddleware = fastMiddleware;
            this._accountRepository = accountRepository;
        }


        public async Task<string> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var account = _mapper.Map<Account>(request);

                await _accountRepository.Save(account);

                await _fastMiddleware.Publish(_mapper.Map<CommandAccountRegisterNotification>(account));

                return await Task.FromResult("Account successful saved");
            }
            catch (Exception ex)
            {
                await _fastMiddleware.Publish(new ErrorNotification { ErrorMessage = $"Error processing request: {ex}" });

                await Task.FromCanceled(cancellationToken);

                return $"Error processing request: {ex}";
            }
        }
    }
}
