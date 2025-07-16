using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FastMiddleware.Interfaces;
using mediator_cqrs_project.Commands;
using mediator_cqrs_project.Interfaces;
using mediator_cqrs_project.Models;
using mediator_cqrs_project.Notifications;
using mediator_cqrs_project.Persistence;
using mediator_cqrs_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace mediator_cqrs_project.Handlers
{
    public class UpdateAccountCommandHandle : IRequestHandler<UpdateAccountCommand, string>
    {
        private readonly IMapper _mapper;

        private readonly IFastMiddleware _fastMiddleware;

        private readonly IRepository<Account> _accountRepository;


        public UpdateAccountCommandHandle(IMapper mapper, IFastMiddleware fastMiddleware, IRepository<Account> accountRepository)
        {
            this._mapper = mapper;
            this._fastMiddleware = fastMiddleware;
            this._accountRepository = accountRepository;
        }

        public async Task<string> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var account = _mapper.Map<Account>(request);

                await _accountRepository.Update(account);

                await _fastMiddleware.Publish(_mapper.Map<CommandAccountUpdateNotification>(account));

                return await Task.FromResult("Account successful updated");
            }
            catch
            {
                await _fastMiddleware.Publish(new ErrorNotification { ErrorMessage = $"Erro ao atualizar conta." });

                return $"Conta não encontrada para atualização";
            }
        }
    }
}
