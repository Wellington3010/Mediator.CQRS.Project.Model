using mediator_cqrs_project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mediator_cqrs_project.Persistence
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions options) : base (options){ }

        public DbSet<Account> Accounts { get; set; }
    }
}
