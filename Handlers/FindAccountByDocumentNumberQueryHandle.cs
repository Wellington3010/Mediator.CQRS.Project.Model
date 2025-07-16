using AutoMapper;
using mediator_cqrs_project.Interfaces;
using mediator_cqrs_project.Models;
using mediator_cqrs_project.Notifications;
using mediator_cqrs_project.Queries;
using System;

using System.Threading;
using System.Threading.Tasks;
using FastMiddleware.Interfaces;
using mediator_cqrs_project.Persistence;
using mediator_cqrs_project.Repositories;
using Microsoft.EntityFrameworkCore;

namespace mediator_cqrs_project.Handlers
{
    public class FindAccountByDocumentNumberQueryHandle : IRequestHandler<FindAccountByDocumentNumberQuery, QueryAccountNotification>
    {
        private readonly IMapper _mapper;

        private readonly IFastMiddleware _fastMiddleware;

        private readonly IRepository<Account> _accountRepository;

        public FindAccountByDocumentNumberQueryHandle(IMapper mapper, IFastMiddleware fastMiddleware, IRepository<Account> accountRepository)
        {
            this._mapper = mapper;
            this._fastMiddleware = fastMiddleware;
            this._accountRepository = accountRepository;
        }

        public async Task<QueryAccountNotification> Handle(FindAccountByDocumentNumberQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var account = new Account();

               if(Equals(account, null))
               {
                  await _fastMiddleware.Publish(new ErrorNotification { ErrorMessage = $"Conta associada ao documento {request.DocumentNumber} não encontrada" });
                  return await Task.FromResult(new QueryAccountNotification());
               }

               var accountNotification = _mapper.Map<QueryAccountNotification>(account);

               await _fastMiddleware.Publish(accountNotification);

               return await Task.FromResult(accountNotification);
            }
            catch (Exception ex)
            {
               await _fastMiddleware.Publish(new ErrorNotification { ErrorMessage = $"Error processing request: {ex}" });

               await Task.FromException(ex);

               return await Task.FromResult(new QueryAccountNotification());
            }
        }
    }
}
