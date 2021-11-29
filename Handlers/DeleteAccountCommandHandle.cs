using AutoMapper;
using mediator_cqrs_project.Commands;
using mediator_cqrs_project.Interfaces;
using mediator_cqrs_project.Models;
using mediator_cqrs_project.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator_cqrs_project.Handlers
{
    public class DeleteAccountCommandHandle : IRequestHandler<DeleteAccountCommand, string>
    {
        private readonly IMediator _mediator;

        private readonly IRepository<Account> _accountRepository;

        private readonly IMapper _mapper;


        public DeleteAccountCommandHandle(IMediator mediator, IRepository<Account> accountRepository, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
            this._accountRepository = accountRepository;
        }
        public async Task<string> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deletedAccount = await _accountRepository.FindByCode(request.DocumentNumber);

                if (deletedAccount == null)
                {
                    await _mediator.Publish(new ErrorNotification { ErrorMessage = $"Conta associada ao documento {request.DocumentNumber} não encontrada" });
                    return await Task.FromResult("");
                }


                await _accountRepository.Delete(deletedAccount);

                await _mediator.Publish(_mapper.Map<CommandAccountDeleteNotification>(deletedAccount));

                return await Task.FromResult("Account successful deleted");
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ErrorNotification { ErrorMessage = $"Error processing request: {ex}" });

                await Task.FromCanceled(cancellationToken);

                return $"Error processing request: {ex}";
            }
        }
    }
}
