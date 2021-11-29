using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using mediator_cqrs_project.Commands;
using mediator_cqrs_project.Interfaces;
using mediator_cqrs_project.Models;
using mediator_cqrs_project.Notifications;
using MediatR;

namespace mediator_cqrs_project.Handlers
{
    public class UpdateAccountCommandHandle : IRequestHandler<UpdateAccountCommand, string>
    {
        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        private readonly IRepository<Account> _accountRepository;


        public UpdateAccountCommandHandle(IMapper mapper, IMediator mediator, IRepository<Account> accountRepository)
        {
            this._mapper = mapper;
            this._mediator = mediator;
            this._accountRepository = accountRepository;
        }

        public async Task<string> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var account = _mapper.Map<Account>(request);

                await _accountRepository.Update(account);

                await _mediator.Publish(_mapper.Map<CommandAccountUpdateNotification>(account));

                return await Task.FromResult("Account successful updated");
            }
            catch
            {
                await _mediator.Publish(new ErrorNotification { ErrorMessage = $"Erro ao atualizar conta." });

                return $"Conta não encontrada para atualização";
            }
        }
    }
}
