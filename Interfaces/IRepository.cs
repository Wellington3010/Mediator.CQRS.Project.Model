using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mediator_cqrs_project.Models;

namespace mediator_cqrs_project.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> FindAll();
        Task<IEnumerable<Account>> FindByType(int code);
        Task<Account> FindByCode(string code);

        Task Save(Account data);

        Task Update(Account data);

        Task Delete(Account data);
    }
}
