using AutoMapper;
using mediator_cqrs_project.Commands;
using mediator_cqrs_project.Interfaces;
using mediator_cqrs_project.Models;
using mediator_cqrs_project.Notifications;
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
    public class DeleteAccountCommandHandle : IRequestHandler<DeleteAccountCommand, string>
    {
        private readonly IFastMiddleware _fastMiddleware;

        private readonly IAccountRepository _accountRepository;

        private readonly IMapper _mapper;


        public DeleteAccountCommandHandle(IFastMiddleware fastMiddleware, IMapper mapper)
        {
            this._fastMiddleware = fastMiddleware;
            this._mapper = mapper;
            this._accountRepository = new AccountRepository(new AccountContext(new DbContextOptions<AccountContext>()));
        }
        public async Task<string> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deletedAccount = await _accountRepository.FindByCode(request.DocumentNumber);

                if (deletedAccount == null)
                {
                    await _fastMiddleware.Publish(new ErrorNotification { ErrorMessage = $"Conta associada ao documento {request.DocumentNumber} não encontrada" });
                    return await Task.FromResult("");
                }


                await _accountRepository.Delete(deletedAccount);

                await _fastMiddleware.Publish(_mapper.Map<CommandAccountDeleteNotification>(deletedAccount));

                return await Task.FromResult("Account successful deleted");
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
