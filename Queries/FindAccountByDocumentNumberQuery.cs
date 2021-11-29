using mediator_cqrs_project.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mediator_cqrs_project.Queries
{
    public class FindAccountByDocumentNumberQuery : IRequest<QueryAccountNotification>
    {
        public string DocumentNumber { get; set; }
    }
}
