using mediator_cqrs_project.Enums;
using mediator_cqrs_project.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastMiddleware.Interfaces;

namespace mediator_cqrs_project.Queries
{
    public class FindAccountByTypeQuery : IRequest<QueryAccountListNotification>
    {
       public EnumAccountTypes AccountType { get; set; }
    }
}
