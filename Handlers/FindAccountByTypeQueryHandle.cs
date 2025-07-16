using AutoMapper;
using mediator_cqrs_project.Enums;
using mediator_cqrs_project.Interfaces;
using mediator_cqrs_project.Models;
using mediator_cqrs_project.Notifications;
using mediator_cqrs_project.Queries;
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
    public class FindAccountByTypeQueryHandle : IRequestHandler<FindAccountByTypeQuery, QueryAccountListNotification>
    {
        private readonly IMapper _mapper;

        private readonly IFastMiddleware _fastMiddleware;

        private readonly IRepository<Account> _accountRepository;

        private QueryAccountListNotification _accountsList;

        public FindAccountByTypeQueryHandle(IMapper mapper, IFastMiddleware fastMiddleware, IRepository<Account> accountRepository)
        {
            this._mapper = mapper;
            this._fastMiddleware = fastMiddleware;
            this._accountRepository = accountRepository;
            this._accountsList = new QueryAccountListNotification();
            this._accountsList.accounts = new List<QueryAccountNotification>();
        }

        public async Task<QueryAccountListNotification> Handle(FindAccountByTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var accountCollection = await _accountRepository.FindByType((int)request.AccountType);

                if (!accountCollection.Any())
                {
                    await _fastMiddleware.Publish(new ErrorNotification { ErrorMessage = $"Contas do tipo {request.AccountType} não encontradas" });
                    return await Task.FromResult(new QueryAccountListNotification());
                }

                var accountEnumarator = accountCollection.GetEnumerator();

                while (accountEnumarator.MoveNext()) {
                    _accountsList.accounts.Add(_mapper.Map<QueryAccountNotification>(accountEnumarator.Current));
                }

                await _fastMiddleware.Publish(_accountsList);

                return await Task.FromResult(_accountsList);
            }
            catch (Exception ex)
            {
                await _fastMiddleware.Publish(new ErrorNotification { ErrorMessage = $"Error processing request: {ex}" });

                return await Task.FromResult(_accountsList);
            }
        }
    }
}
