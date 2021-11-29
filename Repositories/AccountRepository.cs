using mediator_cqrs_project.Interfaces;
using mediator_cqrs_project.Models;
using mediator_cqrs_project.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mediator_cqrs_project.Repositories
{
    public class AccountRepository : IRepository<Account>
    {
        private readonly AccountContext _accountsContext;
        public AccountRepository(AccountContext accountsContext)
        {
            this._accountsContext = accountsContext;
        }

        public async Task Delete(Account account)
        {
            await Task.Run(() => {

                _accountsContext.Accounts.Remove(account);

                _accountsContext.SaveChanges();
            });
        }

        public async Task<IEnumerable<Account>> FindAll()
        {
            return await Task.Run(() => _accountsContext.Accounts.AsEnumerable());
        }

        public async Task<IEnumerable<Account>> FindByType(int accountType)
        {
            return await Task.Run(() => _accountsContext.Accounts.Where(x => x.AccountType == accountType).AsEnumerable());
        }


        public async Task<Account> FindByCode(string documentNumber)
        {
           return await Task.Run(() => _accountsContext.Accounts.FirstOrDefault(x => x.DocumentNumber == documentNumber));
        }


        public async Task Save(Account account)
        {
           await Task.Run(() => {

               _accountsContext.Accounts.Add(account);

               _accountsContext.SaveChanges();
           });
        }


        public async Task Update(Account account)
        {
            try
            {
                var updatedAccount = _accountsContext.Accounts.FirstOrDefault(x => x.DocumentNumber == account.DocumentNumber);

                if (Equals(updatedAccount, null))
                     throw new Exception("Object not found");


                updatedAccount.AccountOwner = account.AccountOwner;
                updatedAccount.AccountBalance = account.AccountBalance;
                updatedAccount.AccountType = account.AccountType;

                _accountsContext.Entry(updatedAccount).State = EntityState.Modified;

                await _accountsContext.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
           
        }
    }
}
