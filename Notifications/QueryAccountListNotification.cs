using mediator_cqrs_project.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastMiddleware.Interfaces;

namespace mediator_cqrs_project.Notifications
{
    public class QueryAccountListNotification : INotification
    {
       public List<QueryAccountNotification> accounts { get; set; }
    }
}
