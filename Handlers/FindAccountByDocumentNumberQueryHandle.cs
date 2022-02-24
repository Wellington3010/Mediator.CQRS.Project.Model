using AutoMapper;
using mediator_cqrs_project.Interfaces;
using mediator_cqrs_project.Models;
using mediator_cqrs_project.Notifications;
using mediator_cqrs_project.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace mediator_cqrs_project.Handlers
{
    public class FindAccountByDocumentNumberQueryHandle : IRequestHandler<FindAccountByDocumentNumberQuery, QueryAccountNotification>
    {
        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        private readonly IRepository<Account> _accountRepository;

        public FindAccountByDocumentNumberQueryHandle(IMapper mapper, IMediator mediator, IRepository<Account> accountRepository)
        {
            this._mapper = mapper;
            this._mediator = mediator;
            this._accountRepository = accountRepository;
        }

        public async Task<QueryAccountNotification> Handle(FindAccountByDocumentNumberQuery request, CancellationToken cancellationToken)
        {
            try
            {
               var account = await _accountRepository.FindByCode(request.DocumentNumber);

               if(Equals(account, null))
               {
                  await _mediator.Publish(new ErrorNotification { ErrorMessage = $"Conta associada ao documento {request.DocumentNumber} não encontrada" });
                  return await Task.FromResult(new QueryAccountNotification());
               }

               var accountNotification = _mapper.Map<QueryAccountNotification>(account);

               await _mediator.Publish(accountNotification);

               return await Task.FromResult(accountNotification);
            }
            catch (Exception ex)
            {
               await _mediator.Publish(new ErrorNotification { ErrorMessage = $"Error processing request: {ex}" });

               await Task.FromException(ex);

               return await Task.FromResult(new QueryAccountNotification());
            }
        }
    }
}
