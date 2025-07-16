using mediator_cqrs_project.Interfaces;
using mediator_cqrs_project.Models;
using mediator_cqrs_project.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace mediator_cqrs_project.Repositories
{
    public class AccountRepository : IRepository<Account>
    {
        public AccountRepository() { }
       
        public async Task Delete(Account account)
        {
            var serviceCollection = new ServiceCollection();
            using var scope = serviceCollection.BuildServiceProvider().CreateScope();
            var accountContext = scope.ServiceProvider.GetRequiredService<AccountContext>();

            await Task.Run(() => {

                accountContext.Accounts.Remove(account);

                accountContext.SaveChanges();
            });
        }

        public async Task<IEnumerable<Account>> FindAll()
        {
            var serviceCollection = new ServiceCollection();
            using var scope = serviceCollection.BuildServiceProvider().CreateScope();
            var accountContext = scope.ServiceProvider.GetRequiredService<AccountContext>();
            return await Task.Run(() => accountContext.Accounts.AsEnumerable());
        }

        public async Task<IEnumerable<Account>> FindByType(int accountType)
        {
            var serviceCollection = new ServiceCollection();
            using var scope = serviceCollection.BuildServiceProvider().CreateScope();
            var accountContext = scope.ServiceProvider.GetRequiredService<AccountContext>();
            return await Task.Run(() => accountContext.Accounts.Where(x => x.AccountType == accountType).AsEnumerable());
        }


        public async Task<Account> FindByCode(string documentNumber)
        {
           var serviceCollection = new ServiceCollection();
           using var scope = serviceCollection.BuildServiceProvider().CreateScope();
           var accountContext = scope.ServiceProvider.GetRequiredService<AccountContext>();
           return await Task.Run(() => accountContext.Accounts.FirstOrDefault(x => x.DocumentNumber == documentNumber));
        }


        public async Task Save(Account account)
        {
           var serviceCollection = new ServiceCollection();
           using var scope = serviceCollection.BuildServiceProvider().CreateScope();
           var accountContext = scope.ServiceProvider.GetRequiredService<AccountContext>();
           
           await Task.Run(() => {

               accountContext.Accounts.Add(account);

               accountContext.SaveChanges();
           });
        }


        public async Task Update(Account account)
        {
            try
            {
                var serviceCollection = new ServiceCollection();
                using var scope = serviceCollection.BuildServiceProvider().CreateScope();
                var accountContext = scope.ServiceProvider.GetRequiredService<AccountContext>();
                
                var updatedAccount = accountContext.Accounts.FirstOrDefault(x => x.DocumentNumber == account.DocumentNumber);

                if (Equals(updatedAccount, null))
                     throw new Exception("Object not found");

                updatedAccount.AccountOwner = account.AccountOwner;
                updatedAccount.AccountBalance = account.AccountBalance;
                updatedAccount.AccountType = account.AccountType;

                accountContext.Entry(updatedAccount).State = EntityState.Modified;

                await accountContext.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
           
        }
       
    }
}
